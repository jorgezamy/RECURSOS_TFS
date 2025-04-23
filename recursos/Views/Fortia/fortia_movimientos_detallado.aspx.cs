using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.Drawing;
using System.IO;
using System.Globalization;
using recursos.Content;

namespace recursos.Views.Fortia
{
    public partial class fortia_movimientos_detallado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            generar();
        }

        private void redirigir()
        {
            if (Request.QueryString["opcion"].ToString() == "1")
            {
                //Redirigir a pantalla
                Response.Redirect("../actualizar_fortia.aspx?opcion=" + "0");
            }
            else
            {
                //Servidor 
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void generar()
        {
            var sp_loadDetallado = DbUtil.GetCursor("sp_recursos_fortia_movimientos_detallado",
                new SqlParameter("@finiquito", "0"),
                new SqlParameter("@noEmpleado", "")
                );

            grid_movimientos_detallado.DataSource = sp_loadDetallado;
            grid_movimientos_detallado.DataBind();

            if (grid_movimientos_detallado.Rows.Count == 0) //Si no hay cambios
            {
                redirigir();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Reporte-" + "Movimientos" + ".xls");
            Response.AddHeader("X-Download-Options", "noopen");

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grid_movimientos_detallado.AllowPaging = false;
                grid_movimientos_detallado.DataSource = sp_loadDetallado;
                grid_movimientos_detallado.DataBind();

                grid_movimientos_detallado.HeaderRow.BackColor = Color.White;

                foreach (TableCell cell in grid_movimientos_detallado.HeaderRow.Cells)
                {
                    cell.BackColor = grid_movimientos_detallado.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_movimientos_detallado.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_movimientos_detallado.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_movimientos_detallado.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_movimientos_detallado.RenderControl(hw);

                if (!Directory.Exists("/GCDM/fortiaReportes/Movimientos-Detallado" ))
                {
                    Directory.CreateDirectory("/GCDM/fortiaReportes/Movimientos-Detallado");
                }

                File.WriteAllText("/GCDM/fortiaReportes/" + "Movimientos-Detallado" + "/" + "MovimientosDetallado" + ".xls", sw.ToString());
                uploadFile.sftp.UploadSFTPFile("/GCDM/fortiaReportes/" + "Movimientos-Detallado" + "/" + "MovimientosDetallado" + "" + ".xls");
                //if (Request.QueryString["opcion"].ToString() != "0")

                redirigir();

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.End();
                Response.Flush();
            }
        }
    }
}