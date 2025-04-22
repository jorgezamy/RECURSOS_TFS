using MsBarco;
using recursos.Content;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace recursos.Views.Fortia
{
    public partial class fortia_puestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            reportes_load(12, "Departamentos");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void redirigir()
        {
            Response.Redirect("fortia_vacaciones.aspx?opcion=" + Request.QueryString["opcion"].ToString());

            //Response.Redirect("fortia_modificaciones.aspx");
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        private void reportes_load(int opcion, string tipoReporte)
        {
            var sp_loadGrid1 = DbUtil.GetCursor("sp_recursos_fortia_reportes_loadDatos",
                new SqlParameter("@opcion", opcion)
                );

            grid_puestos.DataSource = sp_loadGrid1;
            grid_puestos.DataBind();

            if (grid_puestos.Rows.Count == 0) //Si no hay cambios de bajas
            {
                redirigir();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Reporte-" + tipoReporte + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grid_puestos.AllowPaging = false;
                grid_puestos.DataSource = sp_loadGrid1;
                grid_puestos.DataBind();

                grid_puestos.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grid_puestos.HeaderRow.Cells)
                {
                    cell.BackColor = grid_puestos.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_puestos.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_puestos.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_puestos.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_puestos.RenderControl(hw);

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
                Response.Flush();
                Response.End();

            }
        }
    }
}