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
    public partial class vacaciones : System.Web.UI.Page
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
            try
            {
                ddlAnio.Items.Clear();
                ddlAnio.Items.Add(new ListItem("-- Seleccionar --", ""));

                rowNombre.Visible = false;
                rowDepartamento.Visible = false;
                rowPuesto.Visible = false;
                rowIngreso.Visible = false;
                rowAntiguedad.Visible = false;
                rowPeriodo.Visible = false;
                rowDiasOtorgados.Visible = false;
                rowDiasTomados.Visible = false;
                rowDiasPagados.Visible = false;
                rowDiasRestantes.Visible = false;
                rowTiempoRestante.Visible = false;

                txtFechaPago.Text = "";
                rowFecha.Visible = false;
                rowTramite.Visible = false;
                rowDiasPagar.Visible = false;
                rowFechaPago.Visible = false;
                btn_guardar.Visible = false;
                lblComentarios.Visible = false;
                txtComentarios.Visible = false;

                if (ddlEmpleado.SelectedValue != "")
                {
                    rowAnio.Visible = true;

                    var sp_loadAnios = DbUtil.GetCursor("sp_recursos_vacaciones_loadAnios",
                        new SqlParameter("@empleado", ddlEmpleado.SelectedValue)
                        );

                    ddlAnio.DataSource = sp_loadAnios;
                    ddlAnio.DataValueField = "anio";
                    ddlAnio.DataTextField = "descripcion";
                    ddlAnio.DataBind();
                }

                else
                {
                    rowAnio.Visible = false;
                }
            }
            catch { }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTramite.SelectedValue == "1")
                {
                    if (int.Parse(txt_dias.Text) <= int.Parse(lblRestantes.Text))
                    {
                        if (DateTime.Parse(txtFechaInicio.Text) < DateTime.Parse(HFInicioPeriodo.Value) | DateTime.Parse(txtFechaFin.Text) > DateTime.Parse(HFFinPeriodo.Value))
                        {
                            mensaje_error.Text = "Fechas fuera del periodo de vacaciones.";
                        }

                        else
                        {
                            var sp_recursos_vacaciones_agregar = DbUtil.ExecuteProc("sp_recursos_vacaciones_agregar",
                                new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                                new SqlParameter("@dia_ini", DateTime.Parse(txtFechaInicio.Text)),
                                new SqlParameter("@dia_fin", DateTime.Parse(txtFechaFin.Text)),
                                new SqlParameter("@dias_vacaciones", txt_dias.Text),
                                new SqlParameter("@dias_pagar", ""),
                                new SqlParameter("@fecha_pago", ""),
                                new SqlParameter("@anio", txtAnio.Text),
                                new SqlParameter("@comentarios", txtComentarios.Text)
                                );

                            Response.Redirect("vacaciones.aspx");
                            mensaje_error.Text = "Fechas dentro del periodo.";

                        }
                    }

                    else
                    {
                        mensaje_error.Text = "Número de días fuera de rango.";
                    }
                }

                else if (ddlTramite.SelectedValue == "2")
                {
                    if (int.Parse(txt_dias.Text) <= int.Parse(lblRestantes.Text))
                    {
                        if (DateTime.Parse(txtFechaInicio.Text) < DateTime.Parse(HFInicioPeriodo.Value) | DateTime.Parse(txtFechaFin.Text) > DateTime.Parse(HFFinPeriodo.Value))
                        {
                            mensaje_error.Text = "Fechas fuera del periodo de vacaciones.";
                        }

                        else
                        {
                            var sp_recursos_vacaciones_agregar = DbUtil.ExecuteProc("sp_recursos_vacaciones_agregar",
                                new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                                new SqlParameter("@dia_ini", DateTime.Parse(txtFechaInicio.Text)),
                                new SqlParameter("@dia_fin", DateTime.Parse(txtFechaFin.Text)),
                                new SqlParameter("@dias_vacaciones", txt_dias.Text),
                                new SqlParameter("@dias_pagar", txtDiasPagar.Text),
                                new SqlParameter("@fecha_pago", DateTime.Parse(txtFechaPago.Text)),
                                new SqlParameter("@anio", txtAnio.Text),
                                new SqlParameter("@comentarios", txtComentarios.Text)
                                );

                            Response.Redirect("vacaciones.aspx");
                            mensaje_error.Text = "Fechas dentro del periodo.";

                        }
                    }

                    else
                    {
                        mensaje_error.Text = "Número de días fuera de rango.";
                    }
                }

                else if (ddlTramite.SelectedValue == "3")
                {
                    var sp_recursos_vacaciones_agregar = DbUtil.ExecuteProc("sp_recursos_vacaciones_agregar",
                        new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                        new SqlParameter("@dia_ini", ""),
                        new SqlParameter("@dia_fin", ""),
                        new SqlParameter("@dias_vacaciones", ""),
                        new SqlParameter("@dias_pagar", txtDiasPagar.Text),
                        new SqlParameter("@fecha_pago", DateTime.Parse(txtFechaPago.Text)),
                        new SqlParameter("@anio", txtAnio.Text),
                        new SqlParameter("@comentarios", txtComentarios.Text)
                        );

                    Response.Redirect("vacaciones.aspx");
                }



            }

            catch { }



            }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            //try
            //{          
            //    if (int.Parse(txt_dias.Text) <= int.Parse(lblRestantes.Text))
            //    {
            //        if (DateTime.Parse(txtFechaInicio.Text) < DateTime.Parse(HFInicioPeriodo.Value) | DateTime.Parse(txtFechaFin.Text) > DateTime.Parse(HFFinPeriodo.Value))
            //        {
            //            mensaje_error.Text = "Fechas fuera del periodo de vacaciones.";
            //        }

            //        else
            //        {
            //            mensaje_error.Text = "Fechas dentro del periodo.";

            //        }
            //    }

            //    else
            //    {
            //        mensaje_error.Text = "Número de días fuera de rango.";
            //    }

            //}

            //catch { }
            Response.Redirect("vacaciones.aspx");
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            if (txtFechaInicio.Text != "")
            {
                txt_dias.Enabled = true;
                txt_dias.Text = "";
                txtFechaFin.Text = "";

                txt_dias.Focus();
            }
            else
            {
                txt_dias.Enabled = false;
                txt_dias.Text = "";
                txtFechaFin.Text = "";
            }
        }

        protected void txt_dias_TextChanged(object sender, EventArgs e)
        {
            if (txt_dias.Text != "")
            {
                DateTime inicio = DateTime.Parse(txtFechaInicio.Text);
                DateTime fin = inicio.AddDays(Int32.Parse(txt_dias.Text) - 1);
                txtFechaFin.Text = fin.ToString("yyyy-MM-dd");
                txtDiasPagar.Focus();
            }         
            else
            {
                txtFechaFin.Text = "";
            }
        }

        protected void ddlTramite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTramite.SelectedValue != "")
            {
                if (ddlTramite.SelectedValue == "1")
                {
                    txtDiasPagar.Text = "0";
                    txtFechaPago.Text = "";
                    rowFecha.Visible = true;
                    rowDiasPagar.Visible = false;
                    rowFechaPago.Visible = false;
                }

                else if (ddlTramite.SelectedValue == "2")
                {
                    if (Int32.Parse(lblPagados.Text) >= Int32.Parse(lblOtorgados.Text))
                    {
                        txtDiasPagar.Text = "0";
                        txtFechaPago.Text = "";
                    }

                    else
                    {
                        int diasPagar = Int32.Parse(lblOtorgados.Text) - Int32.Parse(lblPagados.Text);
                        txtDiasPagar.Text = diasPagar.ToString();
                    }

                    rowFecha.Visible = true;
                    rowDiasPagar.Visible = true;
                    rowFechaPago.Visible = true;
                }

                else if (ddlTramite.SelectedValue == "3")
                {
                    if (Int32.Parse(lblPagados.Text) >= Int32.Parse(lblOtorgados.Text))
                    {
                        txtDiasPagar.Text = "0";
                        txtFechaPago.Text = "";
                    }

                    else
                    {
                        int diasPagar = Int32.Parse(lblOtorgados.Text) - Int32.Parse(lblPagados.Text);
                        txtDiasPagar.Text = diasPagar.ToString();
                    }

                    txtFechaInicio.Text = "";
                    txt_dias.Text = "0";
                    txtFechaFin.Text = "";
                    rowFecha.Visible = false;
                    rowDiasPagar.Visible = true;
                    rowFechaPago.Visible = true;
                    RFVFechaIni.Enabled = false;
                    RFVFechaFin.Enabled = false;
                }
                txtFechaPago.Text = "";
                lblComentarios.Visible = true;
                txtComentarios.Visible = true;

            }

            else
            {
                rowFecha.Visible = false;
                txtDiasPagar.Text = "0";
                txtFechaPago.Text = "";
                rowDiasPagar.Visible = false;
                rowFechaPago.Visible = false;
                lblComentarios.Visible = false;
                txtComentarios.Visible = false;
            }
        }

        protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAnio.SelectedValue != "")
                {

                    var sp_loadDatos = DbUtil.ExecuteProc("sp_recursos_vacaciones_loadDatos",
                        new SqlParameter("@empleado", ddlEmpleado.SelectedValue),
                        MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@antiguedad", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@periodo", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@inicioPeriodo", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@finPeriodo", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@vacaciones", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@diasTomados", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@diasPagados", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@restantes", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        MsBarco.DbUtil.NewSqlParam("@tiempoRestante", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                        new SqlParameter("@anioIngresado", ddlAnio.SelectedValue)
                        );


                    ddlTramite.Items.Clear();
                    ddlTramite.Items.Add(new ListItem("--Seleccionar--", ""));

                    if (int.Parse(sp_loadDatos["@restantes"].ToString()) > 0)
                    {
                        ddlTramite.Items.Add(new ListItem("Vacaciones", "1"));
                    }

                    if (int.Parse(sp_loadDatos["@diasPagados"].ToString()) < int.Parse(sp_loadDatos["@vacaciones"].ToString()))
                    {
                        ddlTramite.Items.Add(new ListItem("Sólo pago", "3"));
                    }

                    if (int.Parse(sp_loadDatos["@restantes"].ToString()) > 0 && int.Parse(sp_loadDatos["@diasPagados"].ToString()) < int.Parse(sp_loadDatos["@vacaciones"].ToString()))
                    {
                        ddlTramite.Items.Add(new ListItem("Vacaciones y pago", "2"));
                    }

                    if (int.Parse(sp_loadDatos["@restantes"].ToString()) <= 0 && int.Parse(sp_loadDatos["@diasPagados"].ToString()) >= int.Parse(sp_loadDatos["@vacaciones"].ToString()))
                    //else
                    {
                        ddlTramite.Items.Add(new ListItem("No cuenta con días a tomar ni pagar", ""));
                    }

                    lblNombre.Text = sp_loadDatos["@nombre"].ToString();
                    lblDepartamento.Text = sp_loadDatos["@departamento"].ToString();
                    lblPuesto.Text = sp_loadDatos["@puesto"].ToString();
                    lblIngreso.Text = sp_loadDatos["@ingreso"].ToString();
                    lblAntiguedad.Text = sp_loadDatos["@antiguedad"].ToString();
                    lblPeriodo.Text = sp_loadDatos["@periodo"].ToString();
                    HFInicioPeriodo.Value = sp_loadDatos["@inicioPeriodo"].ToString();
                    HFFinPeriodo.Value = sp_loadDatos["@finPeriodo"].ToString();
                    lblOtorgados.Text = sp_loadDatos["@vacaciones"].ToString();
                    lblTomados.Text = sp_loadDatos["@diasTomados"].ToString();
                    lblPagados.Text = sp_loadDatos["@diasPagados"].ToString();
                    lblRestantes.Text = sp_loadDatos["@restantes"].ToString();
                    lblTiempoRestante.Text = sp_loadDatos["@tiempoRestante"].ToString();
                    lblDiasAprobados.Text = "Días aprobados (máximo " + lblRestantes.Text + "):";
                    ddlTramite.SelectedValue = "";
                    rowFecha.Visible = false;
                    txtFechaInicio.Text = "";
                    txtFechaFin.Text = "";
                    txt_dias.Text = "";
                    txt_dias.Enabled = false;
                    rowDiasPagar.Visible = false;
                    rowFechaPago.Visible = false;
                    txtFechaPago.Text = "";
                    lblComentarios.Visible = false;
                    txtComentarios.Visible = false;

                    rowNombre.Visible = true;
                    rowDepartamento.Visible = true;
                    rowPuesto.Visible = true;
                    rowIngreso.Visible = true;
                    rowAntiguedad.Visible = true;
                    rowPeriodo.Visible = true;
                    rowDiasOtorgados.Visible = true;
                    rowDiasTomados.Visible = true;
                    rowDiasPagados.Visible = true;
                    rowDiasRestantes.Visible = true;
                    rowTiempoRestante.Visible = true;

                    rowTramite.Visible = true;
                    //rowFecha.Visible = true;
                    //rowTramite.Visible = true;
                    //rowDiasPagar.Visible = true;
                    //rowFechaPago.Visible = true;
                    btn_guardar.Visible = true;
                    //lblComentarios.Visible = true;
                    //txtComentarios.Visible = true;

                    DateTime inicio = DateTime.Parse(HFInicioPeriodo.Value);
                    int year = inicio.Year;
                    txtAnio.Text = "" + year;




                }
                else
                {
                    rowNombre.Visible = false;
                    rowDepartamento.Visible = false;
                    rowPuesto.Visible = false;
                    rowIngreso.Visible = false;
                    rowAntiguedad.Visible = false;
                    rowPeriodo.Visible = false;
                    rowDiasOtorgados.Visible = false;
                    rowDiasTomados.Visible = false;
                    rowDiasPagados.Visible = false;
                    rowDiasRestantes.Visible = false;
                    rowTiempoRestante.Visible = false;

                    txtFechaPago.Text = "";
                    rowFecha.Visible = false;
                    rowTramite.Visible = false;
                    rowDiasPagar.Visible = false;
                    rowFechaPago.Visible = false;
                    btn_guardar.Visible = false;
                    lblComentarios.Visible = false;
                    txtComentarios.Visible = false;

                }
            }

            catch { }
        }
    }
}