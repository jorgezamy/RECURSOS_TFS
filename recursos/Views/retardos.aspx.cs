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
    public partial class retardos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    //LoadMenuRoles();
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
                var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_retardos_loadDatos",
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
                rowFecha.Visible = true;
                btn_guardar.Visible = true;
                lblComentarios.Visible = true;
                txtComentarios.Visible = true;
            }
            else
            {
                rowNombre.Visible = false;
                rowDepartamento.Visible = false;
                rowPuesto.Visible = false;
                rowFecha.Visible = false;
                btn_guardar.Visible = false;
                lblComentarios.Visible = false;
                txtComentarios.Visible = false;
            }

        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            var sp_recursos_retardos_agregar = DbUtil.ExecuteProc("sp_recursos_retardos_agregar",
                new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                new SqlParameter("@dia_retardo", DateTime.Parse(txtFecha.Text)),
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