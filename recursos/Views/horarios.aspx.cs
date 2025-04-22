using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;

namespace recursos.Views
{
    public partial class horarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    loadChoferes();
                }
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
            }
        }

        private void loadChoferes()
        {
            var sp_loadChoferes = DbUtil.GetCursor("sp_recursos_horarios_cambioHorarios_loadEmpleados");

            drop_noEmpleado.DataSource = sp_loadChoferes;
            drop_noEmpleado.DataTextField = "nombre";
            drop_noEmpleado.DataValueField = "no_empleado";
            drop_noEmpleado.DataBind();
        }

        protected void drop_noEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_noEmpleado.SelectedValue != "")
            {
                var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_loadDatos",
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@e_dom", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@s_dom", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@horarios_dom", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@activo_dom", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@e_lun", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@s_lun", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@horarios_lun", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@activo_lun", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@e_mar", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@s_mar", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@horarios_mar", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@activo_mar", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@e_mie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@s_mie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@horarios_mie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@activo_mie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@e_jue", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@s_jue", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@horarios_jue", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@activo_jue", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@e_vie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@s_vie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@horarios_vie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@activo_vie", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@e_sab", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@s_sab", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@horarios_sab", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@activo_sab", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                lbl_nombre.Text = sp_loadDatos["@nombre"].ToString();
                lbl_depto.Text = sp_loadDatos["@departamento"].ToString();
                lbl_puesto.Text = sp_loadDatos["@puesto"].ToString();

                foreach (ListItem item in chbx_dom.Items)
                {
                    if (item.Value == sp_loadDatos["@horarios_dom"].ToString())
                    {
                        if (sp_loadDatos["@activo_dom"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }

                tb_horarioDom_entrada.Text = sp_loadDatos["@e_dom"].ToString();
                tb_horarioDom_salida.Text = sp_loadDatos["@s_dom"].ToString();

                foreach (ListItem item in chbx_lun.Items)
                {
                    if (item.Value == sp_loadDatos["@horarios_lun"].ToString())
                    {
                        if (sp_loadDatos["@activo_lun"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }

                tb_horarioLun_entrada.Text = sp_loadDatos["@e_lun"].ToString();
                tb_horarioLun_salida.Text = sp_loadDatos["@s_lun"].ToString();

                foreach (ListItem item in chbx_mar.Items)
                {
                    if (item.Value == sp_loadDatos["@horarios_mar"].ToString())
                    {
                        if (sp_loadDatos["@activo_mar"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }

                tb_horarioMar_entrada.Text = sp_loadDatos["@e_mar"].ToString();
                tb_horarioMar_salida.Text = sp_loadDatos["@s_mar"].ToString();

                foreach (ListItem item in chbx_mie.Items)
                {
                    if (item.Value == sp_loadDatos["@horarios_mie"].ToString())
                    {
                        if (sp_loadDatos["@activo_mie"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }

                tb_horarioMie_entrada.Text = sp_loadDatos["@e_mie"].ToString();
                tb_horarioMie_salida.Text = sp_loadDatos["@s_mie"].ToString();

                foreach (ListItem item in chbx_jue.Items)
                {
                    if (item.Value == sp_loadDatos["@horarios_jue"].ToString())
                    {
                        if (sp_loadDatos["@activo_jue"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }

                tb_horarioJue_entrada.Text = sp_loadDatos["@e_jue"].ToString();
                tb_horarioJue_salida.Text = sp_loadDatos["@s_jue"].ToString();

                foreach (ListItem item in chbx_vie.Items)
                {
                    if (item.Value == sp_loadDatos["@horarios_vie"].ToString())
                    {
                        if (sp_loadDatos["@activo_vie"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }

                tb_horarioVie_entrada.Text = sp_loadDatos["@e_vie"].ToString();
                tb_horarioVie_salida.Text = sp_loadDatos["@s_vie"].ToString();

                foreach (ListItem item in chbx_sab.Items)
                {
                    if (item.Value == sp_loadDatos["@horarios_sab"].ToString())
                    {
                        if (sp_loadDatos["@activo_sab"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }

                tb_horarioSab_entrada.Text = sp_loadDatos["@e_sab"].ToString();
                tb_horarioSab_salida.Text = sp_loadDatos["@s_sab"].ToString();
            }
            else
            {
                lbl_nombre.Text = "";
                lbl_depto.Text = "";
                lbl_puesto.Text = "";
            }
        }

        private void load_horaSalida()
        {
            var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_horaSalida",
                new SqlParameter("@no_empleado", drop_noEmpleado.Text),
                new SqlParameter("@hora_entrada", Session["horaEntrada"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@hora_salida", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            Session.Add("horaSalida", sp_horaSalida["@hora_salida"].ToString());
        }

        protected void tb_horarioLun_entrada_TextChanged(object sender, EventArgs e)
        {
            Session.Add("horaEntrada", tb_horarioLun_entrada.Text);
            load_horaSalida();
            tb_horarioLun_salida.Text = Session["horaSalida"].ToString();
            Session.Remove("horaEntrada");
            Session.Remove("horaSalida");
        }

        protected void tb_horarioMar_entrada_TextChanged(object sender, EventArgs e)
        {
            Session.Add("horaEntrada", tb_horarioMar_entrada.Text);
            load_horaSalida();
            tb_horarioMar_salida.Text = Session["horaSalida"].ToString();
            Session.Remove("horaEntrada");
            Session.Remove("horaSalida");
        }

        protected void tb_horarioMie_entrada_TextChanged(object sender, EventArgs e)
        {
            Session.Add("horaEntrada", tb_horarioMie_entrada.Text);
            load_horaSalida();
            tb_horarioMie_salida.Text = Session["horaSalida"].ToString();
            Session.Remove("horaEntrada");
            Session.Remove("horaSalida");
        }

        protected void tb_horarioJue_entrada_TextChanged(object sender, EventArgs e)
        {
            Session.Add("horaEntrada", tb_horarioJue_entrada.Text);
            load_horaSalida();
            tb_horarioJue_salida.Text = Session["horaSalida"].ToString();
            Session.Remove("horaEntrada");
            Session.Remove("horaSalida");
        }

        protected void tb_horarioVie_entrada_TextChanged(object sender, EventArgs e)
        {
            Session.Add("horaEntrada", tb_horarioVie_entrada.Text);
            load_horaSalida();
            tb_horarioVie_salida.Text = Session["horaSalida"].ToString();
            Session.Remove("horaEntrada");
            Session.Remove("horaSalida");
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            var activo = "";

            foreach (ListItem item in chbx_dom.Items)
            {
                if (item.Selected)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }

                var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_insertDatos",
                    new SqlParameter("@hora_entrada", tb_horarioDom_entrada.Text),
                    new SqlParameter("@hora_salida", tb_horarioDom_salida.Text),
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    new SqlParameter("@id_dias", item.Value),
                    new SqlParameter("@activo", activo)
                    );
            }

            foreach (ListItem item in chbx_lun.Items)
            {
                if (item.Selected)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }

                var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_insertDatos",
                    new SqlParameter("@hora_entrada", tb_horarioLun_entrada.Text),
                    new SqlParameter("@hora_salida", tb_horarioLun_salida.Text),
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    new SqlParameter("@id_dias", item.Value),
                    new SqlParameter("@activo", activo)
                    );
            }

            foreach (ListItem item in chbx_mar.Items)
            {
                if (item.Selected)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }

                var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_insertDatos",
                    new SqlParameter("@hora_entrada", tb_horarioMar_entrada.Text),
                    new SqlParameter("@hora_salida", tb_horarioMar_salida.Text),
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    new SqlParameter("@id_dias", item.Value),
                    new SqlParameter("@activo", activo)
                    );
            }

            foreach (ListItem item in chbx_mie.Items)
            {
                if (item.Selected)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }

                var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_insertDatos",
                    new SqlParameter("@hora_entrada", tb_horarioMie_entrada.Text),
                    new SqlParameter("@hora_salida", tb_horarioMie_salida.Text),
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    new SqlParameter("@id_dias", item.Value),
                    new SqlParameter("@activo", activo)
                    );
            }

            foreach (ListItem item in chbx_jue.Items)
            {
                if (item.Selected)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }

                var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_insertDatos",
                    new SqlParameter("@hora_entrada", tb_horarioJue_entrada.Text),
                    new SqlParameter("@hora_salida", tb_horarioJue_salida.Text),
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    new SqlParameter("@id_dias", item.Value),
                    new SqlParameter("@activo", activo)
                    );
            }

            foreach (ListItem item in chbx_vie.Items)
            {
                if (item.Selected)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }

                var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_insertDatos",
                    new SqlParameter("@hora_entrada", tb_horarioVie_entrada.Text),
                    new SqlParameter("@hora_salida", tb_horarioVie_salida.Text),
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    new SqlParameter("@id_dias", item.Value),
                    new SqlParameter("@activo", activo)
                    );
            }

            foreach (ListItem item in chbx_sab.Items)
            {
                if (item.Selected)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }

                var sp_horaSalida = DbUtil.ExecuteProc("sp_recursos_horarios_cambioHorarios_insertDatos",
                    new SqlParameter("@hora_entrada", tb_horarioSab_entrada.Text),
                    new SqlParameter("@hora_salida", tb_horarioSab_salida.Text),
                    new SqlParameter("@no_empleado", drop_noEmpleado.SelectedValue),
                    new SqlParameter("@id_dias", item.Value),
                    new SqlParameter("@activo", activo)
                    );

                //ScriptManager.RegisterStartupScript(UpdatePanel_horarios, this.GetType(), "CallMyFunction", "ShowLabel(); hide(); popup();", true);
            }
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}