using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.IO;
using System.Drawing;
using recursos.Content;

namespace recursos.Views.Fortia
{
    public partial class fortia_movimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = "";

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                usuario = Session["usuario"].ToString();
            }
            else
            {
                usuario = "server";
            }

            var sp_nomina = DbUtil.ExecuteProc("sp_recursos_fortia_movimientos_nomina",
                new SqlParameter("@usuario", usuario),
                MsBarco.DbUtil.NewSqlParam("@existe", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );




            if (sp_nomina["@existe"].ToString() == "0")
            {
                //Crear gridview
                //var sp_subtotalesXdia = DbUtil.ExecuteProc("sp_recursos_fortia_movimientos_subtotalesXdia",
                //    new SqlParameter("@finiquito", "0"),
                //    new SqlParameter("@noEmpleado", "")
                //    );

                //var sp_horasXdia = DbUtil.ExecuteProc("sp_recursos_fortia_movimientos_horasXdia",
                //    new SqlParameter("@finiquito", "0"),
                //    new SqlParameter("@noEmpleado", "")
                //    );

                var sp_totalesHibrido = DbUtil.ExecuteProc("sp_recursos_fortia_movimientos_preciosTotales",
                    new SqlParameter("@finiquito", "0"),
                    new SqlParameter("@noEmpleado", "")
                    );
            }


            Generar();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        //            var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_fortia_movimientos_loadDatos");

        private void redirigir()
        {
            Response.Redirect("fortia_puestos.aspx?opcion=" + Request.QueryString["opcion"].ToString());

            //Response.Redirect("fortia_modificaciones.aspx");
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        private void Generar()
        {
            var sp_loadDatos = DbUtil.GetCursor("sp_recursos_fortia_movimientos_loadDatos");

            //var sp_loadDatos = DbUtil.GetCursor("sp_recursos_fortia_reportes_loadDatos",
            //   new SqlParameter("@opcion", 1)
            //   );

            grid_movimientos.DataSource = sp_loadDatos;
            grid_movimientos.DataBind();

            if (grid_movimientos.Rows.Count == 0) //Si no hay cambios
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
                grid_movimientos.AllowPaging = false;
                grid_movimientos.DataSource = sp_loadDatos;
                grid_movimientos.DataBind();

                grid_movimientos.HeaderRow.BackColor = Color.White;

                foreach (TableCell cell in grid_movimientos.HeaderRow.Cells)
                {
                    cell.BackColor = grid_movimientos.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_movimientos.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_movimientos.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_movimientos.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_movimientos.RenderControl(hw);

                if (!Directory.Exists("/GCDM/fortiaReportes/Movimientos" ))
                {
                    Directory.CreateDirectory("/GCDM/fortiaReportes/Movimientos" );
                }

                File.WriteAllText("/GCDM/fortiaReportes/" + "Movimientos" + "/" + "Movimientos" + ".xls", sw.ToString());
                uploadFile.sftp.UploadSFTPFile("/GCDM/fortiaReportes/" + "Movimientos" + "/" + "Movimientos" + "" + ".xls");
                //if (Request.QueryString["opcion"].ToString() != "0")

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
                //redirigir();

            }
        }


    }
}