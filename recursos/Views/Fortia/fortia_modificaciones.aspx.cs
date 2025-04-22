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
    public partial class fortia_modificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            reportes_load_modificaciones(3, "Modificaciones");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void redirigir()
        {
            Response.Redirect("fortia_modificaciones_salarios.aspx?opcion=" + Request.QueryString["opcion"].ToString());

            ////Response.Redirect("fortia_modificaciones.aspx");
            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }


        private void reportes_load_modificaciones(int opcion, string tipoReporte)
        {
            var sp_loadGrid1 = DbUtil.GetCursor("sp_recursos_fortia_reportes_loadDatos",
                new SqlParameter("@opcion", opcion)
                );

            grid_modificaciones.DataSource = sp_loadGrid1;
            grid_modificaciones.DataBind();


            if (grid_modificaciones.Rows.Count == 0) //Si no hay cambios de modificaciones
            {
                redirigir();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Reporte-" + tipoReporte + ".xls");
            Response.AddHeader("X-Download-Options", "noopen");

            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grid_modificaciones.AllowPaging = false;
                grid_modificaciones.DataSource = sp_loadGrid1;
                grid_modificaciones.DataBind();

                grid_modificaciones.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grid_modificaciones.HeaderRow.Cells)
                {
                    cell.BackColor = grid_modificaciones.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_modificaciones.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_modificaciones.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_modificaciones.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_modificaciones.RenderControl(hw);

                if (!Directory.Exists("/GCDM/fortiaReportes/" + tipoReporte))
                {
                    Directory.CreateDirectory("/GCDM/fortiaReportes/" + tipoReporte);
                }

                File.WriteAllText("/GCDM/fortiaReportes/" + tipoReporte + "/" + tipoReporte + ".xls", sw.ToString());
                uploadFile.sftp.UploadSFTPFile("/GCDM/fortiaReportes/" + tipoReporte + "/" + tipoReporte + ".xls");

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