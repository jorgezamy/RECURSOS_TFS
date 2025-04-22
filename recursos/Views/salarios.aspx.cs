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
    public partial class salarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    loadNuevosSalarios();

                    loadEmpleados();
                }
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
            }
        }

        protected void b0_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("salarios_bonos.aspx");
        }

        protected void b1_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 0;

            b1.CssClass = "clicked";
            b2.CssClass = "initial";

            tb_NuevosSalarios.Focus();
        }

        protected void b2_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 1;

            b1.CssClass = "initial";
            b2.CssClass = "clicked";
        }

        private void loadNuevosSalarios()
        {
            var sp_loadNuevosSalarios = DbUtil.GetCursor("sp_recursos_salarios_nuevosSalarios_loadGrid",
                new SqlParameter("@buscar", tb_NuevosSalarios.Text)
                );

            grid_nuevosSalarios.DataSource = sp_loadNuevosSalarios;
            grid_nuevosSalarios.DataBind();
        }

        protected void bt_btnbuscar_Click(object sender, ImageClickEventArgs e)
        {
            loadNuevosSalarios();
            tb_NuevosSalarios.Focus();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func_nuevosSalariosBuscar()", true);
        }

        protected void grid_nuevosSalarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "aprobado")
            {
                aprobado_mensajes_alerta.Visible = true;
                aprobado_mensajes_confirmacion.Visible = false;

                btn_nuevosSalarios_aprobar.Visible = true;

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                var noEmpleado = HttpUtility.HtmlDecode(grid_nuevosSalarios.Rows[rowIndex].Cells[1].Text.ToString());
                var nombre = HttpUtility.HtmlDecode(grid_nuevosSalarios.Rows[rowIndex].Cells[2].Text.ToString());
                var salarioAntes = HttpUtility.HtmlDecode(grid_nuevosSalarios.Rows[rowIndex].Cells[3].Text.ToString());
                var salarioDespues = HttpUtility.HtmlDecode(grid_nuevosSalarios.Rows[rowIndex].Cells[4].Text.ToString());
                var tipo = HttpUtility.HtmlDecode(grid_nuevosSalarios.Rows[rowIndex].Cells[5].Text.ToString());

                var sp_loadAprobado = DbUtil.ExecuteProc("sp_recursos_salarios_nuevosSalarios_aprobarLoadDatos",
                    new SqlParameter("@noEmpleado", noEmpleado),
                    MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                lb_aprobado_noEmpleado.Text = noEmpleado;
                lb_aprobado_nombre.Text = nombre;
                lb_aprobado_departamento.Text = sp_loadAprobado["@departamento"].ToString();
                lb_aprobado_puesto.Text = sp_loadAprobado["@puesto"].ToString();
                lb_aprobado_antes.Text = salarioAntes;
                lb_aprobado_despues.Text = salarioDespues;
                lb_aprobado_tipo.Text = tipo;
            }
        }

        protected void btn_nuevosSalarios_aprobar_Click(object sender, EventArgs e)
        {
            var sp_updateAprobado = DbUtil.ExecuteProc("sp_recursos_salarios_nuevosSalarios_aprobar",
                new SqlParameter("@no_empleado", lb_aprobado_noEmpleado.Text),
                new SqlParameter("@tipo", lb_aprobado_tipo.Text)
                );

            aprobado_mensajes_alerta.Visible = false;
            aprobado_mensajes_confirmacion.Visible = true;

            btn_nuevosSalarios_aprobar.Visible = false;

            loadNuevosSalarios();
        }

        private void loadEmpleados()
        {
            drop_empleados_salarios.Items.Clear();

            var load_empleadosSalarios = DbUtil.GetCursor("sp_recursos_salarios_loadEmpleados");

            drop_empleados_salarios.Items.Add(new ListItem("-- Seleccionar --", ""));

            drop_empleados_salarios.DataSource = load_empleadosSalarios;
            drop_empleados_salarios.DataValueField = "noEmpleado";
            drop_empleados_salarios.DataTextField = "nombre";
            drop_empleados_salarios.DataBind();
        }

        private void loadFormaPago()
        {
            drop_formaPago.Items.Clear();
            drop_formaPago.Items.Add(new ListItem("-- Seleccionar --", ""));

            var sp_loadFormaPago = DbUtil.GetCursor("sp_recursos_salarios_loadFormaPago");

            drop_formaPago.DataSource = sp_loadFormaPago;
            drop_formaPago.DataValueField = "idFormaPago";
            drop_formaPago.DataTextField = "descripcion";
            drop_formaPago.DataBind();
        }

        private void loadBancos()
        {
            drop_banco.Items.Clear();
            drop_banco.Items.Add(new ListItem("-- Seleccionar --", ""));

            var sp_loadBancos = DbUtil.GetCursor("sp_recursos_salarios_loadBancos");

            drop_banco.DataSource = sp_loadBancos;
            drop_banco.DataValueField = "idBanco";
            drop_banco.DataTextField = "nombreComercial";
            drop_banco.DataBind();
        }

        protected void drop_empleados_salarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_empleados_salarios.SelectedValue != "")
            {
                loadFormaPago();
                loadBancos();

                drop_formaPago.Enabled = true;
                drop_banco.Enabled = true;
                tb_noCuenta.Enabled = true;
                tb_salario.Enabled = true;

                tb_noCuenta.Text = "";
                tb_salario.Text = "";

                var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_salarios_loadEmpleadosDatos",
                    new SqlParameter("@noEmpleado", drop_empleados_salarios.SelectedValue),
                    MsBarco.DbUtil.NewSqlParam("@no_empleado", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@idFormaPago", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@id_banco", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@no_cuenta", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                lbl_noEmpleado.Text = sp_loadDatos["@no_empleado"].ToString();
                lbl_nombre.Text = sp_loadDatos["@nombre"].ToString();
                drop_formaPago.SelectedValue = sp_loadDatos["@idFormaPago"].ToString();
                drop_banco.SelectedValue = sp_loadDatos["@id_banco"].ToString();
                tb_noCuenta.Text = sp_loadDatos["@no_cuenta"].ToString();
                tb_salario.Text = sp_loadDatos["@salario"].ToString();
                hidden_salario.Value = sp_loadDatos["@salario"].ToString();

                if (drop_formaPago.SelectedValue == "28")
                {
                    row_banco.Visible = true;
                    row_noCuenta.Visible = true;
                }
                else
                {
                    row_banco.Visible = false;
                    row_noCuenta.Visible = false;
                }

                if (sp_loadDatos["@puesto"].ToString() != "6.1.1.5")
                {
                    tb_salario.Enabled = true;
                }
                else
                {
                    tb_salario.Enabled = false;
                }

                mensaje_error.Text = "";

                row_salario.Visible = true;
                btn_update.Visible = true;
            }
            else
            {
                drop_formaPago.SelectedValue = "";
                drop_formaPago.Enabled = false;

                drop_banco.SelectedValue = "";
                drop_banco.Enabled = false;

                tb_noCuenta.Enabled = false;
                tb_noCuenta.Text = "";

                tb_salario.Enabled = false;
                tb_salario.Text = "";

                mensaje_error.Text = "";

                row_salario.Visible = false;
                btn_update.Visible = false;
            }
        }

        protected void drop_formaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_formaPago.SelectedValue == "28")
            {
                row_banco.Visible = true;
                row_noCuenta.Visible = true;
            }
            else
            {
                row_banco.Visible = false;
                row_noCuenta.Visible = false;
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (float.Parse(tb_salario.Text) >= float.Parse(hidden_salario.Value.ToString()))
            {
                var sp_updateDatos = DbUtil.ExecuteProc("sp_recursos_salarios_updateEmpleadosDatos",
                    new SqlParameter("@noEmpleado", drop_empleados_salarios.SelectedValue),
                    new SqlParameter("@formaPagoIngresado", drop_formaPago.SelectedValue),
                    new SqlParameter("@idBancoIngresado", !string.IsNullOrEmpty(drop_banco.SelectedValue) ? drop_banco.SelectedValue : (object)DBNull.Value),
                    new SqlParameter("@noCuentaIngresado", !string.IsNullOrEmpty(tb_noCuenta.Text) ? tb_noCuenta.Text : (object)DBNull.Value),
                    new SqlParameter("@salarioIngresado", tb_salario.Text)
                    );

                mensaje_error.Text = "Datos actualizados correctamente.";
            }
            else
            {
                mensaje_error.Text = "El salario no puede ser menor al establecido.";
            }
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void grid_nuevosSalarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_nuevosSalarios.PageIndex = e.NewPageIndex;
            grid_nuevosSalarios.DataBind();

            loadNuevosSalarios();
        }
    }
}