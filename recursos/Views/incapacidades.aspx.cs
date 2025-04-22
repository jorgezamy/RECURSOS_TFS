using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using MsBarco;

namespace recursos.Views
{
    public partial class incapacidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        private void loadControlIncapacidad()
        {
            var loadddlControl = DbUtil.GetCursor("sp_recursos_incapacidades_ddlControl",
                                 new SqlParameter("@causa", ddlCausa.SelectedValue),
                                 new SqlParameter("@secuela", ddlSecuela.SelectedValue)
            );

            ddlControl.Items.Clear();
            ddlControl.Items.Add(new ListItem("-- Seleccionar --", ""));

            ddlControl.DataSource = loadddlControl;
            ddlControl.DataTextField = "descripcion";
            ddlControl.DataValueField = "id_control_incapacidad";
            ddlControl.DataBind();
        }
        
        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpleado.SelectedValue != "")
            {
                var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_incapacidades_loadDatos",
                    new SqlParameter("@empleado", ddlEmpleado.SelectedValue),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                    MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                    MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
                    );

                lblNombre.Text = sp_loadDatos["@nombre"].ToString();
                lblDepartamento.Text = sp_loadDatos["@departamento"].ToString();
                lblPuesto.Text = sp_loadDatos["@puesto"].ToString();

                rowNombre.Visible = true;
                rowDepartamento.Visible = true;
                rowPuesto.Visible = true;
                //no_serie.Visible = true;
                rowFecha.Visible = true;
                rowDias.Visible = true;
                rowFechaFin.Visible = true;

                rowFolio.Visible = true;
                rowCausa.Visible = true;
                ddlCausa.SelectedValue = "";
                rowInicial.Visible = true;
                rowComentarios.Visible = true;

                btn_guardar.Visible = true;
                //lblMotivo.Visible = true;
                //ddlMotivo.Visible = true;
                //lblComentarios.Visible = true;
                //txtComentarios.Visible = true;
            }
            else
            {
                rowNombre.Visible = false;
                rowDepartamento.Visible = false;
                rowPuesto.Visible = false;
                //no_serie.Visible = false;
                rowFecha.Visible = false;
                rowDias.Visible = false;
                rowFechaFin.Visible = false;

                rowFolio.Visible = false;
                rowCausa.Visible = false;
                rowRiesgo.Visible = false;
                rowSecuela.Visible = false;
                rowControl.Visible = false;
                rowInicial.Visible = false;
                rowComentarios.Visible = false;

                btn_guardar.Visible = false;
                //lblMotivo.Visible = false;
                //ddlMotivo.Visible = false;
                lblComentarios.Visible = false;
                txtComentarios.Visible = false;
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            var sp_recursos_incapacidades_agregar = DbUtil.ExecuteProc("sp_recursos_incapacidades_agregar",
                new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                new SqlParameter("@folio", tb_folio.Text),                
                new SqlParameter("@dia_ini", txtFechaInicio.Text),
                new SqlParameter("@dia_fin", txtFechaFin.Text),
                new SqlParameter("@dias_incapacidad", tbDias.Text),
                new SqlParameter("@causa_incapacidad", ddlCausa.SelectedValue),
                new SqlParameter("@tipo_incapacidad", ddlRiesgo.SelectedValue),
                new SqlParameter("@secuela_incapacidad", ddlSecuela.SelectedValue),
                new SqlParameter("@control_incapacidad", ddlControl.SelectedValue),
                new SqlParameter("@incapacidad_inicial", RBLInicial.SelectedValue),
                new SqlParameter("@comentarios", txtComentarios.Text)
                );

            Response.Redirect("confirmacion_registro.aspx");
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text!="")
            {
                tbDias.Enabled = true;
                tbDias.Text = "";

                tbDias.Focus();
            }
            else
            {
                tbDias.Enabled = false;
                tbDias.Text = "";
            }
        }

        protected void tbDias_TextChanged(object sender, EventArgs e)
        {
            DateTime inicio = DateTime.Parse(txtFechaInicio.Text);
            DateTime fin = inicio.AddDays(Int32.Parse(tbDias.Text)-1);
            txtFechaFin.Text = fin.ToString("yyyy-MM-dd");
            txtComentarios.Focus();
        }

        protected void ddlCausa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCausa.SelectedValue != "")
            {
                loadControlIncapacidad();

                if (ddlCausa.SelectedValue == "2") //Riesgo
                {
                    rowRiesgo.Visible = true;
                    rowSecuela.Visible = false;
                    rowControl.Visible = false;
                }

                else if (ddlCausa.SelectedValue == "4") //Maternidad
                {
                    rowRiesgo.Visible = false;
                    rowSecuela.Visible = false;
                    rowControl.Visible = true;
                }

                else //EG
                {
                    rowRiesgo.Visible = false;
                    rowSecuela.Visible = false;
                    rowControl.Visible = true;
                }
            }

            else
            {
                rowRiesgo.Visible = false;
                rowSecuela.Visible = false;
                rowControl.Visible = false;
            }

        }

        protected void ddlRiesgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRiesgo.SelectedValue != "")
            {
                rowSecuela.Visible = true;
            }

            else
            {
                rowSecuela.Visible = false;
                rowControl.Visible = false;
            }
        }

        protected void ddlSecuela_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSecuela.SelectedValue != "")
            {
                loadControlIncapacidad();
                rowControl.Visible = true;
            }

            else
            {
                rowControl.Visible = false;
            }
        }
    }
}