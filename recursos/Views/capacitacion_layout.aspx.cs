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
using System.Linq;
using System.Web.UI.WebControls;


namespace recursos.Views
{
    public partial class capacitacion_layout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadMenuRoles();

                    loadCursos();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        protected void LoadMenuRoles()
        {
            var usuario = Session["usuario"].ToString();

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_capacitacion_layout_roles_permitirAcceso",
               new SqlParameter("@usuario", usuario),
               MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@cadenaAccesos_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
               );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                a.Value = sp_loadRol["@cadenaAccesos_modulo"].ToString();
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("~/views/capacitacion.aspx");
            }
        }

        private void loadCursos()
        {
            var sp_loadCursos = DbUtil.GetCursor("sp_recursos_capacitacion_layout_loadCursos");

            drop_curso.DataSource = sp_loadCursos;
            drop_curso.DataValueField = "idCurso";
            drop_curso.DataTextField = "descripcion";
            drop_curso.DataBind();
        }

        private void loadCargaMasiva()
        {
            var sp_loadCargaMasiva = DbUtil.GetCursor("sp_recursos_capacitacion_layout_loadCargaMasiva",
                new SqlParameter("@idCurso", drop_curso.SelectedValue),
                new SqlParameter("@fecha_inicio", tb_fecha_inicial.Text),
                new SqlParameter("@fecha_fin", tb_fecha_final.Text)
                );

            grid_cargaMasiva.DataSource = sp_loadCargaMasiva;
            grid_cargaMasiva.DataBind();
        }
        
        protected void btnbuscar_Click(object sender, ImageClickEventArgs e)
        {
            loadCargaMasiva();
        }

        protected void grid_cargaMasiva_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_cargaMasiva.PageIndex = e.NewPageIndex;
            loadCargaMasiva();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }
        
        protected void btn_descargar_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grid_cargaMasiva.AllowPaging = false;
                this.loadCargaMasiva();

                grid_cargaMasiva.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in grid_cargaMasiva.HeaderRow.Cells)
                {
                    cell.BackColor = grid_cargaMasiva.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_cargaMasiva.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_cargaMasiva.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_cargaMasiva.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_cargaMasiva.RenderControl(hw);

                //File.WriteAllText("/GCDM/fortiaReportes/Reporte" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", sw.ToString());
                //uploadFile.sftp.UploadSFTPFile("/GCDM/fortiaReportes/Reporte" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");


                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("capacitacion.aspx");
        }
    }
}