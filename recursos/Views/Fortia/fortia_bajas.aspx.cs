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
    public partial class fortia_bajas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            reportes_load_bajas(2, "Bajas");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void redirigir()
        {
            Response.Redirect("fortia_departamentos.aspx?opcion=" + Request.QueryString["opcion"].ToString());

            //Response.Redirect("fortia_modificaciones.aspx");
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        private void reportes_load_bajas(int opcion, string tipoReporte)
        {
            var sp_loadGrid1 = DbUtil.GetCursor("sp_recursos_fortia_reportes_loadDatos",
                new SqlParameter("@opcion", opcion)
                );

            grid_bajas.DataSource = sp_loadGrid1;
            grid_bajas.DataBind();

            if (grid_bajas.Rows.Count == 0) //Si no hay cambios de bajas
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
                grid_bajas.AllowPaging = false;
                grid_bajas.DataSource = sp_loadGrid1;
                grid_bajas.DataBind();

                grid_bajas.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grid_bajas.HeaderRow.Cells)
                {
                    cell.BackColor = grid_bajas.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_bajas.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_bajas.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_bajas.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_bajas.RenderControl(hw);

                if (!Directory.Exists("/GCDM/fortiaReportes/" + tipoReporte))
                {
                    Directory.CreateDirectory("/GCDM/fortiaReportes/" + tipoReporte);
                }

                File.WriteAllText("/GCDM/fortiaReportes/" + tipoReporte + "/" + tipoReporte + ".xls", sw.ToString());
                uploadFile.sftp.UploadSFTPFile("/GCDM/fortiaReportes/" + tipoReporte + "/" + tipoReporte  + ".xls");

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