﻿using System;
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
    public partial class fortia_hrsExtra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            reportes_load_hrsExtra(8, "Horas extra");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void redirigir()
        {
            Response.Redirect("fortia_incapacidades.aspx?opcion=" + Request.QueryString["opcion"].ToString());

            //Response.Redirect("fortia_modificaciones.aspx");
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        private void reportes_load_hrsExtra(int opcion, string tipoReporte)
        {
            var sp_loadGrid1 = DbUtil.GetCursor("sp_recursos_fortia_reportes_loadDatos",
                new SqlParameter("@opcion", opcion)
                );

            grid_hrsExtra.DataSource = sp_loadGrid1;
            grid_hrsExtra.DataBind();

            if (grid_hrsExtra.Rows.Count == 0) //Si no hay cambios de bajas
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
                grid_hrsExtra.AllowPaging = false;
                grid_hrsExtra.DataSource = sp_loadGrid1;
                grid_hrsExtra.DataBind();

                grid_hrsExtra.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grid_hrsExtra.HeaderRow.Cells)
                {
                    cell.BackColor = grid_hrsExtra.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_hrsExtra.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_hrsExtra.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_hrsExtra.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_hrsExtra.RenderControl(hw);

                if (!Directory.Exists("/GCDM/fortiaReportes/" + tipoReporte))
                {
                    Directory.CreateDirectory("/GCDM/fortiaReportes/" + tipoReporte);
                }

                File.WriteAllText("/GCDM/fortiaReportes/" + tipoReporte + "/" + tipoReporte + ".xls", sw.ToString());
                uploadFile.sftp.UploadSFTPFile("/GCDM/fortiaReportes/" + tipoReporte + "/" + tipoReporte + "" + ".xls");

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