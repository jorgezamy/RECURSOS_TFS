using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using MsBarco;
using System.Globalization;

namespace recursos.Views
{
    public partial class empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    MultiView.ActiveViewIndex = 0;
                    MultiView_baja.ActiveViewIndex = 0;


                    FillNumeroNinos();

                    loadAlertas();
                    loadsupervisor();
                    loadcolonias();

                    loadCheckboxlist();

                    Drop_cliente.Items.Clear();
                    Drop_puestoCliente.Items.Clear();

                    tb_buscarNumero.Focus();

                    Buscar();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        private void loadCheckboxlist()
        {
            var sp_loadColumnas = DbUtil.GetCursor("sp_recursos_empleados_editar_loadCheckBoxList");

            checkBox_tipo_licencia.DataSource = sp_loadColumnas;
            checkBox_tipo_licencia.DataTextField = "tipo";
            checkBox_tipo_licencia.DataValueField = "tipo";
            checkBox_tipo_licencia.DataBind();
        }

        private void loadAlertas()
        {
            var sp_alertas = DbUtil.GetCursor("sp_recursos_empleados_alerta",
                new SqlParameter("@opcion", "1"),
                new SqlParameter("@id_empresa", Session["idEmpresa"])
                );

            gridAlertas.DataSource = sp_alertas;
            gridAlertas.DataBind();
        }

        private void loadGridViewDisplay(string op)
        {
            var sp_alertas = DbUtil.GetCursor("sp_recursos_empleados_alerta",
                new SqlParameter("@opcion", op),
                new SqlParameter("@id_empresa", Session["idEmpresa"]));

            grid_alertas_display.DataSource = sp_alertas;
            grid_alertas_display.DataBind();
        }

        private void loadsupervisor()
        {
            var sp_loadSupervisor = DbUtil.GetCursor("sp_recursos_empleado_add_seleccionarSupervisores",
                new SqlParameter("id_empresa", Session["idEmpresa"]));

            Drop_supervisor.DataSource = sp_loadSupervisor;
            Drop_supervisor.DataValueField = "noEmpleado";
            Drop_supervisor.DataTextField = "nombre";
            Drop_supervisor.DataBind();

        }

        private void loadcolonias()
        {
            var sp_loadColonias = DbUtil.GetCursor("sp_recursos_empleado_add_seleccionarColonias");

            Drop_colonia.DataSource = sp_loadColonias;
            Drop_colonia.DataValueField = "colonia";
            Drop_colonia.DataTextField = "colonia";
            Drop_colonia.DataBind();
        }

        private void MaxDate()
        {
            btn_save_modal_agregar.Visible = true;

            lbl_alerta.Text = "";

            DateTime dt1;
            DateTime limite = DateTime.Now;

            if (tb_penal.Text != "")
            {
                if (tb_penalVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_penalVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(1))
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Vigencia de carta penal no puede ser superior a 1 año.";
                        btn_save_modal_agregar.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Carta penal no tiene fecha de vigencia registrada.";
                    btn_save_modal_agregar.Visible = false;
                }
            }
            else
            {
                if (tb_penalVig.Text != "")
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "La carta penal no tiene numero de folio.";
                    btn_save_modal_agregar.Visible = false;
                }
            }

            if (tb_licencia.Text != "")
            {
                if (tb_licenciaVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_licenciaVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(6))
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Vigencia de licencia de conducir no puede ser superior a 6 años.";
                        btn_save_modal_agregar.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Carta de licencia de conducir no tiene fecha de vigencia registrada.";
                    btn_save_modal_agregar.Visible = false;
                }
            }
            else
            {
                if (tb_licenciaVig.Text != "")
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se ingreso el numero de licencia de conducir.";
                    btn_save_modal_agregar.Visible = false;
                }
            }

            if (tb_apto.Text != "")
            {
                if (tb_aptoVig.Text != "")
                {
                    if (tb_aptoInicio.Text != "")
                    {
                        dt1 = DateTime.ParseExact(tb_aptoVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                        DateTime dt3 = DateTime.ParseExact(tb_aptoInicio.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);

                        if (dt1.Date < dt3.Date)
                        {
                            lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Vigencia de APTO no puede ser menor a la de inicio.";
                            btn_save_modal_agregar.Visible = false;
                        }
                        else
                        {
                            if (dt1.Date > limite.Date.AddYears(2))
                            {
                                lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Vigencia de APTO no puede ser superior a 2 años.";
                                btn_save_modal_agregar.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "APTO no tiene fecha de inicio registrada.";
                        btn_save_modal_agregar.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "APTO no tiene fecha de vigencia registrada.";
                    btn_save_modal_agregar.Visible = false;
                }
            }
            else
            {
                if (tb_aptoVig.Text != "" || tb_aptoInicio.Text != "")
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se ingreso el numero de apto.";
                    btn_save_modal_agregar.Visible = false;
                }
            }

            if (tb_fast.Text != "")
            {
                if (tb_fastVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_fastVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(6))
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Vigencia de FAST no puede ser superior a 5 años.";
                        btn_save_modal_agregar.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "FAST no tiene fecha de vigencia registrada.";
                    btn_save_modal_agregar.Visible = false;
                }
            }
            else
            {
                if (tb_fastVig.Text != "")
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se ingreso el numero de FAST";
                    btn_save_modal_agregar.Visible = false;
                }
            }

            if (tb_visa.Text != "")
            {
                if (tb_visaVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_visaVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(10))
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Vigencia de VISA no puede ser superior a 10 años.";
                        btn_save_modal_agregar.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se registro fecha de vigencia de VISA.";
                    btn_save_modal_agregar.Visible = false;
                }
            }
            else
            {
                if (tb_visaVig.Text != "")
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se ingreso el numero de VISA.";
                    btn_save_modal_agregar.Visible = false;
                }
            }

            if (tb_NumServicio.Text != "")
            {
                if (date_ServicioVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(date_ServicioVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(1))
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Vigencia del recibo de servicio no puede ser superior a 1 año.";
                        btn_save_modal_agregar.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Recibo de servicio no tiene fecha de vigencia.";
                    btn_save_modal_agregar.Visible = false;
                }
            }
            else
            {
                if (date_ServicioVig.Text != "")
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se ingreso el numero de recibo de servicio";
                    btn_save_modal_agregar.Visible = false;
                }
            }

            //**************Checar la edad del empleado********************
            DateTime BirthDate = DateTime.ParseExact(tb_fecnac.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
            int YearsPassed = DateTime.Now.Year - BirthDate.Year;
            // Are we before the birth date this year? If so subtract one year from the mix
            if (DateTime.Now.Month < BirthDate.Month || (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day < BirthDate.Day))
            {
                YearsPassed--;
            }

            if (YearsPassed < 18)
            {
                lbl_alerta.Text = lbl_alerta.Text + "<br />" + "El empleado es menor de edad, no es contratable.";
                btn_save_modal_agregar.Visible = false;
            }
            //************** FIN Checar la edad del empleado********************

            //*****************COSNTANCIA**********************
            bool check = false;
            foreach (ListItem item in checkBox_tipo_licencia.Items)
            {
                if (item.Selected)
                {
                    check = true;
                }
            }

            if (tb_constancia.Text != "")
            {
                if (check == false)
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se selecciono un tipo de licencia";
                    btn_save_modal_agregar.Visible = false;
                }
                else
                {
                    if (tb_constancia_inicio.Text == "")
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se ingreso fecha de inicio de constancia";
                        btn_save_modal_agregar.Visible = false;
                    }

                    if (tb_constancia_fin.Text == "")
                    {
                        lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se ingreso fecha de fin de constancia";
                        btn_save_modal_agregar.Visible = false;
                    }
                    else
                    {
                        dt1 = DateTime.ParseExact(tb_constancia_inicio.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                        DateTime dt2 = DateTime.ParseExact(tb_constancia_fin.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);

                        if (dt1.Date > limite)
                        {
                            lbl_alerta.Text = lbl_alerta.Text + "<br />" + "La fecha de inicio de constancia no puede ser mayor a fecha actual";
                            btn_save_modal_agregar.Visible = false;
                        }
                        if (dt1.Date > dt2.Date)
                        {
                            lbl_alerta.Text = lbl_alerta.Text + "<br />" + "Fecha de vigencia de constancia es menor que la de inicio";
                            btn_save_modal_agregar.Visible = false;
                        }
                    }
                }
            }
            else
            {
                if (check == true)
                {
                    lbl_alerta.Text = lbl_alerta.Text + "<br />" + "No se registro folio de constancia del tipo de licencia";
                    btn_save_modal_agregar.Visible = false;
                }
            }

            if (!string.IsNullOrEmpty(tb_salario.Text))
            {
                if (float.Parse(tb_salario.Text) < float.Parse(hidden_salario.Value))
                {
                    lbl_alerta.Text = "El salario NO puede ser menor al establecido.";
                    btn_save_modal_agregar.Visible = false;
                }
            }
            else
            {
                lbl_alerta.Text = "El salario no puede ser vacio.";
                btn_save_modal_agregar.Visible = false;
            }

            if (tb_nombre.Text == "" && tb_apepat.Text == "" && tb_apemat.Text == "" && tb_fecnac.Text == "" && SexoList.SelectedValue == "")
            {
                lbl_alerta.Text = "Datos personales en blanco.";
                btn_save_modal_agregar.Visible = false;
            }

            if (tb_curp.Text == "")
            {
                lbl_alerta.Text = "CURP vacío.";
                btn_save_modal_agregar.Visible = false;
            }

            if (tb_rfc.Text == "")
            {
                lbl_alerta.Text = "RFC vacío.";
                btn_save_modal_agregar.Visible = false;
            }

            if (tb_imss.Text == "")
            {
                lbl_alerta.Text = "IMSS vacío.";
                btn_save_modal_agregar.Visible = false;
            }
            
            if (Drop_depto.SelectedValue == "" && Drop_puesto.SelectedValue == "")
            {
                lbl_alerta.Text = "Datos laborales vacíos.";
                btn_save_modal_agregar.Visible = false;
            }
        }

        protected void grid_buscar_DataBound(object sender, EventArgs e)
        {
            var usuario = Session["usuario"].ToString();

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_empleado_inicio_roles_permitirAcceso",
                new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                new SqlParameter("@usuario", usuario),
                MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@empleados_editar", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@empleados_alta", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@empleados_baja", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@empleados_fotos", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                if (sp_loadRol["@empleados_editar"].ToString() == "1")
                {
                    for (int i = 0; i < grid_buscar.Rows.Count; i++)
                    {
                        Button btn_editar = (Button)grid_buscar.Rows[i].Cells[1].FindControl("btn_edit");
                        btn_editar.CssClass = "btn_select";
                        btn_editar.Enabled = true;
                    }
                }
                if (sp_loadRol["@empleados_alta"].ToString() == "1")
                {
                    Button1.ImageUrl = "~/images/empleados/menuBtn_empleados_1.png";
                    Button1.CssClass = "menuBtn_empleados";
                    Button1.Enabled = true;
                }
                if (sp_loadRol["@empleados_baja"].ToString() == "1")
                {
                    Button2.ImageUrl = "~/images/empleados/menuBtn_empleados_2.png";
                    Button2.CssClass = "menuBtn_empleados";
                    Button2.Enabled = true;
                }
                if (sp_loadRol["@empleados_fotos"].ToString() == "1")
                {
                    Button3.ImageUrl = "~/images/empleados/menuBtn_empleados_5.png";
                    Button3.CssClass = "menuBtn_empleados";
                    Button3.Enabled = true;
                }
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("inicio.aspx");
            }
        }

        protected void tab1_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 0;
            tb_nombre.Focus();

            tab1.CssClass = "clicked";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
        }

        protected void tab2_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 1;
            tb_telefono.Focus();

            tab1.CssClass = "initial";
            tab2.CssClass = "clicked";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
        }

        protected void tab3_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 2;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "clicked";
            tab4.CssClass = "initial";

            //ClientScript.RegisterStartupScript(GetType(), "calendarioHTML", "calendarioHTML();", true);
        }

        protected void tab4_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 3;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "clicked";

            //ClientScript.RegisterStartupScript(GetType(), "calendarioHTML", "calendarioHTML();", true);
        }

        private void FillNumeroNinos()
        {
            Drop_NoNinos.Items.Clear();

            for (int i = 0; i <= 10; i++)
            {
                Drop_NoNinos.Items.Add(i.ToString());

                //Otra solucion
                //Drop_NoNinos.Items.Insert(i, new ListItem( (i + 1).ToString() ) );
            }
        }

        protected void SexoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SexoList.Text == "F")
            {
                Drop_tallaP.Items.Clear();
                Drop_tallaP.Items.Add(new ListItem("--Seleccionar--", ""));

                for (int i = 1; i <= 15; i += 2)
                {
                    Drop_tallaP.Items.Add(i.ToString());
                }
            }

            if (SexoList.Text == "M")
            {
                Drop_tallaP.Items.Clear();
                Drop_tallaP.Items.Add(new ListItem("--Seleccionar--", ""));

                for (int i = 28; i <= 42; i += 2)
                {
                    Drop_tallaP.Items.Add(i.ToString());
                }
            }
        }

        protected void Drop_depto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ValorDropDepto = Drop_depto.SelectedValue;

            if (ValorDropDepto == "")
            {
                Drop_puesto.Items.Clear();
                Drop_puesto.Enabled = false;

                tb_salario.Text = "";
                tb_salario.Enabled = false;

                Drop_cliente.Items.Clear();
                Drop_cliente.Enabled = false;

                Drop_puestoCliente.Items.Clear();
                Drop_puestoCliente.Enabled = false;
            }

            if (ValorDropDepto != "")
            {
                Drop_puesto.Enabled = true;
                Drop_puesto.Items.Clear();

                tb_salario.Text = "";
                tb_salario.Enabled = false;

                var sp_DropDepto = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropDepto",
                    new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                    new SqlParameter("@salario", ""),
                    new SqlParameter("@Drop_valueDepto", ValorDropDepto),
                    new SqlParameter("@Drop_valuePuesto", ""),
                    new SqlParameter("@Drop_valueCliente", ""),
                    MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@salarioCantidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

                var sp_DropDepto1 = DbUtil.ExecuteProc("sp_recursos_empleado_add_LoadDropDepto",
                    new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                    new SqlParameter("@salario", ""),
                    new SqlParameter("@Drop_valueDepto", ValorDropDepto),
                    new SqlParameter("@Drop_valuePuesto", ""),
                    new SqlParameter("@Drop_valueCliente", ""),
                    MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@salarioCantidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

                if (sp_DropDepto1["@stored_vacio"].ToString() == "0")
                {
                    Drop_puesto.Items.Add(new ListItem("-- Seleccionar --", ""));

                    Drop_puesto.DataSource = sp_DropDepto;
                    Drop_puesto.DataValueField = "IdPuesto";
                    Drop_puesto.DataTextField = "DescPuesto";
                    Drop_puesto.DataBind();
                }
                if (sp_DropDepto1["@stored_vacio"].ToString() == "1")
                {
                    Drop_puesto.Items.Clear();
                    Drop_puesto.Items.Add(new ListItem("-- Sin puestos --", ""));
                }

                if (ValorDropDepto != "6")
                {
                    Drop_cliente.Items.Clear();
                    Drop_cliente.Enabled = false;
                    Drop_puestoCliente.Items.Clear();
                    Drop_puestoCliente.Enabled = false;
                }
            }
        }

        protected void Drop_puesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool puestoContieneChofer = Drop_puesto.SelectedItem.Text.Contains("Chofer");
            bool puestoContienePlanta = Drop_puesto.SelectedItem.Text.Contains("Planta");

            //if (puestoContieneChofer == true || puestoContienePlanta == true)
            if (Drop_puesto.SelectedValue != "")
            {
                tb_salario.Enabled = true;

                if (Drop_puesto.SelectedValue == "6.1.1.5")
                {
                    Drop_cliente.Enabled = false;
                    Drop_cliente.Visible = false;
                    lb_cliente.Visible = false;

                    Drop_puestoCliente.Enabled = true;
                    Drop_puestoCliente.Visible = true;
                    lb_puestoCliente.Visible = true;

                    //chklBonos.Visible = true;

                    var sp_DropPuesto = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropDepto",
                        new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                        new SqlParameter("@salario", "0"),
                        new SqlParameter("@Drop_valueDepto", ""),
                        new SqlParameter("@Drop_valuePuesto", "ChoferOPlanta"),
                        new SqlParameter("@Drop_valueCliente", ""),
                        MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                        MsBarco.DbUtil.NewSqlParam("@salarioCantidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                    Drop_cliente.DataSource = sp_DropPuesto;
                    Drop_cliente.DataValueField = "idCliente";
                    Drop_cliente.DataTextField = "NombreCliente";
                    Drop_cliente.DataBind();

                    var sp_DropPuesto1 = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropDepto",
                        new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                        new SqlParameter("@salario", "0"),
                        new SqlParameter("@Drop_valueDepto", ""),
                        new SqlParameter("@Drop_valuePuesto", "ChoferOPlanta1"),
                        new SqlParameter("@Drop_valueCliente", ""),
                        MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                        MsBarco.DbUtil.NewSqlParam("@salarioCantidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                    Drop_puestoCliente.DataSource = sp_DropPuesto1;
                    Drop_puestoCliente.DataValueField = "IdPuestoClientes";
                    Drop_puestoCliente.DataTextField = "TipoTractorMovimiento";
                    Drop_puestoCliente.DataBind();
                }

                var sp_DropPuesto2 = DbUtil.ExecuteProc("sp_recursos_empleado_add_LoadDropDepto",
                    new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                    new SqlParameter("@salario", "1"),
                    new SqlParameter("@Drop_valueDepto", ""),
                    new SqlParameter("@Drop_valuePuesto", Drop_puesto.SelectedValue),
                    new SqlParameter("@Drop_valueCliente", ""),
                    MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@salarioCantidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

                tb_salario.Text = sp_DropPuesto2["@salarioCantidad"].ToString();
                hidden_salario.Value = sp_DropPuesto2["@salarioCantidad"].ToString();

                if (Drop_puesto.SelectedValue != "6.1.1.5")
                {
                    tb_salario.Enabled = true;
                }
                else
                {
                    tb_salario.Enabled = false;
                }
            }
            else
            {
                Drop_cliente.Items.Clear();
                //Drop_cliente.Enabled = false;
                //Drop_cliente.Visible = false;

                Drop_puestoCliente.Items.Clear();
                //Drop_puestoCliente.Enabled = false;
                //Drop_puestoCliente.Visible = false;
                //Drop_puesto.Visible = false;

                Drop_cliente.Visible = false;
                lb_cliente.Visible = false;

                Drop_puestoCliente.Enabled = false;
                Drop_puestoCliente.Visible = false;
                lb_puestoCliente.Visible = false;

                tb_salario.Text = "";
                tb_salario.Enabled = false;

                //chklBonos.Visible = false;
            }
        }

        protected void Drop_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ValorDropCliente = Drop_cliente.SelectedValue;

            if (ValorDropCliente != "")
            {
                bool puestoContieneChofer = Drop_puesto.SelectedItem.Text.Contains("Chofer");
                bool puestoContienePlanta = Drop_puesto.SelectedItem.Text.Contains("Planta");

                if (puestoContieneChofer == true)
                {
                    Drop_puestoCliente.Enabled = true;
                    Drop_puestoCliente.Visible = true;

                    //Aqui podemos concoer si este stored procedure podemos esta vacio o no
                    var sp_DropCliente2 = DbUtil.ExecuteProc("sp_recursos_empleado_add_LoadDropDepto",
                        new SqlParameter("@Drop_valueDepto", ""),
                        new SqlParameter("@Drop_valuePuesto", ""),
                        new SqlParameter("@Drop_valueCliente", ValorDropCliente),
                        MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                    if (sp_DropCliente2["@stored_vacio"].ToString() == "1")
                    {
                        var sp_DropCliente = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropDepto",
                        new SqlParameter("@Drop_valueDepto", ""),
                        new SqlParameter("@Drop_valuePuesto", ""),
                        new SqlParameter("@Drop_valueCliente", ValorDropCliente),
                        MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                        );

                        Drop_puestoCliente.DataSource = sp_DropCliente;
                        Drop_puestoCliente.DataValueField = "IdPuestoClientes";
                        Drop_puestoCliente.DataTextField = "TipoTractorMovimiento";
                        Drop_puestoCliente.DataBind();
                    }

                    if (sp_DropCliente2["@stored_vacio"].ToString() == "0")
                    {
                        Drop_puestoCliente.Items.Clear();
                        Drop_puestoCliente.Items.Insert(0, new ListItem("--Sin puestos--", ""));
                    }
                }

                if (puestoContieneChofer == false)
                {
                    Drop_puestoCliente.Enabled = false;
                    Drop_puestoCliente.Visible = false;
                }

                if (puestoContienePlanta == true)
                {
                    Drop_puestoCliente.Items.Clear();
                    Drop_puestoCliente.Enabled = false;
                }
            }
            else
            {
                Drop_puestoCliente.Items.Clear();
                Drop_puestoCliente.Enabled = false;
            }
        }

        private void Get_NoReloj()
        {
            var sp_get_noreloj = DbUtil.ExecuteProc("sp_recursos_empleado_add_UltimoNoReloj",
                MsBarco.DbUtil.NewSqlParam("@no_empleado", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            Session.Add("Reloj", sp_get_noreloj["@no_empleado"].ToString());
        }

        protected void guardar_empleado_Click(object sender, EventArgs e)
        {
            modalpop_Alta.Hide();



            Get_NoReloj();
            var fecha = tb_fecnac.Text;
            //var Reloj = NoReloj.Text.Substring(8, 4);

            var no_reloj = Session["Reloj"].ToString();

            string tipo_licencia = "";
            foreach (ListItem item in checkBox_tipo_licencia.Items)
            {
                if (item.Selected)
                {
                    if (tipo_licencia == "")
                        tipo_licencia = item.Text;
                    else
                        tipo_licencia = tipo_licencia + "," + item.Text;
                }
            }

            string direccion;
            if (tb_numInt.Text == "")
            {
                direccion = tb_numDir.Text;
            }
            else
            {
                direccion = tb_numDir.Text + " - " + tb_numInt.Text;
            }

            if (tb_telefono.Text == "+__(___)___-____")
            {
                tb_telefono.Text = "";
            }

            string calle, codigo_postal, colonia;
            if (Drop_cdDir.SelectedValue == "61" || Drop_cdDir.SelectedValue == "1464")
            {
                calle = Drop_calleDir.SelectedItem.Text;
                codigo_postal = Drop_codPostDir.SelectedItem.Text;
                colonia = Drop_colonia.SelectedItem.Text;
            }
            else
            {
                calle = tb_calle.Text;
                codigo_postal = tb_codpostDir.Text;
                colonia = tb_colonia.Text;
            }

                var sp_recursos_empleado_add = DbUtil.ExecuteProc("sp_recursos_empleado_add",
                new SqlParameter("@no_empleado", no_reloj),
                new SqlParameter("@nombre", tb_nombre.Text),
                new SqlParameter("@ape_pat", tb_apepat.Text),
                new SqlParameter("@ape_mat", tb_apemat.Text),
                new SqlParameter("@fec_nac", tb_fecnac.Text),
                new SqlParameter("@sexo", SexoList.SelectedValue),
                new SqlParameter("@edo_civil", Drop_civil.SelectedValue),
                new SqlParameter("@hijos", Drop_NoNinos.Text),
                new SqlParameter("@pais_nac", Drop_paisNac.SelectedValue),
                new SqlParameter("@edo_nac", Drop_edoNac.SelectedValue),
                new SqlParameter("@cd_nac", Drop_cdNac.SelectedValue),
                new SqlParameter("@nom_padre", tb_nompat.Text),
                new SqlParameter("@nom_madre", tb_nommat.Text),

                new SqlParameter("@zona_eco", Drop_zona.SelectedValue),
                new SqlParameter("@telefono", tb_telefono.Text),
                new SqlParameter("@email", tb_correo.Text),
                new SqlParameter("@escolaridad", Drop_escolaridad.SelectedValue),
                new SqlParameter("@escolaridad_documento", drop_escolaridad_documento.SelectedValue),
                new SqlParameter("@escolaridad_institucion", drop_escolaridad_institucion.SelectedValue),

                new SqlParameter("@carrera", tb_carrera.Text),
                new SqlParameter("@ingles", tb_ingles.Text),
                new SqlParameter("@pais", Drop_paisDir.SelectedValue),
                new SqlParameter("@estado", Drop_edoDir.SelectedValue),
                new SqlParameter("@ciudad", Drop_cdDir.Text),
                new SqlParameter("@calle_dir", calle),
                new SqlParameter("@num_dir", direccion),
                new SqlParameter("@codpost_dir", codigo_postal),
                new SqlParameter("@colonia", colonia),
                new SqlParameter("@curp", tb_curp.Text),
                new SqlParameter("@rfc", tb_rfc.Text),
                new SqlParameter("@tallaP", Drop_tallaP.SelectedValue),
                new SqlParameter("@tallaC", !string.IsNullOrEmpty(Drop_tallaC.SelectedValue) ? Drop_tallaC.SelectedValue : (object)DBNull.Value),

                new SqlParameter("@imss", tb_imss.Text),
                new SqlParameter("@validarINFONAVIT", chkINFONAVIT.Checked ? 1 : 0),
                new SqlParameter("@noINFONAVIT", !string.IsNullOrEmpty(txtNoCredito.Text) ? txtNoCredito.Text : (object)DBNull.Value),
                new SqlParameter("@tipoINFONAVIT", ddlINFONAVIT.SelectedValue),
                new SqlParameter("@factorINFONAVIT", txtFactorINFONAVIT.Text),

                new SqlParameter("@validarFONACOT", chkFONACOT.Checked ? 1 : 0),
                new SqlParameter("@fonacot_numero", !string.IsNullOrEmpty(txtNoFONACOT.Text) ? txtNoFONACOT.Text : (object)DBNull.Value),
                new SqlParameter("@fonacot_descuento_mensual", txtRetencionFONACOT.Text),
                new SqlParameter("@fonacot_total", txtTotalFONACOT.Text),
                new SqlParameter("@fonacot_inicio", !string.IsNullOrEmpty(txtFechaFONACOT.Text) ? txtFechaFONACOT.Text : (object)DBNull.Value),

                new SqlParameter("@validarPension", chkPension.Checked ? 1 : 0),
                new SqlParameter("@descuentoPension", txtPension.Text),

                new SqlParameter("@departamento", Drop_depto.SelectedValue),
                new SqlParameter("@puesto", Drop_puesto.SelectedValue),
                new SqlParameter("@puesto_clientes", !string.IsNullOrEmpty(Drop_puestoCliente.SelectedValue) ? Drop_puestoCliente.SelectedValue : (object)DBNull.Value),
                new SqlParameter("@servicio", Drop_servicio.SelectedValue),
                new SqlParameter("@servicio_num", tb_NumServicio.Text),
                new SqlParameter("@servicio_vig", !string.IsNullOrEmpty(date_ServicioVig.Text) ? date_ServicioVig.Text : (object)DBNull.Value),
                new SqlParameter("@cartapenal_num", tb_penal.Text),
                new SqlParameter("@cartapenal_vig", !string.IsNullOrEmpty(tb_penalVig.Text) ? tb_penalVig.Text : (object)DBNull.Value),
                new SqlParameter("@licencia_num", tb_licencia.Text),
                new SqlParameter("@licencia_vig", !string.IsNullOrEmpty(tb_licenciaVig.Text) ? tb_licenciaVig.Text : (object)DBNull.Value),
                new SqlParameter("@apto_folio", tb_apto.Text),
                new SqlParameter("@apto_inicio", !string.IsNullOrEmpty(tb_aptoInicio.Text) ? tb_aptoInicio.Text : (object)DBNull.Value),
                new SqlParameter("@apto_vigencia", !string.IsNullOrEmpty(tb_aptoVig.Text) ? tb_aptoVig.Text : (object)DBNull.Value),
                new SqlParameter("@pasaporte_num", tb_pasaporte.Text),
                new SqlParameter("@pasaporte_vig", !string.IsNullOrEmpty(tb_pasaporteVig.Text) ? tb_pasaporteVig.Text : (object)DBNull.Value),
                new SqlParameter("@visa_num", tb_visa.Text),
                new SqlParameter("@visa_vig", !string.IsNullOrEmpty(tb_visaVig.Text) ? tb_visaVig.Text : (object)DBNull.Value),
                new SqlParameter("@lic_fast_num", tb_fast.Text),
                new SqlParameter("@lic_fast_vig", !string.IsNullOrEmpty(tb_fastVig.Text) ? tb_fastVig.Text : (object)DBNull.Value),
                //new SqlParameter("@cartapolicial_num", tb_policial.Text),
                new SqlParameter("@cartapolicial_vig", !string.IsNullOrEmpty(tb_policialVig.Text) ? tb_policialVig.Text : (object)DBNull.Value),

                new SqlParameter("@supervisor", !string.IsNullOrEmpty(Drop_supervisor.SelectedValue) ? Drop_supervisor.SelectedValue : (object)DBNull.Value),
                new SqlParameter("@contrato", Drop_contrato.SelectedValue),

                new SqlParameter("@constancia_inicio", !string.IsNullOrEmpty(tb_constancia_inicio.Text) ? tb_constancia_inicio.Text : (object)DBNull.Value),
                new SqlParameter("@constancia_fin", !string.IsNullOrEmpty(tb_constancia_fin.Text) ? tb_constancia_fin.Text : (object)DBNull.Value),
                new SqlParameter("@folio_constancia", !string.IsNullOrEmpty(tb_constancia.Text) ? tb_constancia.Text : (object)DBNull.Value),
                new SqlParameter("@tipo_licencia", !string.IsNullOrEmpty(tipo_licencia) ? tipo_licencia : (object)DBNull.Value),

                new SqlParameter("@salarioIngresado", !string.IsNullOrEmpty(tb_salario.Text) ? tb_salario.Text : (object)DBNull.Value),

                MsBarco.DbUtil.NewSqlParam("@existe_empleado", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );


            if (chklBonos.Visible == true)
            {
                foreach (ListItem item in chklBonos.Items)
                {
                    var sp_recursos_empleado_bonos_add = DbUtil.ExecuteProc("sp_recursos_empleado_bonos_add",
                    new SqlParameter("@no_empleado", no_reloj),
                    new SqlParameter("@puesto_clientes", Drop_puestoCliente.SelectedValue),
                    new SqlParameter("@id_aportacion_deduccion_concepto", item.Value),
                    new SqlParameter("@activo", item.Selected ? '1' : '0')
                    );
                }
            }

            if (tb_contacto_nombre.Text != "" && tb_contacto_telefono.Text != "")
            {
                var sp_recursos_contacto_emergencia = DbUtil.ExecuteProc("sp_recursos_empleado_editar_agregar_contacto_emergencia",
                new SqlParameter("@no_empleado", no_reloj),
                new SqlParameter("@nombre", tb_contacto_nombre.Text),
                new SqlParameter("@numero", tb_contacto_telefono.Text)
                );
            }

            MultiView.ActiveViewIndex = 4;

            tab1.Visible = false;
            tab2.Visible = false;
            tab3.Visible = false;
            tab4.Visible = false;
            guardar_empleado.Visible = false;

            if (sp_recursos_empleado_add["@existe_empleado"].ToString() == "0")
            {
                lb4.Text = "¡Nuevo empleado registrado con exito! Número de empleado: #" + no_reloj;
            }
            else
            {
                lb4.Text = "¡Empleado no registrado... Ya ha sido ingresado anteriormente!";
            }
        }
    

        protected void ContinuarAdd_empleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados_fotos.aspx");
        }

        protected void Drop_paisNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorPais = Drop_paisNac.SelectedValue;

            if (valorPais != "")
            {
                Drop_edoNac.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", valorPais),
                    new SqlParameter("@Drop_valueEstado", "")
                    );

                Drop_edoNac.DataSource = sp_LoadDropPais;
                Drop_edoNac.DataTextField = "descEstado";
                Drop_edoNac.DataValueField = "idestado";
                Drop_edoNac.DataBind();

                Drop_cdNac.Items.Clear();
                Drop_cdNac.Enabled = false;


            }
            else
            {
                Drop_edoNac.Items.Clear();
                Drop_edoNac.Enabled = false;
                Drop_cdNac.Items.Clear();
                Drop_cdNac.Enabled = false;
            }
        }

        protected void Drop_edoNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorEstado = Drop_edoNac.SelectedValue;

            if (valorEstado != "")
            {

                Drop_cdNac.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", ""),
                    new SqlParameter("@Drop_valueEstado", valorEstado)
                    );

                Drop_cdNac.DataSource = sp_LoadDropPais;
                Drop_cdNac.DataTextField = "descCiudad";
                Drop_cdNac.DataValueField = "idciudad";
                Drop_cdNac.DataBind();
            }
            else
            {
                Drop_cdNac.Items.Clear();
                Drop_cdNac.Enabled = false;
            }
        }

        protected void Drop_paisDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorPais = Drop_paisDir.SelectedValue;

            if (valorPais != "")
            {
                Drop_edoDir.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", valorPais),
                    new SqlParameter("@Drop_valueEstado", "")
                    );

                Drop_edoDir.Items.Clear();
                //Drop_edoDir.Items.Add(new ListItem("-- Seleccionar --", ""));

                Drop_edoDir.DataSource = sp_LoadDropPais;
                Drop_edoDir.DataTextField = "descEstado";
                Drop_edoDir.DataValueField = "idestado";
                Drop_edoDir.DataBind();


            }
            else
            {

                Drop_edoDir.SelectedValue = "";
                Drop_edoDir.Enabled = false;

                Drop_cdDir.SelectedValue = "";
                Drop_cdDir.Enabled = false;

                Drop_codPostDir.SelectedValue = "";
                Drop_codPostDir.Enabled = false;

                Drop_colonia.SelectedValue = "";
                Drop_colonia.Enabled = false;

                Drop_calleDir.SelectedValue = "";
                Drop_calleDir.Enabled = false;
            }
        }

        protected void Drop_edoDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorEstado = Drop_edoDir.SelectedValue;

            if (valorEstado != "")
            {
                Drop_cdDir.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", ""),
                    new SqlParameter("@Drop_valueEstado", valorEstado)
                    );

                Drop_cdDir.Items.Clear();
                Drop_cdDir.Items.Add(new ListItem("-- Seleccionar --", ""));

                Drop_cdDir.DataSource = sp_LoadDropPais;
                Drop_cdDir.DataTextField = "descCiudad";
                Drop_cdDir.DataValueField = "idciudad";
                Drop_cdDir.DataBind();
            }
            else
            {

                Drop_cdDir.SelectedValue = "";
                Drop_cdDir.Enabled = false;

                Drop_codPostDir.SelectedValue = "";
                Drop_codPostDir.Enabled = false;

                Drop_colonia.SelectedValue = "";
                Drop_colonia.Enabled = false;

                Drop_calleDir.SelectedValue = "";
                Drop_calleDir.Enabled = false;
            }
        }

        private void Buscar()
        {
            var sp_buscar = DbUtil.GetCursor("sp_recursos_empleado_buscar_NombreNumero",
                new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                new SqlParameter("@nombreNumero", tb_buscarNumero.Text)
                );

            grid_buscar.DataSource = sp_buscar;
            grid_buscar.DataBind();
        }

        protected void grid_buscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_buscar.PageIndex = e.NewPageIndex;
            grid_buscar.DataBind();

            Buscar();
            tb_buscarNumero.Focus();
        }

        protected void bt_btnbuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            tb_buscarNumero.Focus();
        }

        protected void grid_buscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument); // Get the current row
                var GetNumero = grid_buscar.Rows[rowIndex].Cells[2].Text;

                Session.Add("GetNumero", GetNumero); 

                Response.Redirect("empleados_ver.aspx");
            }

            if (e.CommandName == "edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int GetNumero = Convert.ToInt32(grid_buscar.Rows[rowIndex].Cells[2].Text);

                Session.Add("GetNumero", GetNumero);

                Response.Redirect("empleados_editar.aspx");
            }
        }      
        protected void drop_numBaja_SelectedIndexChanged(object sender, EventArgs e)
        {

            modal_baja.Show();


            var valNumero = drop_numBaja.SelectedValue;

            if (valNumero != "")
            {
                var sp_mostrardato = DbUtil.ExecuteProc("sp_recursos_empleado_baja_mostrarinformacion",
                    new SqlParameter("@numero", valNumero),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                    );

                lb_NombreCompletoBaja.Text = sp_mostrardato["@nombre"].ToString();
                lb_depaCompletoBaja.Text = sp_mostrardato["@departamento"].ToString();
                lb_puesCompletoBaja.Text = sp_mostrardato["@puesto"].ToString();

                loadEquipo(valNumero);
            }
            else
            {
                lb_NombreCompletoBaja.Text = "Seleccione un empleado.";
                lb_depaCompletoBaja.Text = "Seleccione un empleado.";
                lb_puesCompletoBaja.Text = "Seleccione un empleado.";
            
                GV_equipo.DataSource = null;
                GV_equipo.DataBind();
            }
        }

        protected void loadEquipo(string emp)
        {
            var sp_equipo = DbUtil.GetCursor("sp_recursos_empleado_load_equipo",
           new SqlParameter("@no_empleado", emp)
           );

            GV_equipo.DataSource = sp_equipo;
            GV_equipo.DataBind();
        }

        protected void bajar_empleado_Click(object sender, ImageClickEventArgs e)
        {
            //Obtener el ultimo ID en la base de datos y sumarle un 1
            var sp_getUltimoIDBaja = DbUtil.ExecuteProc("sp_recursos_empleado_baja",
                new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                new SqlParameter("@no_empleado", ""),
                new SqlParameter("@id_causaBaja", ""),
                new SqlParameter("@comentarios_baja", ""),
                new SqlParameter("@fecha_egreso", ""),
                new SqlParameter("@fecha_registro", ""),
                new SqlParameter("@idBaja_final", ""),
                MsBarco.DbUtil.NewSqlParam("@ultimo_idBaja", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            var ultimoID = sp_getUltimoIDBaja["@ultimo_idBaja"].ToString();

            //Poniendo formato de fecha para la fecha de egreso
            //tb_egreso.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm"); //convertir el texto a formato de fecha datetime.now
            DateTime egreso = DateTime.Parse(tb_egreso.Text);

            //Obteniendo la fecha exacta del registro
            DateTime TimeNow = DateTime.Now;
            var TimeFinal = TimeNow.ToString("yyyy-MM-dd HH:mm:ss.fff");

            //variable para checar si esta seleccionado algun checkbox
            bool isAnySelected = cb_causa.SelectedIndex != -1;

            if (isAnySelected == true)
            {
                //Agregar a la base de datos la baja de los empleados con las diferentes causas
                foreach (ListItem item in cb_causa.Items)
                {
                    if (item.Selected)
                    {
                        var sp_empleado_BajaPersonal = DbUtil.ExecuteProc("sp_recursos_empleado_baja",
                            new SqlParameter("@no_empleado", drop_numBaja.Text),
                            new SqlParameter("@id_causaBaja", item.Value),
                            new SqlParameter("@comentarios_baja", tb_descBaja.Text),
                            new SqlParameter("@fecha_egreso", egreso),
                            new SqlParameter("@fecha_registro", TimeFinal),
                            new SqlParameter("@idBaja_final", ultimoID),
                            MsBarco.DbUtil.NewSqlParam("@ultimo_idBaja", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                            );
                    }
                }

                MultiView_baja.ActiveViewIndex = 1;

                baja_mensaje.Text = "El empleado '" + lb_NombreCompletoBaja.Text + "' ha sido dado de baja exitosamente.";
            }

            else
            {
                lb_cb_causa_aviso.Visible = true;
                lb_cb_causa_aviso.Text = "Selecciona una o más causas de bajas.";
            }
        }

        protected void baja_continuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }

        protected void btn_menuPrincipal_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void chkINFONAVIT_CheckedChanged(object sender, EventArgs e)
        {
            if(chkINFONAVIT.Checked==true)
            {
                lblNoCredito.Visible = true;
                txtNoCredito.Visible = true;
                lblTipoDescuento.Visible = true;
                ddlINFONAVIT.Visible = true;
                
            }

            else
            {
                lblNoCredito.Visible = false;
                txtNoCredito.Visible = false;
                lblTipoDescuento.Visible = false;
                ddlINFONAVIT.Visible = false;
                lblFactorINFONAVIT.Visible = false;
                txtFactorINFONAVIT.Visible = false;
                ddlINFONAVIT.SelectedValue = "";
            }
        }

        protected void ddlINFONAVIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = ddlINFONAVIT.SelectedValue;
            if (selected == "1")
            {
                lblFactorINFONAVIT.Visible = true;
                lblFactorINFONAVIT.Text = "Porcentaje (%): ";
                txtFactorINFONAVIT.Visible = true;
            }

            else if (selected == "2")
            {
                lblFactorINFONAVIT.Visible = true;
                lblFactorINFONAVIT.Text = "Cuota ($): ";
                txtFactorINFONAVIT.Visible = true;
            }

            else if (selected == "3")
            {
                lblFactorINFONAVIT.Visible = true;
                lblFactorINFONAVIT.Text = "Factor: ";
                txtFactorINFONAVIT.Visible = true;
            }

            else
            {
                lblFactorINFONAVIT.Visible = false;
                lblFactorINFONAVIT.Text = "";
                txtFactorINFONAVIT.Visible = false;
            }
        }

        protected void chkFONACOT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFONACOT.Checked == true)
            {
                lblNoFONACOT.Visible = true;
                txtNoFONACOT.Visible = true;
                lblRetencionFONACOT.Visible = true;
                txtRetencionFONACOT.Visible = true;
                lblTotalFONACOT.Visible = true;
                txtTotalFONACOT.Visible = true;
                lblFechaFONACOT.Visible = true;
                txtFechaFONACOT.Visible = true;
            }

            else
            {
                lblNoFONACOT.Visible = false;
                txtNoFONACOT.Visible = false;
                lblRetencionFONACOT.Visible = false;
                txtRetencionFONACOT.Visible = false;
                lblTotalFONACOT.Visible = false;
                txtTotalFONACOT.Visible = false;
                lblFechaFONACOT.Visible = false;
                txtFechaFONACOT.Visible = false;
            }
        }

        protected void chkPension_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPension.Checked == true)
            {
                lblNoPension.Visible = true;
                txtPension.Visible = true;
            }
            else
            {
                lblNoPension.Visible = false;
                txtPension.Visible = false;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Depto: " + Drop_depto.SelectedValue + " Puesto: " + Drop_puesto.SelectedValue + " Cliente: "+Drop_cliente.SelectedValue+" Puesto clientes: "+Drop_puestoCliente.SelectedValue+"');", true);
        }

        protected void Button3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("empleados_fotos.aspx");
        }

        protected void gridAlertas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gridAlertas.Rows[e.NewSelectedIndex];
            if (row.Cells[1].Text == "Empleados activos")
            {
                label_alerta.Text = "Empleados activos";
                idOpcion.Value = "Activos";
                loadGridViewDisplay("4");
            }
            if (row.Cells[1].Text == "Empleados sin foto")
            {
                    label_alerta.Text = "Empleados sin foto registrada";
                    idOpcion.Value = "Foto";
                    loadGridViewDisplay("2");
            }

            if (row.Cells[1].Text == "Empleados carta penal vencida")
            {
                    label_alerta.Text = "Empleados con carta penal vencida";
                    idOpcion.Value = "Penal";
                loadGridViewDisplay("3");
            }

            if (row.Cells[1].Text == "Empleados APTO vencido")
            {
                label_alerta.Text = "Empleados con APTO vencido";
                idOpcion.Value = "APTO";
                loadGridViewDisplay("5");
            }

            if (row.Cells[1].Text == "Empleados gafete &#250;nico vencido")
            {
                label_alerta.Text = "Empleados con gafete único vencido";
                idOpcion.Value = "Gafete";
                loadGridViewDisplay("6");
            }
            ModalDatos.Show();
        }

        protected void grid_alertas_display_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (idOpcion.Value == "Foto")
            {
                grid_alertas_display.PageIndex = e.NewPageIndex;
                loadGridViewDisplay("2");
            }
            if (idOpcion.Value == "Penal")
            {
                grid_alertas_display.PageIndex = e.NewPageIndex;
                loadGridViewDisplay("3");
            }
            if (idOpcion.Value == "Activos")
            {
                grid_alertas_display.PageIndex = e.NewPageIndex;
                loadGridViewDisplay("4");
            }
            if (idOpcion.Value == "APTO")
            {
                grid_alertas_display.PageIndex = e.NewPageIndex;
                loadGridViewDisplay("5");
            }
            if (idOpcion.Value == "Gafete")
            {
                grid_alertas_display.PageIndex = e.NewPageIndex;
                loadGridViewDisplay("6");
            }
            ModalDatos.Show();
        }

        protected void gridAlertas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].CssClass = "boton_alerta";
                int cantidad = Convert.ToInt32(e.Row.Cells[2].Text);
                if (e.Row.Cells[1].Text != "Empleados activos")
                {
                    if (cantidad >0 )
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#DF4444");
                        e.Row.Cells[2].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                    else
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#79A22B");
                        e.Row.Cells[2].ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    }
                }
            }    
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalDatos.Hide();
        }

        protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
        {
            if (Drop_depto.SelectedValue != "" && Drop_puesto.SelectedValue != "")
            {
                MaxDate();
                lbl_nombre.Text = tb_nombre.Text;
                lbl_apellido_paterno.Text = tb_apepat.Text;
                lbl_apellido_materno.Text = tb_apemat.Text;
                lbl_fecha_nacimiento.Text = tb_fecnac.Text;
                lbl_curp.Text = tb_curp.Text;
                lbl_rfc.Text = tb_rfc.Text;
                lbl_imss.Text = tb_imss.Text;
                lbl_departamento.Text = Drop_depto.SelectedItem.Text;
                lbl_puesto.Text = Drop_puesto.SelectedItem.Text;
            }
            else
            {
                lbl_alerta.Text = "Faltan campos por completar.";
            }

            modalpop_Alta.Show();
        }

        protected void Button2_Click(object sender, ImageClickEventArgs e)
        {
            modal_baja.Show();
        }

        protected void Drop_cdDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Drop_cdDir.SelectedValue != "")
            {
                if (Drop_cdDir.SelectedValue == "61" || Drop_cdDir.SelectedValue == "1464")
                {

                    tb_calle.Visible = false;
                    Drop_calleDir.Visible = true;
                    Drop_calleDir.SelectedValue = "";

                    tb_colonia.Visible = false;
                    Drop_colonia.Visible = true;
                    Drop_colonia.SelectedValue = "";


                    tb_codpostDir.Visible = false;
                    Drop_codPostDir.Visible = true;


                    Drop_codPostDir.Enabled = true;

                    var sp_loadCod = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropCodigosPostales",
                        new SqlParameter("@idCiudad", Drop_cdDir.SelectedValue)
                        );

                    Drop_codPostDir.Items.Clear();

                    Drop_codPostDir.DataSource = sp_loadCod;
                    Drop_codPostDir.DataTextField = "cod_pos";
                    Drop_codPostDir.DataValueField = "cod_pos";
                    Drop_codPostDir.DataBind();

                    Drop_codPostDir.Items.Add(new ListItem("-- Seleccionar --", ""));
                    Drop_codPostDir.SelectedValue = "";
                }
                else
                {
                    
                        tb_calle.Visible = true;
                        Drop_calleDir.Visible = false;

                        tb_colonia.Visible = true;
                        Drop_colonia.Visible = false;

                        tb_codpostDir.Visible = true;
                        Drop_codPostDir.Visible = false;
                    
                }             
            }
            else
            {
                Drop_codPostDir.SelectedValue = "";
                Drop_codPostDir.Enabled = false;

                Drop_colonia.SelectedValue = "";
                Drop_colonia.Enabled = false;

                Drop_calleDir.SelectedValue = "";
                Drop_calleDir.Enabled = false;
            }
        }

        protected void Drop_colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Drop_colonia.SelectedValue != "")
            {
                var sp_loadCalles = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropCalles",
                    new SqlParameter("@Colonia", Drop_colonia.SelectedItem.Text),
                    new SqlParameter("@idCiudad", Drop_cdDir.Text),
                    new SqlParameter("@codigoPostal", Drop_codPostDir.Text)

               );

                Drop_calleDir.Items.Clear();
                Drop_calleDir.Items.Add(new ListItem("-- Seleccionar --", ""));

                Drop_calleDir.DataSource = sp_loadCalles;
                Drop_calleDir.DataTextField = "descripcion";
                Drop_calleDir.DataValueField = "id_calle";
                Drop_calleDir.DataBind();

                Drop_calleDir.Enabled = true;
                //Drop_calleDir.SelectedValue = "";
            }
            else
            {
                Drop_calleDir.SelectedValue = "";
                Drop_calleDir.Enabled = false;
            }
        }

        protected void Drop_codPostDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Drop_codPostDir.SelectedValue != "")
            {
              
                var sp_loadColonia = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropColonias",
                    new SqlParameter("@codPostal", Drop_codPostDir.SelectedValue)
                    );

                Drop_colonia.Items.Clear();
                Drop_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));

                Drop_colonia.DataSource = sp_loadColonia;
                Drop_colonia.DataTextField = "descripcion";
                Drop_colonia.DataValueField = "descripcion";
                Drop_colonia.DataBind();

                //Drop_colonia.SelectedValue = "";

                Drop_colonia.Enabled = true;


            }
            else
            {


                Drop_colonia.SelectedValue = "";
                Drop_colonia.Enabled = false;

                Drop_calleDir.SelectedValue = "";
                Drop_calleDir.Enabled = false;
            }
        }
    }
}