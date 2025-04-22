using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;

namespace recursos.Views
{
    public partial class capacitacion_cursos_planAnual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadMenuRoles();

                    loadAnio();
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

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_planAnual_roles_permitirAcceso",
               new SqlParameter("@usuario", usuario),
               MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@cadenaAccesos_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
               );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                //a.Value = sp_loadRol["@cadenaAccesos_modulo"].ToString();
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("~/views/capacitacion_cursos.aspx");
            }
        }

        private void loadAnio()
        {
            var year = DateTime.Now.Year.ToString();

            int anio = int.Parse(year);

            for (int i = anio; i <= anio + 1; i++)
            {
                drop_planAnual_anio.Items.Add(Convert.ToString(i));
            }
        }

        protected void drop_planAnual_anio_SelectedIndexChanged(object sender, EventArgs e)
        {
            checklist_cursosActivos.Items.Clear();

            if (drop_planAnual_anio.SelectedValue != "")
            {
                loadCursos();
                loadCursosActivosHorarios();
                loadCamposCurso();

                updateCierre(0);

                btn_cursoNuevo_guardar.Visible = true;
                btn_cursosActivos.Visible = true;
                drop_cursoFechas_curso.Enabled = true;

                btn_planAnual_cierre.Visible = true;
            }
            else
            {
                btn_cursoNuevo_guardar.Visible = false;
                btn_cursosActivos.Visible = false;
                drop_cursoFechas_curso.Enabled = false;

                btn_planAnual_cierre.Visible = false;
            }

            lbl_cursoNuevo_mensaje.Text = "";
            lbl_cursoFechas_mensaje.Text = "";
            mensaje_error.Text = "";

            drop_cursoFechas_curso.SelectedValue = "";

            drop_cursoFechas_inicio.SelectedValue = "";
            drop_cursoFechas_inicio.Enabled = false;
            drop_cursoFechas_fin.SelectedValue = "";
            drop_cursoFechas_fin.Enabled = false;
            grid_planAnual_horarios.Visible = false;
        }

        private void loadCamposCurso()
        {
            drop_cursoNuevo_modalidad.Items.Clear();

            var sp_loadDatos = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_planAnual_loadDatosCurso",
                new SqlParameter("@parte", "1")
                );

            drop_cursoNuevo_modalidad.Items.Add(new ListItem("-- Seleccionar --", ""));

            drop_cursoNuevo_modalidad.DataSource = sp_loadDatos;
            drop_cursoNuevo_modalidad.DataValueField = "idModalidades";
            drop_cursoNuevo_modalidad.DataTextField = "descripcion";
            drop_cursoNuevo_modalidad.DataBind();


            drop_cursoNuevo_objetivo.Items.Clear();

            var sp_loadDatos2 = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_planAnual_loadDatosCurso",
                new SqlParameter("@parte", "2")
                );

            drop_cursoNuevo_objetivo.Items.Add(new ListItem("-- Seleccionar --", ""));
            
            drop_cursoNuevo_objetivo.DataSource = sp_loadDatos2;
            drop_cursoNuevo_objetivo.DataValueField = "idObjetivos";
            drop_cursoNuevo_objetivo.DataTextField = "descripcion";
            drop_cursoNuevo_objetivo.DataBind();


            drop_cursoNuevo_areaTematica.Items.Clear();

            var sp_loadDatos3 = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_planAnual_loadDatosCurso",
                new SqlParameter("@parte", "3")
                );

            drop_cursoNuevo_areaTematica.Items.Add(new ListItem("-- Seleccionar --", ""));
            
            drop_cursoNuevo_areaTematica.DataSource = sp_loadDatos3;
            drop_cursoNuevo_areaTematica.DataValueField = "idAreasTematicas";
            drop_cursoNuevo_areaTematica.DataTextField = "descripcion";
            drop_cursoNuevo_areaTematica.DataBind();
        }

        protected void btn_cursoNuevo_guardar_Click(object sender, EventArgs e)
        {
            var sp_insertCurso = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_planAnual_insertNuevoCurso",
                new SqlParameter("@id_compania", Session["companiaID"].ToString()),
                new SqlParameter("@descripcion", txt_cursoNuevo_descripcion.Text),
                new SqlParameter("@id_capacitacion_modalidades", drop_cursoNuevo_modalidad.SelectedValue),
                new SqlParameter("@objetivos_internos", drop_cursoNuevo_objetivo.SelectedValue),
                new SqlParameter("@id_capacitacion_objetivos", drop_cursoNuevo_objetivo.SelectedValue),
                new SqlParameter("@duracion", txt_cursoNuevo_duracion.Text),
                new SqlParameter("@id_capacitacion_areasTematicas", drop_cursoNuevo_areaTematica.SelectedValue),
                new SqlParameter("@usuario", Session["usuario"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@existe", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_insertCurso["@existe"].ToString() == "0")
            {
                lbl_cursoNuevo_mensaje.Text = "Curso creado exitosamente.";
            }
            else
            {
                lbl_cursoNuevo_mensaje.Text = "Ya existe el curso.";
            }

            updateCierre(0);
            loadCursos();
        }

        private void loadCursos()
        {
            var sp_loadCursos = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_planAnual_loadCursos_plantilla",
                new SqlParameter("@id_compania", Session["companiaID"].ToString())
                );

            checklist_cursosActivos.DataSource = sp_loadCursos;
            checklist_cursosActivos.DataTextField = "descripcion";
            checklist_cursosActivos.DataValueField = "id_capacitacion_cursos";
            checklist_cursosActivos.DataBind();

            var sp_loadCursosSeleccionados = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_planAnual_loadCursos",
                new SqlParameter("@anio", drop_planAnual_anio.SelectedValue)
                );

            foreach (ListItem item in checklist_cursosActivos.Items)
            {
                if (sp_loadCursosSeleccionados.Rows.OfType<DataRow>().Select(dr => dr.Field<int>("id_capacitacion_cursos")).ToList().Contains(int.Parse(item.Value)))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }

        private void loadCursosActivosHorarios()
        {
            drop_cursoFechas_curso.Items.Clear();

            var sp_loadPuestos = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_planAnual_loadCursos",
                new SqlParameter("@anio", drop_planAnual_anio.SelectedValue)
                );

            drop_cursoFechas_curso.Items.Add(new ListItem("-- Seleccionar --", ""));

            drop_cursoFechas_curso.DataSource = sp_loadPuestos;
            drop_cursoFechas_curso.DataTextField = "descripcion";
            drop_cursoFechas_curso.DataValueField = "id_capacitacion_cursos";
            drop_cursoFechas_curso.DataBind();

        }

        //protected void grid_cursosActivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grid_cursosActivos.PageIndex = e.NewPageIndex;
        //    loadCursosActivos();
        //}

        protected void checklist_cursosActivos_Click(object sender, EventArgs e)
        {
            int valor;

            foreach (ListItem item in checklist_cursosActivos.Items)
            {
                if (item.Selected)
                {
                    valor = 1;
                }
                else
                {
                    valor = 0;
                }

                var sp_guardarCursosActivos = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_planAnual_insert",
                        new SqlParameter("@id_capacitacion_cursos", item.Value),
                        new SqlParameter("@anio", drop_planAnual_anio.SelectedValue),
                        new SqlParameter("@seleccionado", valor),
                        new SqlParameter("@usuario", Session["usuario"].ToString())
                    );
            }

            loadCursosActivosHorarios();
            updateCierre(0);
        }

        private void loadMes()
        {
            var month = DateTime.Now.Month.ToString();
            int mes = int.Parse(month);

            drop_cursoFechas_inicio.Items.Clear();
            drop_cursoFechas_inicio.Items.Add(new ListItem("-- Seleccionar --", ""));

            var year = DateTime.Now.Year.ToString();
            int anio = int.Parse(year);

            if (int.Parse(drop_planAnual_anio.SelectedValue) > anio)
            {
                mes = 1;
            }

            for (int i = mes; i <= 12; i++)
            {
                /*version en ingles*/
                //string monthName = new DateTime(anio, i, 1).ToString("MMM", CultureInfo.InvariantCulture);
                string monthName = new DateTime(int.Parse(drop_planAnual_anio.SelectedValue), i, 1).ToString("MMM", CultureInfo.CreateSpecificCulture("es"));
                drop_cursoFechas_inicio.Items.Add(new ListItem(Convert.ToString(monthName), Convert.ToString(i)));
            }
        }

        protected void drop_cursoFechas_curso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_cursoFechas_curso.SelectedValue != "")
            {
                loadMes();
                drop_cursoFechas_inicio.Enabled = true;

                load_cursoHorarios();

                grid_planAnual_horarios.Visible = true;
            }
            else
            {
                drop_cursoFechas_inicio.SelectedValue = "";
                drop_cursoFechas_inicio.Enabled = false;

                drop_cursoFechas_fin.SelectedValue = "";
                drop_cursoFechas_fin.Enabled = false;

                grid_planAnual_horarios.Visible = false;
            }
        }

        protected void drop_cursoFechas_inicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_cursoFechas_inicio.SelectedValue != "")
            {
                drop_cursoFechas_fin.Enabled = true;

                var mes_fin = drop_cursoFechas_inicio.SelectedValue;
                var mes = int.Parse(mes_fin);

                drop_cursoFechas_fin.Items.Clear();
                drop_cursoFechas_fin.Items.Add(new ListItem("-- Seleccionar --", ""));

                for (int i = mes; i <= 12; i++)
                {
                    string monthName = new DateTime(int.Parse(drop_planAnual_anio.SelectedValue), i, 1).ToString("MMM", CultureInfo.CreateSpecificCulture("es"));
                    drop_cursoFechas_fin.Items.Add(new ListItem(Convert.ToString(monthName), Convert.ToString(i)));
                }
            }
            else
            {
                drop_cursoFechas_fin.Enabled = false;
                drop_cursoFechas_fin.SelectedValue = "";
            }
        }

        protected void btn_cursoFechas_agregar_Click(object sender, EventArgs e)
        {
            var sp_insertHorario = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_planAnual_insertCalendario",
                new SqlParameter("@id_capacitacion_cursos", drop_cursoFechas_curso.SelectedValue),
                new SqlParameter("@anio_inicio", drop_planAnual_anio.SelectedValue),
                new SqlParameter("@mes_inicio", drop_cursoFechas_inicio.SelectedValue),
                new SqlParameter("@dia_inicio", ""),
                new SqlParameter("@anio_fin", drop_planAnual_anio.SelectedValue),
                new SqlParameter("@mes_fin", drop_cursoFechas_fin.SelectedValue),
                new SqlParameter("@dia_fin", ""),
                new SqlParameter("@usuario", Session["usuario"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@existe", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
            );
            
            if (sp_insertHorario["@existe"].ToString().ToString() == "1")
            {
                lbl_cursoFechas_mensaje.Text = "Ya existe el registro.";
            }
            else
            {
                lbl_cursoFechas_mensaje.Text = "";
            }

            load_cursoHorarios();
            updateCierre(0);
        }

        private void load_cursoHorarios()
        {
            var sp_loadHorarios = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_planAnual_loadCalendario",
                new SqlParameter("@id_capacitacion_cursos", drop_cursoFechas_curso.SelectedValue),
                new SqlParameter("@anio", drop_planAnual_anio.SelectedValue)
            );

            grid_planAnual_horarios.DataSource = sp_loadHorarios;
            grid_planAnual_horarios.DataBind();
        }

        protected void grid_planAnual_horarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_planAnual_horarios.PageIndex = e.NewPageIndex;
            load_cursoHorarios();
        }

        protected void grid_planAnual_horarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //this.grid_planAnual_horarios.Columns[1].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grid_planAnual_horarios.HeaderRow.Cells[1].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void grid_planAnual_horarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = grid_planAnual_horarios.SelectedRow;
            TableCell id_curso = grid_planAnual_horarios.Rows[e.RowIndex].Cells[1]; //Obtener ID

            var sp_deleteHorarios = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_planAnual_deleteCalendario",
                new SqlParameter("@id_capacitacion_calendario", id_curso.Text)
            );

            load_cursoHorarios();
            updateCierre(0);
        }

        protected void btn_planAnual_cierre_Click(object sender, EventArgs e)
        {
            updateCierre(1);
            updateCierre(0);
        }

        private void updateCierre(int cierre)
        {
            var sp_updateCierre = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_planAnual_updateCierre",
                new SqlParameter("@anio", drop_planAnual_anio.SelectedValue),
                new SqlParameter("@cerrar", cierre),
                new SqlParameter("@usuario", Session["usuario"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@cierrePermitido", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@info", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                MsBarco.DbUtil.NewSqlParam("@color", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
                );
            
            mensaje_error.Text = sp_updateCierre["@info"].ToString();

            if (sp_updateCierre["@color"].ToString() == "1")
            {
                mensaje_error.CssClass = "cierre_green_lbl";
            }

            if (sp_updateCierre["@color"].ToString() == "2")
            {
                mensaje_error.CssClass = "cierre_red_lbl";
            }

            if (sp_updateCierre["@color"].ToString() == "3")
            {
                mensaje_error.CssClass = "cierre_yellow_lbl";
            }

            if (sp_updateCierre["@cierrePermitido"].ToString() == "0")
            {
                btn_planAnual_cierre.Text = "No Permitido";
                btn_planAnual_cierre.CssClass = "cierre_gray";
                btn_planAnual_cierre.Enabled = false;
            }
            else
            {
                btn_planAnual_cierre.Text = "Cerrar";
                btn_planAnual_cierre.CssClass = "cierre_green";
                btn_planAnual_cierre.Enabled = true;
            }
        }
        
        protected void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("capacitacion_cursos.aspx");
        }
    }
}