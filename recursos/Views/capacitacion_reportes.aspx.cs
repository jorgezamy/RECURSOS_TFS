using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MsBarco;

namespace recursos.Views
{
    public partial class capacitacion_reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadMenuRoles();
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

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_capacitacion_reportes_roles_permitirAcceso",
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

        protected void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("capacitacion.aspx");
        }

        protected void tab1_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 0;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;
            chkFechasPlanAnual.Checked = false;

            tab1.CssClass = "clicked";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
        }

        protected void tab2_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 1;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;
            chkFechasCurso.Checked = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "clicked";
            tab3.CssClass = "initial";
        }

        protected void tab3_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 2;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;
            //chkFechasEmpleado.Text = "Sólo " + Convert.ToDouble(DateTime.Now.Year.ToString());
            //chkFechasEmpleado.Checked = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "clicked";
        }

        //void loadReporte(string opcion, string curso, string empleado, string fechaInicio, string fechaFin)
        //{
        //    var sp_reporte = DbUtil.GetCursor("sp_recursos_reportes_loadDatos",
        //        new SqlParameter("@opcion", opcion),
        //        new SqlParameter("@curso", !string.IsNullOrEmpty(curso) ? curso : (object)DBNull.Value),
        //        new SqlParameter("@empleado", !string.IsNullOrEmpty(empleado) ? empleado : (object)DBNull.Value),
        //        new SqlParameter("@fechaInicio", !string.IsNullOrEmpty(fechaInicio) ? fechaInicio : (object)DBNull.Value),
        //        new SqlParameter("@fechaFin", !string.IsNullOrEmpty(fechaFin) ? fechaFin : (object)DBNull.Value));

        //    gridview_reporte.DataSource = sp_reporte;
        //    gridview_reporte.DataBind();

        //}

        //void BindGrid()
        //{
        //    string fechaIniEmpleado = Request.Form["datepickerIni"];
        //    string fechaFinEmpleado = Request.Form["datepickerFin"];

        //    if (multi_view.ActiveViewIndex == 0)
        //    {
        //        //var sp_reporte = DbUtil.GetCursor("sp_recursos_capacitacion_reportes_loadReportes",
        //        //    new SqlParameter("@opcion", 1),
        //        //    new SqlParameter("@curso", (object)DBNull.Value),
        //        //    new SqlParameter("@empleado", ddlEmpleados.SelectedValue),
        //        //    new SqlParameter("@fechaInicio", !string.IsNullOrEmpty(fechaIniEmpleado) ? fechaIniEmpleado : (object)DBNull.Value),
        //        //    new SqlParameter("@fechaFin", !string.IsNullOrEmpty(fechaFinEmpleado) ? fechaFinEmpleado : (object)DBNull.Value)
        //        //    );

        //        //gridview_reporte.DataSource = sp_reporte;
        //        //gridview_reporte.DataBind();
        //        loadReporte("1", null, ddlEmpleados.SelectedValue, fechaIniEmpleado, fechaFinEmpleado);
        //        chkFechasPlanAnual.Checked = false;
        //    }

        //    if (multi_view.ActiveViewIndex == 1)
        //    {
        //        //var sp_reporte = DbUtil.GetCursor("sp_recursos_capacitacion_reportes_loadReportes",
        //        //    new SqlParameter("@opcion", 2),
        //        //    new SqlParameter("@curso", ddlCurso.SelectedValue),
        //        //    new SqlParameter("@empleado", (object)DBNull.Value),
        //        //    new SqlParameter("@fechaInicio", !string.IsNullOrEmpty(tb_fecha_inicio_curso.Text) ? tb_fecha_inicio_curso.Text : (object)DBNull.Value),
        //        //    new SqlParameter("@fechaFin", !string.IsNullOrEmpty(tb_fecha_fin_curso.Text) ? tb_fecha_fin_curso.Text : (object)DBNull.Value)
        //        //    );

        //        //gridview_reporte.DataSource = sp_reporte;
        //        //gridview_reporte.DataBind();
        //        loadReporte("2", ddlCurso.SelectedValue, null, tb_fecha_inicio_curso.Text, tb_fecha_fin_curso.Text);
        //        chkFechasCurso.Checked = false;
        //    }

        //    if (multi_view.ActiveViewIndex == 2)
        //    {
        //        //var sp_reporte = DbUtil.GetCursor("sp_recursos_capacitacion_reportes_loadReportes",
        //        //    new SqlParameter("@opcion", 3),
        //        //    new SqlParameter("@curso", (object)DBNull.Value),
        //        //    new SqlParameter("@empleado", !string.IsNullOrEmpty(ddlEmpleados.SelectedValue) ? ddlEmpleados.SelectedValue : (object)DBNull.Value),
        //        //    new SqlParameter("@fechaInicio", !string.IsNullOrEmpty(fechaIniEmpleado) ? fechaIniEmpleado : (object)DBNull.Value),
        //        //    new SqlParameter("@fechaFin", !string.IsNullOrEmpty(fechaFinEmpleado) ? fechaFinEmpleado : (object)DBNull.Value)
        //        //    );

        //        //gridview_reporte.DataSource = sp_reporte;
        //        //gridview_reporte.DataBind();
        //        loadReporte("3", null, ddlEmpleados.SelectedValue, fechaIniEmpleado, fechaFinEmpleado);
        //        chkFechasEmpleado.Checked = false;
        //    }
        //}

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            gridview_reporte.Visible = true;
            //BindGrid();
            var inicio = tb_fecha_inicio_curso.Text;
            var fin = tb_fecha_fin_curso.Text;
            string fechaIniEmpleado = Request.Form["datepickerIni"];
            string fechaFinEmpleado = Request.Form["datepickerFin"];

            if (multi_view.ActiveViewIndex == 0)
            {
                var sp_reporte = DbUtil.GetCursor("sp_recursos_capacitacion_reportes_loadReportes",
                    new SqlParameter("@opcion", 1),
                    new SqlParameter("@curso", (object)DBNull.Value),
                    new SqlParameter("@empleado", ddlEmpleados.SelectedValue),
                    new SqlParameter("@asistencia", ddlAsistencia.SelectedValue),
                    new SqlParameter("@fechaInicio", !string.IsNullOrEmpty(fechaIniEmpleado) ? fechaIniEmpleado : (object)DBNull.Value),
                    new SqlParameter("@fechaFin", !string.IsNullOrEmpty(fechaFinEmpleado) ? fechaFinEmpleado : (object)DBNull.Value)
                    );

                gridview_reporte.DataSource = sp_reporte;
                gridview_reporte.DataBind();

                chkFechasPlanAnual.Checked = false;
            }

            if (multi_view.ActiveViewIndex == 1)
            {
                var sp_reporte = DbUtil.GetCursor("sp_recursos_capacitacion_reportes_loadReportes",
                    new SqlParameter("@opcion", 2),
                    new SqlParameter("@curso", ddlCurso.SelectedValue),
                    new SqlParameter("@empleado", (object)DBNull.Value),
                    new SqlParameter("@asistencia", ddlAsistencia.SelectedValue),
                    new SqlParameter("@fechaInicio", !string.IsNullOrEmpty(tb_fecha_inicio_curso.Text) ? tb_fecha_inicio_curso.Text : (object)DBNull.Value),
                    new SqlParameter("@fechaFin", !string.IsNullOrEmpty(tb_fecha_fin_curso.Text) ? tb_fecha_fin_curso.Text : (object)DBNull.Value)
                    );

                gridview_reporte.DataSource = sp_reporte;
                gridview_reporte.DataBind();

                chkFechasCurso.Checked = false;
            }

            if (multi_view.ActiveViewIndex == 2)
            {
                var sp_reporte = DbUtil.GetCursor("sp_recursos_capacitacion_reportes_loadReportes",
                    new SqlParameter("@opcion", 3),
                    new SqlParameter("@curso", (object)DBNull.Value),
                    new SqlParameter("@empleado", !string.IsNullOrEmpty(ddlEmpleados.SelectedValue) ? ddlEmpleados.SelectedValue : (object)DBNull.Value),
                    new SqlParameter("@asistencia", !string.IsNullOrEmpty(ddlAsistencia.SelectedValue) ? ddlAsistencia.SelectedValue : (object)DBNull.Value),
                    new SqlParameter("@fechaInicio", !string.IsNullOrEmpty(fechaIniEmpleado) ? fechaIniEmpleado : (object)DBNull.Value),
                    new SqlParameter("@fechaFin", !string.IsNullOrEmpty(fechaFinEmpleado) ? fechaFinEmpleado : (object)DBNull.Value)
                    );

                gridview_reporte.DataSource = sp_reporte;
                gridview_reporte.DataBind();

                //chkFechasEmpleado.Checked = false;
            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void ExportToExcel(object sender, EventArgs e)
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
                gridview_reporte.AllowPaging = false;
                //this.BindGrid();

                gridview_reporte.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gridview_reporte.HeaderRow.Cells)
                {
                    cell.BackColor = gridview_reporte.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gridview_reporte.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gridview_reporte.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gridview_reporte.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gridview_reporte.RenderControl(hw);

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

        protected void gridview_reporte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int rowCount = gridview_reporte.Rows.Count;

            if (rowCount == 0)
            {
                btn_descargar.Visible = false;
            }
            else
            {
                btn_descargar.Visible = true;
            }
        }

        protected void gridview_reporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview_reporte.PageIndex = e.NewPageIndex;
            //BindGrid();
        }
    }
}