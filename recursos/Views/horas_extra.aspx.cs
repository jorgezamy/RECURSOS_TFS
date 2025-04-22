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
using System.Drawing;


namespace recursos.Views
{
    public partial class horas_extra : System.Web.UI.Page
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

        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpleado.SelectedValue != "")
            {
                var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_horasExtra_loadDatos",
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
                rowFechaInicio.Visible = true;
                rowFechaFin.Visible = true;
                rowHoras.Visible = true;
                btn_guardar.Visible = true;
                lblComentarios.Visible = true;
                txtComentarios.Visible = true;
            }
            else
            {
                rowNombre.Visible = false;
                rowDepartamento.Visible = false;
                rowPuesto.Visible = false;
                rowFechaInicio.Visible = false;
                rowFechaFin.Visible = false;
                rowHoras.Visible = false;
                btn_guardar.Visible = false;
                lblComentarios.Visible = false;
                txtComentarios.Visible = false;
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            //var fecha = txtFechaInicio.Text;
            //var fecha2 = txtFechaFin.Text;

            var sp_recursos_hrsExtra_agregar = DbUtil.ExecuteProc("sp_recursos_horasExtra_agregar",
                new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                new SqlParameter("@fechaIni", txtFechaInicio.Text),
                new SqlParameter("@fechaFin", txtFechaFin.Text),
                new SqlParameter("@no_horas", txtHoras.Text),
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
            try
            {
                string dateInput = txtFechaInicio.Text;
                DateTime dt = Convert.ToDateTime(dateInput);
                DayOfWeek today = dt.DayOfWeek;

                if (today == DayOfWeek.Monday)
                {
                    DateTime inicio = DateTime.Parse(txtFechaInicio.Text);
                    DateTime fin = inicio.AddDays(6);

                    txtFechaFin.Text = fin.ToString("yyyy-MM-dd");

                    mensaje_error.Text = "";
                }

                else
                {
                    txtFechaFin.Text = "";
                    mensaje_error.Text = "Debe seleccionar un Lunes";
                }
            }

            catch { }


        }

        //protected void TextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    DateTime inicio = DateTime.Parse(TextBox1.Text);
        //    DateTime fin = inicio.AddDays(6);

        //    TextBox2.Text = fin.ToString("yyyy-MM-dd");

        //}

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    if (CustomValidator1.Text != "")
        //    {
        //        mensaje_error.BackColor = Color.Green;
        //    }

        //    else
        //    {
        //        mensaje_error.BackColor = Color.Red;
        //    }

        //}
    }
}