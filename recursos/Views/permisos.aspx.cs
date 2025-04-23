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
    public partial class permisos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();
                }
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
            }
        }
        
        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpleado.SelectedValue != "")
            {
                var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_permisos_loadDatos",
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
                
                rowRBList.Visible = true;

                rblist_horaOdias.SelectedIndex = -1;
            }
            else
            {
                rowRBList.Visible = false;

                rowNombre.Visible = false;
                rowDepartamento.Visible = false;
                rowPuesto.Visible = false;
            }

            rowFechaHora.Visible = false;

            rowFecha.Visible = false;
            rowCausa.Visible = false;
            lblComentarios.Visible = false;
            txtComentarios.Visible = false;

            btn_guardar.Visible = false;
        }

        protected void rblist_horaOdias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblist_horaOdias.SelectedValue != "")
            {
                if (rblist_horaOdias.SelectedValue == "1")
                {
                    rowFechaHora.Visible = true;

                    rowFecha.Visible = false;
                }
                else
                {
                    rowFechaHora.Visible = false;

                    rowFecha.Visible = true;

                }
                rowCausa.Visible = true;
                lblComentarios.Visible = true;
                txtComentarios.Visible = true;
                btn_guardar.Visible = true;

                txt_fecha_dia.Text = "";
                txt_fecha_hora_inicio.Text = "";
                txt_fecha_hora_fin.Text = "";

                txtFechaInicio.Text = "";
                txt_dias.Enabled = false;
                txt_dias.Text = "";
                txtFechaFin.Text = "";
                ddlCausa.SelectedValue = "";
                txtComentarios.Text = "";
            }
            else
            {
                btn_guardar.Visible = false;
                rowCausa.Visible = false;
                lblComentarios.Visible = false;
                txtComentarios.Visible = false;
            }
        }

        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text != "")
            {
                txt_dias.Enabled = true;
                txt_dias.Text = "";

                txt_dias.Focus();
            }
            else
            {
                txt_dias.Enabled = false;
                txt_dias.Text = "";
            }
        }

        protected void txt_dias_TextChanged(object sender, EventArgs e)
        {
            if (txt_dias.Text != "")
            {
                DateTime inicio = DateTime.Parse(txtFechaInicio.Text);
                DateTime fin = inicio.AddDays(Int32.Parse(txt_dias.Text) - 1);
                txtFechaFin.Text = fin.ToString("yyyy-MM-dd");
                txtComentarios.Focus();
            }

        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            var sp_recursos_permisos_agregar = DbUtil.ExecuteProc("sp_recursos_permisos_agregar",
                new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                new SqlParameter("@horaOfecha", rblist_horaOdias.SelectedValue),
                new SqlParameter("@dia", txt_fecha_dia.Text),
                new SqlParameter("@hora_inicio", txt_fecha_hora_inicio.Text),
                new SqlParameter("@hora_fin", txt_fecha_hora_fin.Text),
                new SqlParameter("@numero_dias", txt_dias.Text),
                new SqlParameter("@fecha_ini", txtFechaInicio.Text),
                new SqlParameter("@fecha_fin", txtFechaFin.Text),
                new SqlParameter("@causa", ddlCausa.SelectedValue),
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
    }
}