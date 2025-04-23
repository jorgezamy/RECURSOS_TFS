using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.Globalization;
using System.Text.RegularExpressions;

namespace recursos.Views
{
    public partial class empleados_editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    LoadMenuRoles();

                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    loadcolonias();
                    LoadDrops();
                    loadDatos();

                    loadCheckboxlist();

                    LoadCheckboxLicencia();

                    MV_EditarEmpleados.ActiveViewIndex = 0;

                    LoadChkBonosEmpleado();

                    mostrarReactivar();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        private void MaxDate()
        {
            btn_cerrarPopUp_guardar_si.Visible = true;

            lbl_alerta_editar.Text = "";

            DateTime dt1;
            DateTime dt2;
            DateTime limite = DateTime.Now;


            if (tb_policialVig.Text != "")
            {
                dt1 = DateTime.ParseExact(tb_policialVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                if (dt1.Date > limite)
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "La carta policial no puede ser mayor a la fecha actual.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }


            if (tb_penal.Text != "")
            {
                if (tb_penalVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_penalVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(1))
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia de carta penal no puede ser superior a 1 año.";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Carta penal no tiene fecha de vigencia registrada.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }
            else
            {
                if (tb_penalVig.Text!= "")
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "La carta penal no tiene numero de folio.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }

            if (tb_licencia.Text != "")
            {
                if (tb_licenciaVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_licenciaVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(4))
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia de licencia de conducir no puede ser superior a 4 años.";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Carta de licencia de conducir no tiene fecha de vigencia registrada.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }
            else
            {
                if (tb_licenciaVig.Text != "")
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso el numero de licencia de conducir.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }

            if (tb_apto.Text != "")
            {
                if (tb_apto_vigencia.Text != "")
                {
                    if (tb_apto_inicio.Text != "")
                    {
                        dt1 = DateTime.ParseExact(tb_apto_vigencia.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                        DateTime dt3 = DateTime.ParseExact(tb_apto_inicio.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);

                        if (dt1.Date < dt3.Date)
                        {
                            lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia de APTO no puede ser menor a la de inicio.";
                            btn_cerrarPopUp_guardar_si.Visible = false;
                        }
                        else
                        {
                            if (dt1.Date > limite.Date.AddYears(2))
                            {
                                lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia de APTO no puede ser superior a 2 años.";
                                btn_cerrarPopUp_guardar_si.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "APTO no tiene fecha de inicio registrada.";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "APTO no tiene fecha de vigencia registrada.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }
            else
            {
                if (tb_apto_vigencia.Text!="" || tb_apto_inicio.Text != "")
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso el numero de apto.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }

            if (tb_fast.Text != "")
            {
                if (tb_fastVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_fastVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(6))
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia de FAST no puede ser superior a 5 años.";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "FAST no tiene fecha de vigencia registrada.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }
            else
            {
                if (tb_fastVig.Text != "")
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso el numero de FAST";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }

            if (tb_visa.Text != "")
            {
                if (tb_visaVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_visaVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(10))
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia de VISA no puede ser superior a 10 años.";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se registro fecha de vigencia de VISA.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }
            else
            {
                if (tb_visaVig.Text != "")
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso el numero de VISA.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }

            if (tb_servNo.Text != "")
            {
                if (tb_servNoVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_servNoVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(1))
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia del recibo de servicio no puede ser superior a 1 año.";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Recibo de servicio no tiene fecha de vigencia.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }
            else
            {
                if (tb_servNoVig.Text != "")
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso el numero de recibo de servicio";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }

            if (tb_gafete_unico.Text != "")
            {
                if (tb_gafete_unicoVig.Text != "")
                {
                    dt1 = DateTime.ParseExact(tb_gafete_unicoVig.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                    if (dt1.Date > limite.Date.AddYears(1))
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Vigencia de gafete único no puede ser superior a 1 año.";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                }
                else
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Gafete unico no tiene fecha de vigencia.";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
            }
            else
            {
                if (tb_gafete_unicoVig.Text != "")
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso el numero de gafete unico";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }

            }


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
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se selecciono un tipo de licencia";
                    btn_cerrarPopUp_guardar_si.Visible = false;
                }
                else
                {
                    if (tb_constancia_inicio.Text == "")
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso fecha de inicio de constancia";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }

                    if (tb_constancia_fin.Text == "")
                    {
                        lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se ingreso fecha de fin de constancia";
                        btn_cerrarPopUp_guardar_si.Visible = false;
                    }
                    else
                    {
                        dt1 = DateTime.ParseExact(tb_constancia_inicio.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);
                         dt2 = DateTime.ParseExact(tb_constancia_fin.Text, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None);

                        if (dt1.Date > limite)
                        {
                            lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "La fecha de inicio de constancia no puede ser mayor a fecha actual";
                            btn_cerrarPopUp_guardar_si.Visible = false;
                        }
                        if (dt1.Date > dt2.Date)
                        {
                            lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "Fecha de vigencia de constancia es menor que la de inicio";
                            btn_cerrarPopUp_guardar_si.Visible = false;
                        }
                    }
                }
            }
            else
            {
                if (check == true)
                {
                    lbl_alerta_editar.Text = lbl_alerta_editar.Text + "<br />" + "No se registro folio de constancia del tipo de licencia";
                    btn_cerrarPopUp_guardar_si.Visible = false;
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

        private void LoadCheckboxLicencia()
        {

            var sp_loadSelected = DbUtil.ExecuteProc("sp_recursos_empleados_editar_loadCheckBoxList_tipo_licencia",
                new SqlParameter("@no_empleado", Session["GetNumero"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@cadena", null, SqlDbType.VarChar, ParameterDirection.Output, 200)
                );

            string str = sp_loadSelected["@cadena"].ToString();

            string[] array = str.Split(',');

            int MaxValue = array.GetUpperBound(0);

            foreach (ListItem listItem in checkBox_tipo_licencia.Items)
            {
                for (int i = 0; i <= MaxValue; i++)
                {
                    if (array[i] == listItem.Text)
                    {
                        listItem.Selected = true;
                    }
                }
            }
        }

        private void LoadMenuRoles()
        {
            var usuario = Session["usuario"].ToString();

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_empleado_editar_roles_permitirAcceso",
                new SqlParameter("@usuario", usuario),
                MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                //tiene acceso al modulo
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("empleados.aspx");
            }
        }

        private void LoadChkBonosEmpleado()
        {
            var numerotabla = Session["GetNumero"].ToString();

            var sp_editar_loadDatos_loadBonos = DbUtil.ExecuteProc("sp_recursos_empleado_editar_LoadDatos_LoadBonos",
                new SqlParameter("@no_empleado", numerotabla),
                MsBarco.DbUtil.NewSqlParam("@bonosGeneral", null, SqlDbType.VarChar, ParameterDirection.Output, 200),
                MsBarco.DbUtil.NewSqlParam("@bonosEmpleado", null, SqlDbType.VarChar, ParameterDirection.Output, 200)
                );

            string[] bonosGeneral = sp_editar_loadDatos_loadBonos["@bonosGeneral"].ToString().Split(',');
            List<string> listaBonosGeneral = new List<string>(); //make a new string list    
            listaBonosGeneral.AddRange(bonosGeneral);

            string[] bonosEmpleado = sp_editar_loadDatos_loadBonos["@bonosEmpleado"].ToString().Split(',');
            List<string> listabonosEmpleado = new List<string>(); //make a new string list    
            listabonosEmpleado.AddRange(bonosEmpleado);

            //List<string> availableItems = new List<string> { sp_editar_loadDatos_loadBonos["@bonosGeneral"].ToString() };
            //List<string> selectedItems = new List<string> { sp_editar_loadDatos_loadBonos["@bonosEmpleado"].ToString() };

            // add available items to checkboxlist control
            foreach (string item in listaBonosGeneral)
                chklBonos.Items.Add(new ListItem(item));
            // check pre-selected items
            var query = from ListItem listItem in chklBonos.Items
                        join item in listabonosEmpleado
                        on listItem.Value equals item
                        select listItem;
            foreach (ListItem listItem in query)
                listItem.Selected = true;

            if(drop_puesto.SelectedValue == "6.1.1.5")
            {
                rowBonos.Visible = true;
            }

            else
            {
                rowBonos.Visible = false;
            }
        }

        private void loadcolonias()
        {
            var sp_loadColonias = DbUtil.GetCursor("sp_recursos_empleado_add_seleccionarColonias");

            Drop_colonia.DataSource = sp_loadColonias;
            Drop_colonia.DataValueField = "colonia";
            Drop_colonia.DataTextField = "colonia";
            Drop_colonia.DataBind();
        }

        private void loadDatos()
         {
            var numerotabla = Session["GetNumero"].ToString();

            var sp_editar_loadDatos = DbUtil.ExecuteProc("sp_recursos_empleado_editar_LoadDatos",
                new SqlParameter("@numero", numerotabla),
                MsBarco.DbUtil.NewSqlParam("@no_empleado", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@puesto_cliente", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@idCliente", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                //MsBarco.DbUtil.NewSqlParam("@id_tipoMovimiento", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@id_zona", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@supervisor", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@telefono", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@email", null, SqlDbType.VarChar, ParameterDirection.Output, 35),
                MsBarco.DbUtil.NewSqlParam("@ciudad", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@estado", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@pais", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@calle", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                MsBarco.DbUtil.NewSqlParam("@calle_num", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@codpost", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@colonia", null, SqlDbType.VarChar, ParameterDirection.Output, 90),
                MsBarco.DbUtil.NewSqlParam("@tallaC", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@tallaP", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@fec_nac", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@sexo", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@civil", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@ninos", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@cdNac", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@edoNac", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@escolaridad", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@escolaridad_documento", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@escolaridad_institucion", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@paisNac", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@carrera", null, SqlDbType.VarChar, ParameterDirection.Output, 35),
                MsBarco.DbUtil.NewSqlParam("@ingles", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@nom_padre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@nom_madre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@servicio", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@servicio_num", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@servicio_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@curp", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@rfc", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                //MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@pasaporte", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@pasaporte_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@visa", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@visa_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@licencia", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@licencia_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@fast", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@fast_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@penal", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@penal_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                //MsBarco.DbUtil.NewSqlParam("@policial", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@policial_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@esChofer", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@apto", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@apto_inicio", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@apto_fin", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@contacto", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@contacto_telefono", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@gafete_unico", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@gafete_unicoVig", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@existePaisDir", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@existePaisNac", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@validarINFONAVIT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@noINFONAVIT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@tipoINFONAVIT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@factorINFONAVIT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@validarFONACOT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@noFONACOT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@descuentoFONACOT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@totalFONACOT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@inicioFONACOT", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@validarPension", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@descuentoPension", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@folio_constancia", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@constancia_inicio", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@constancia_fin", null, SqlDbType.VarChar, ParameterDirection.Output, 25)

                );

            txtIMSS.Text = sp_editar_loadDatos["@imss"].ToString();

            if (sp_editar_loadDatos["@validarINFONAVIT"].ToString() == "1")
            {
                chkINFONAVIT.Checked = true;
                txtNoCredito.Text = sp_editar_loadDatos["@noINFONAVIT"].ToString();
                ddlINFONAVIT.SelectedValue = sp_editar_loadDatos["@tipoINFONAVIT"].ToString();
                txtFactorINFONAVIT.Text = sp_editar_loadDatos["@factorINFONAVIT"].ToString();
            }

            else
            {
                chkINFONAVIT.Checked = false;
            }

            if (sp_editar_loadDatos["@id_zona"].ToString() != "")
            {
                drop_zonas.SelectedValue = sp_editar_loadDatos["@id_zona"].ToString();
            }

            if (sp_editar_loadDatos["@validarFONACOT"].ToString() == "1")
            {
                chkFONACOT.Checked = true;
                txtNoFONACOT.Text = sp_editar_loadDatos["@noFONACOT"].ToString();
                txtRetencionFONACOT.Text = sp_editar_loadDatos["@descuentoFONACOT"].ToString();
                txtTotalFONACOT.Text = sp_editar_loadDatos["@totalFONACOT"].ToString();
                txtFechaFONACOT.Text = sp_editar_loadDatos["@inicioFONACOT"].ToString();
            }
            else
            {
                chkFONACOT.Checked = false;
            }

            if (sp_editar_loadDatos["@validarPension"].ToString() == "1")
            {
                chkPension.Checked = true;
                txtPension.Text = sp_editar_loadDatos["@descuentoPension"].ToString();
            }

            else
            {
                chkPension.Checked = false;
            }

            var prueba = sp_editar_loadDatos["@esChofer"].ToString();
            var tipo = sp_editar_loadDatos["@puesto_cliente"].ToString();

            if (sp_editar_loadDatos["@esChofer"].ToString() == "6.1.1.5")
            {
                drop_cliente.SelectedValue = sp_editar_loadDatos["@idCliente"].ToString();
                drop_puestoCliente.SelectedValue = sp_editar_loadDatos["@puesto_cliente"].ToString();

            }



            if (sp_editar_loadDatos["@existePaisDir"].ToString() == "1")
            {
                drop_edoDir.SelectedValue = sp_editar_loadDatos["@estado"].ToString();
                drop_paisDir.SelectedValue = sp_editar_loadDatos["@pais"].ToString();

                drop_cdDir.SelectedValue = sp_editar_loadDatos["@ciudad"].ToString();

            }



            if (drop_cdDir.SelectedValue == "61" || drop_cdDir.SelectedValue == "1464")
            {
                tb_calle.Visible = false;
                Drop_calleDir.Visible = true;

                tb_colonia.Visible = false;
                Drop_colonia.Visible = true;


                tb_codPos.Visible = false;
                Drop_codPostDir.Visible = true;

                //Drop_codPostDir.SelectedValue = sp_editar_loadDatos["@ciudad"].ToString();

                /** -- Cargar Codigo Postal  -- **/
                var sp_editar_DropDownLists_codPost = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropCodigosPostales",
                 new SqlParameter("@idCiudad", sp_editar_loadDatos["@ciudad"].ToString())
                );

                Drop_codPostDir.DataSource = sp_editar_DropDownLists_codPost;
                Drop_codPostDir.DataTextField = "cod_pos";
                Drop_codPostDir.DataValueField = "cod_pos";
                Drop_codPostDir.DataBind();

                Drop_codPostDir.SelectedValue = sp_editar_loadDatos["@codpost"].ToString();

                if (sp_editar_loadDatos["@calle"].ToString() != "")
                {
                    var sp_loadCalles = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropCalles",
                        new SqlParameter("@Colonia", sp_editar_loadDatos["@colonia"].ToString()),
                        new SqlParameter("@idCiudad", sp_editar_loadDatos["@ciudad"].ToString()),
                         new SqlParameter("@codigoPostal", sp_editar_loadDatos["@codpost"].ToString())
                         
                    );

                    Drop_calleDir.Items.Clear();
                    Drop_calleDir.Items.Add(new ListItem("-- Seleccionar --", ""));

                    Drop_calleDir.DataSource = sp_loadCalles;
                    Drop_calleDir.DataTextField = "descripcion";
                    Drop_calleDir.DataValueField = "id_calle";
                    Drop_calleDir.DataBind();

                    Drop_calleDir.Enabled = true;

                    if (Drop_calleDir.Items.FindByText(sp_editar_loadDatos["@calle"].ToString()) != null)
                    {
                        Drop_calleDir.SelectedItem.Text = sp_editar_loadDatos["@calle"].ToString();
                    }
                    else
                    {
                        Drop_calleDir.SelectedValue = "";
                    }
                }

                var sp_loadColonia = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropColonias",
                    new SqlParameter("@codPostal", Drop_codPostDir.SelectedValue)
                    );

                Drop_colonia.DataSource = sp_loadColonia;
                Drop_colonia.DataTextField = "descripcion";
                Drop_colonia.DataValueField = "descripcion";
                Drop_colonia.DataBind();

                Drop_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));

                if (Drop_colonia.Items.FindByValue(sp_editar_loadDatos["@colonia"].ToString()) != null)
                {
                    Drop_colonia.SelectedValue = sp_editar_loadDatos["@colonia"].ToString();
                }
                else
                {
                    Drop_colonia.Items.Insert(0, new ListItem("-- Seleccionar --", "na"));
                    Drop_colonia.Items.FindByText("-- Seleccionar --").Selected = true;
                }

            }
            else
            {
                tb_calle.Visible = true;
                Drop_calleDir.Visible = false;

                tb_colonia.Visible = true;
                Drop_colonia.Visible = false;


                tb_codPos.Visible = true;
                Drop_codPostDir.Visible = false;

                tb_calle.Text = sp_editar_loadDatos["@calle"].ToString();
                tb_colonia.Text = sp_editar_loadDatos["@colonia"].ToString();
                tb_codPos.Text = sp_editar_loadDatos["@codpost"].ToString(); 
            }


            if (sp_editar_loadDatos["@existePaisDir"].ToString() != "1")
            {
                drop_edoDir.Enabled = false;
                drop_cdDir.Enabled = false;
            }

            if (sp_editar_loadDatos["@existePaisNac"].ToString() == "1")
            {
                drop_cdNac.SelectedValue = sp_editar_loadDatos["@cdNac"].ToString();
                drop_edoNac.SelectedValue = sp_editar_loadDatos["@edoNac"].ToString();
                drop_paisNac.SelectedValue = sp_editar_loadDatos["@paisNac"].ToString();
            }

            if (sp_editar_loadDatos["@existePaisNac"].ToString() != "1")
            {
                drop_edoNac.Enabled = false;
                drop_cdNac.Enabled = false;
            }

            lb_numero.Text = sp_editar_loadDatos["@no_empleado"].ToString();
            nombre.Text = sp_editar_loadDatos["@nombre"].ToString();

            if (sp_editar_loadDatos["@departamento"].ToString() == "0")
            {
                drop_depto.SelectedValue = "";
            }
            else
            {
                drop_depto.SelectedValue = sp_editar_loadDatos["@departamento"].ToString();
            }

            drop_puesto.SelectedValue = sp_editar_loadDatos["@puesto"].ToString();
            drop_supervisor.SelectedValue = sp_editar_loadDatos["@supervisor"].ToString();
            tb_telefono.Text = sp_editar_loadDatos["@telefono"].ToString();
            tb_correo.Text = sp_editar_loadDatos["@email"].ToString();

           

            //tb_calle.Text = sp_editar_loadDatos["@calle"].ToString();
            //tb_CalleNo.Text = sp_editar_loadDatos["@calle_num"].ToString();
            if (sp_editar_loadDatos["@calle_num"].ToString() != "")
            {
                if (sp_editar_loadDatos["@calle_num"].ToString().Contains(" - "))
                    {
                    // Taking a string 
                    String str = sp_editar_loadDatos["@calle_num"].ToString();

                    String[] separator = { " - "};
                    Int32 count = 2;

                    // using the method 
                    String[] strlist = str.Split(separator, count,
                           StringSplitOptions.RemoveEmptyEntries);
                    
                    if (str != " - ")
                    {
                        tb_CalleNo.Text = strlist[0];
                        tb_calleInt.Text = strlist[1];
                    }
                    else
                    {
                        tb_CalleNo.Text = "";
                        tb_calleInt.Text = "";
                    }

                }
                    else
                    {
                        tb_CalleNo.Text = sp_editar_loadDatos["@calle_num"].ToString();
                    }                
            }



            //tb_codPos.Text = sp_editar_loadDatos["@codpost"].ToString();
            //tb_colonia.Text = sp_editar_loadDatos["@colonia"].ToString();

            rb_sexo.SelectedValue = sp_editar_loadDatos["@sexo"].ToString();
            drop_tallaC.SelectedValue = sp_editar_loadDatos["@tallaC"].ToString();
            tb_fecNac.Text = sp_editar_loadDatos["@fec_nac"].ToString(); 
            drop_civil.SelectedValue = sp_editar_loadDatos["@civil"].ToString();
            tb_noNinos.Text = sp_editar_loadDatos["@ninos"].ToString();
            tb_carrera.Text = sp_editar_loadDatos["@carrera"].ToString();
            tb_ingles.Text = sp_editar_loadDatos["@ingles"].ToString();
            tb_nomPat.Text = sp_editar_loadDatos["@nom_padre"].ToString();
            tb_nomMat.Text = sp_editar_loadDatos["@nom_madre"].ToString();
            drop_servicioTipo.SelectedValue = sp_editar_loadDatos["@servicio"].ToString();
            tb_servNo.Text = sp_editar_loadDatos["@servicio_num"].ToString();
            tb_servNoVig.Text = sp_editar_loadDatos["@servicio_vig"].ToString();
            lb_curpNo.Text = sp_editar_loadDatos["@curp"].ToString();
            tb_rfc.Text = sp_editar_loadDatos["@rfc"].ToString();
            //tb_imss.Text = sp_editar_loadDatos["@imss"].ToString();
            tb_pasaporte.Text = sp_editar_loadDatos["@pasaporte"].ToString();
            tb_pasaporteVig.Text = sp_editar_loadDatos["@pasaporte_vig"].ToString();
            tb_visa.Text = sp_editar_loadDatos["@visa"].ToString();
            tb_visaVig.Text = sp_editar_loadDatos["@visa_vig"].ToString();
            tb_licencia.Text = sp_editar_loadDatos["@licencia"].ToString();
            tb_licenciaVig.Text = sp_editar_loadDatos["@licencia_vig"].ToString();
            tb_fast.Text = sp_editar_loadDatos["@fast"].ToString();
            tb_fastVig.Text = sp_editar_loadDatos["@fast_vig"].ToString();
            tb_penal.Text = sp_editar_loadDatos["@penal"].ToString();
            tb_penalVig.Text = sp_editar_loadDatos["@penal_vig"].ToString();
            //tb_policial.Text = sp_editar_loadDatos["@policial"].ToString();
            tb_policialVig.Text = sp_editar_loadDatos["@policial_vig"].ToString();
            drop_escolaridad.SelectedValue = sp_editar_loadDatos["@escolaridad"].ToString();
            drop_escolaridad_documento.SelectedValue = sp_editar_loadDatos["@escolaridad_documento"].ToString();
            drop_escolaridad_institucion.SelectedValue = sp_editar_loadDatos["@escolaridad_institucion"].ToString();


            // Cargar apto si tiene registrado alguno
            tb_apto.Text = sp_editar_loadDatos["@apto"].ToString();
            tb_apto_inicio.Text = sp_editar_loadDatos["@apto_inicio"].ToString();
            tb_apto_vigencia.Text = sp_editar_loadDatos["@apto_fin"].ToString();

            // Cargar contacto si tiene registrado alguno
            tb_contacto.Text = sp_editar_loadDatos["@contacto"].ToString();
            tb_contacto_telefono.Text = sp_editar_loadDatos["@contacto_telefono"].ToString();

            // Cargar contacto si tiene registrado alguno
            tb_gafete_unico.Text = sp_editar_loadDatos["@gafete_unico"].ToString();
            tb_gafete_unicoVig.Text = sp_editar_loadDatos["@gafete_unicoVig"].ToString();

            //Carga constancia de licencia
            tb_constancia.Text = sp_editar_loadDatos["@folio_constancia"].ToString();
            tb_constancia_inicio.Text = sp_editar_loadDatos["@constancia_inicio"].ToString();
            tb_constancia_fin.Text = sp_editar_loadDatos["@constancia_fin"].ToString();


            // Cargar talla de pantalon dependiendo de si se encontro que el empleado es Masculino o Femenino
            if (rb_sexo.Text == "F")
            {
                drop_tallaP.Items.Clear();
                drop_tallaP.Items.Add(new ListItem("--Seleccionar--", ""));

                for (int i = 1; i <= 15; i += 2)
                {
                    drop_tallaP.Items.Add(i.ToString());
                }
            }

            if (rb_sexo.Text == "M")
            {
                drop_tallaP.Items.Clear();
                drop_tallaP.Items.Add(new ListItem("--Seleccionar--", ""));

                for (int i = 28; i <= 42; i += 2)
                {
                    drop_tallaP.Items.Add(i.ToString());
                }
            }

            //Despues de haber cargado la lista de las tallas de pantalon dependiendo del sexo se selecciona la talla que tenga registrado en la BD
            drop_tallaP.SelectedValue = sp_editar_loadDatos["@tallaP"].ToString();

            ////var sp_editar_loadDatos_loadBonos = DbUtil.ExecuteProc("sp_recursos_empleado_editar_LoadDatos_LoadBonos",
            ////        MsBarco.DbUtil.NewSqlParam("@descripcion", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
            ////    );

            ////chklBonos.DataSource = sp_editar_loadDatos_loadBonos;
            ////chklBonos.DataValueField = "id_bono_operador";
            ////chklBonos.DataTextField = "descripcion";
            ////chklBonos.DataBind();

            ////sp_editar_loadDatos_loadBonos["@descripcion"].ToString();

        }

        private void LoadDrops()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Current.Session["GetNumero"] as string))
            //{
            var numeroTabla = Session["GetNumero"].ToString();

            /****************************** Obtener Puesto ******************************/
            var sp_editar_DropDownLists0 = DbUtil.ExecuteProc("sp_recursos_empleado_editar_DropDownLists",
                    new SqlParameter("@numero", numeroTabla),
                    new SqlParameter("@numeroPuesto", ""),
                    new SqlParameter("@numeroCliente", ""),
                    new SqlParameter("@numeroTractor", ""),
                    MsBarco.DbUtil.NewSqlParam("@noesChofer", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

            var esChofer = sp_editar_DropDownLists0["@noesChofer"].ToString();

            /****************************** Obtener Puesto ******************************/
            var sp_editar_DropDownLists = DbUtil.GetCursor("sp_recursos_empleado_editar_DropDownLists",
                        new SqlParameter("@numero", ""),
                        new SqlParameter("@numeroPuesto", numeroTabla),
                        new SqlParameter("@numeroCliente", ""),
                        new SqlParameter("@numeroTractor", ""),
                        MsBarco.DbUtil.NewSqlParam("@noesChofer", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                        );

            drop_puesto.DataSource = sp_editar_DropDownLists;
            drop_puesto.DataValueField = "IdPuesto";
            drop_puesto.DataTextField = "DescPuesto";
            drop_puesto.DataBind();

            if (esChofer == "1")
            {
                /****************************** Obtener Cliente ******************************/
                var sp_editar_DropDownLists1 = DbUtil.GetCursor("sp_recursos_empleado_editar_DropDownLists",
                    new SqlParameter("@numero", ""),
                    new SqlParameter("@numeroPuesto", ""),
                    new SqlParameter("@numeroCliente", numeroTabla),
                    new SqlParameter("@numeroTractor", ""),
                    MsBarco.DbUtil.NewSqlParam("@noesChofer", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                drop_cliente.DataSource = sp_editar_DropDownLists1;
                drop_cliente.DataTextField = "PuestoClienteCliente";
                drop_cliente.DataValueField = "idPuestoClienteCliente";
                drop_cliente.DataBind();

                /****************************** Obtener Tractor - Tipo de movimiento ******************************/
                var sp_editar_DropDownLists2 = DbUtil.GetCursor("sp_recursos_empleado_editar_DropDownLists",
                    new SqlParameter("@numero", ""),
                    new SqlParameter("@numeroPuesto", ""),
                    new SqlParameter("@numeroCliente", ""),
                    new SqlParameter("@numeroTractor", numeroTabla),
                    MsBarco.DbUtil.NewSqlParam("@noesChofer", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                drop_puestoCliente.DataSource = sp_editar_DropDownLists2;
                drop_puestoCliente.DataTextField = "tipoMovimiento";
                drop_puestoCliente.DataValueField = "IdPuestoClientes";
                drop_puestoCliente.DataBind();

                var sp_loadTieneTractor = DbUtil.ExecuteProc("sp_recursos_empleado_editar_tieneTractorAsignado",
                    new SqlParameter("@noEmpleado", numeroTabla),
                    MsBarco.DbUtil.NewSqlParam("@tieneTractor", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                if (sp_loadTieneTractor["@tieneTractor"].ToString() == "1")
                {
                    drop_depto.Enabled = false;
                    drop_puesto.Enabled = false;
                    drop_cliente.Enabled = false;
                    drop_puestoCliente.Enabled = false;
                }
            }
            else
            {
                drop_cliente.Enabled = false;
                drop_puestoCliente.Enabled = false;

                row_movimiento_drop.Visible = false;
                row_movimiento_lb.Visible = false;

                row_cliente_lb.Visible = false;
                row_cliente_drop.Visible = false;
            }

            /****************************** Obtener Estados Direccion ******************************/
            var sp_editar_DropDownLists_Edo_Dir = DbUtil.GetCursor("sp_recursos_empleado_editar_DropDownLists_EdoCd",
                    new SqlParameter("@numeroEstadosDir", numeroTabla),
                    new SqlParameter("@numeroCiudadesDir", ""),
                    new SqlParameter("@numeroEstadosNac", ""),
                    new SqlParameter("@numeroCiudadesNac", "")
                    );

            drop_edoDir.DataSource = sp_editar_DropDownLists_Edo_Dir;
            drop_edoDir.DataTextField = "DescEdo";
            drop_edoDir.DataValueField = "IdEstado";
            drop_edoDir.DataBind();

            /****************************** Obtener Ciudades Direccion ******************************/
            var sp_editar_DropDownLists_Cd_Dir = DbUtil.GetCursor("sp_recursos_empleado_editar_DropDownLists_EdoCd",
                new SqlParameter("@numeroEstadosDir", ""),
                new SqlParameter("@numeroCiudadesDir", numeroTabla),
                new SqlParameter("@numeroEstadosNac", ""),
                new SqlParameter("@numeroCiudadesNac", "")
                );

            drop_cdDir.DataSource = sp_editar_DropDownLists_Cd_Dir;
            drop_cdDir.DataTextField = "DescCd";
            drop_cdDir.DataValueField = "IdCiudad";
            drop_cdDir.DataBind();

            /****************************** Obtener Estados Nacimiento ******************************/
            var sp_editar_DropDownLists_Edo_Nac = DbUtil.GetCursor("sp_recursos_empleado_editar_DropDownLists_EdoCd",
                    new SqlParameter("@numeroEstadosDir", ""),
                    new SqlParameter("@numeroCiudadesDir", ""),
                    new SqlParameter("@numeroEstadosNac", numeroTabla),
                    new SqlParameter("@numeroCiudadesNac", "")
                    );

            drop_edoNac.DataSource = sp_editar_DropDownLists_Edo_Nac;
            drop_edoNac.DataTextField = "DescEdo";
            drop_edoNac.DataValueField = "IdEstado";
            drop_edoNac.DataBind();

            /****************************** Obtener Ciudades Nacimiento ******************************/
            var sp_editar_DropDownLists_Cd_Nac = DbUtil.GetCursor("sp_recursos_empleado_editar_DropDownLists_EdoCd",
                new SqlParameter("@numeroEstadosDir", ""),
                new SqlParameter("@numeroCiudadesDir", ""),
                new SqlParameter("@numeroEstadosNac", ""),
                new SqlParameter("@numeroCiudadesNac", numeroTabla)
                );

            drop_cdNac.DataSource = sp_editar_DropDownLists_Cd_Nac;
            drop_cdNac.DataTextField = "DescCd";
            drop_cdNac.DataValueField = "IdCiudad";
            drop_cdNac.DataBind();

            /****************************** Obtener Codigos Postales ******************************/
            
        }

        private void Load_Baja()
        {
            var sp_editar_loadDatos = DbUtil.ExecuteProc("sp_recursos_empleado_editar_load_baja",
                            new SqlParameter("@no_emp", Session["GetNumero"].ToString()),
                            MsBarco.DbUtil.NewSqlParam("@motivo", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                            MsBarco.DbUtil.NewSqlParam("@descripcion", null, SqlDbType.VarChar, ParameterDirection.Output, 80),
                            MsBarco.DbUtil.NewSqlParam("@recontratable", null, SqlDbType.VarChar, ParameterDirection.Output, 10),
                            MsBarco.DbUtil.NewSqlParam("@fecha_baja", null, SqlDbType.VarChar, ParameterDirection.Output, 25)
                            );

            lbl_baja_motivo.Text = sp_editar_loadDatos["@motivo"].ToString();
            lbl_baja_comentarios.Text = sp_editar_loadDatos["@descripcion"].ToString();
            lbl_baja_recontratable.Text = sp_editar_loadDatos["@recontratable"].ToString();
            lbl_baja_fecha.Text = sp_editar_loadDatos["@fecha_baja"].ToString();
        }

        protected void drop_depto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ValorDropDepto = drop_depto.SelectedValue;

            if (ValorDropDepto == "")
            {
                drop_puesto.Items.Clear();
                drop_puesto.Enabled = false;

                drop_cliente.Items.Clear();
                drop_cliente.Enabled = false;

                drop_puestoCliente.Items.Clear();
                drop_puestoCliente.Enabled = false;

                rowBonos.Visible = false;
            }

            if (ValorDropDepto != "")
            {
                drop_puesto.Enabled = true;
                drop_puesto.Items.Clear();

                var sp_DropDepto = DbUtil.GetCursor("sp_recursos_empleado_editar_LoadDropDepto",
                    new SqlParameter("@numero", lb_numero.Text),
                    new SqlParameter("@Drop_valueDepto", ValorDropDepto),
                    new SqlParameter("@Drop_valuePuesto", ""),
                    new SqlParameter("@Drop_valueCliente", ""),
                    MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

                var sp_DropDepto1 = DbUtil.ExecuteProc("sp_recursos_empleado_editar_LoadDropDepto",
                    new SqlParameter("@numero", lb_numero.Text),
                    new SqlParameter("@Drop_valueDepto", ValorDropDepto),
                    new SqlParameter("@Drop_valuePuesto", ""),
                    new SqlParameter("@Drop_valueCliente", ""),
                    MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );
                
                if (sp_DropDepto1["@stored_vacio"].ToString() == "0")
                {
                    drop_puesto.Items.Add(new ListItem("-- Seleccionar --", ""));

                    drop_puesto.DataSource = sp_DropDepto;
                    drop_puesto.DataValueField = "IdPuesto";
                    drop_puesto.DataTextField = "DescPuesto";
                    drop_puesto.DataBind();
                }
                if (sp_DropDepto1["@stored_vacio"].ToString() == "1")
                {
                    drop_puesto.Items.Clear();
                    drop_puesto.Items.Add(new ListItem("-- Sin puestos --", ""));
                }

                if (ValorDropDepto != "6")
                {
                    drop_cliente.Items.Clear();
                    drop_cliente.Enabled = false;
                    drop_puestoCliente.Items.Clear();
                    drop_puestoCliente.Enabled = false;
                    rowBonos.Visible = false;
                }
            }
        }

        protected void drop_puesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool puestoContieneChofer = drop_puesto.SelectedItem.Text.Contains("Chofer");
            bool puestoContienePlanta = drop_puesto.SelectedItem.Text.Contains("Planta");

            if (puestoContieneChofer == true || puestoContienePlanta == true)
            {
                drop_puestoCliente.Enabled = true;

                rowBonos.Visible = true;

                var sp_DropPuesto1 = DbUtil.GetCursor("sp_recursos_empleado_editar_LoadDropDepto",
                    new SqlParameter("@numero", lb_numero.Text),
                    new SqlParameter("@Drop_valueDepto", ""),
                    new SqlParameter("@Drop_valuePuesto", "ChoferOPlanta1"),
                    new SqlParameter("@Drop_valueCliente", ""),
                    MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

                var sp_DropPuesto2 = DbUtil.GetCursor("sp_recursos_empleado_editar_LoadDropDepto",
                    new SqlParameter("@numero", lb_numero.Text),
                    new SqlParameter("@Drop_valueDepto", ""),
                    new SqlParameter("@Drop_valuePuesto", "ChoferOPlanta2"),
                    new SqlParameter("@Drop_valueCliente", ""),
                    MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );
                
                drop_cliente.DataSource = sp_DropPuesto2;
                drop_cliente.DataValueField = "idPuestoCliente";
                drop_cliente.DataTextField = "nombreComercial";
                drop_cliente.DataBind();

                drop_puestoCliente.DataSource = sp_DropPuesto1;
                drop_puestoCliente.DataValueField = "IdPuestoClientes";
                drop_puestoCliente.DataTextField = "TipoTractorMovimiento";
                drop_puestoCliente.DataBind();

                drop_cliente.SelectedValue = "0";

                row_movimiento_drop.Visible = true;
                row_movimiento_lb.Visible = true;

                row_cliente_lb.Visible = true;
                row_cliente_drop.Visible = true;

            }
            else
            {
                drop_cliente.Items.Clear();
                drop_cliente.Enabled = false;

                drop_puestoCliente.Items.Clear();
                drop_puestoCliente.Enabled = false;

                rowBonos.Visible = false;

                row_movimiento_drop.Visible = false;
                row_movimiento_lb.Visible = false;

                row_cliente_lb.Visible = false;
                row_cliente_drop.Visible = false;
            }
        }

        protected void drop_puestoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            drop_cliente.SelectedValue = "0";
        }

        protected void drop_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ValorDropCliente = drop_cliente.SelectedValue;

            if (ValorDropCliente != "--Seleccionar--")
            {
                bool puestoContieneChofer = drop_puesto.SelectedItem.Text.Contains("Chofer");
                bool puestoContienePlanta = drop_puesto.SelectedItem.Text.Contains("Planta");

                if (puestoContieneChofer == true)
                {
                    drop_puestoCliente.Enabled = true;

                    //Aqui podemos concoer si este stored procedure podemos esta vacio o no
                    var sp_DropCliente2 = DbUtil.ExecuteProc("sp_recursos_empleado_editar_LoadDropDepto",
                        new SqlParameter("@numero", lb_numero.Text),
                        new SqlParameter("@Drop_valueDepto", ""),
                        new SqlParameter("@Drop_valuePuesto", ""),
                        new SqlParameter("@Drop_valueCliente", ValorDropCliente),
                        MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                    if (sp_DropCliente2["@stored_vacio"].ToString() == "1")
                    {
                        var sp_DropCliente = DbUtil.GetCursor("sp_recursos_empleado_editar_LoadDropDepto",
                        new SqlParameter("@numero", lb_numero.Text),
                        new SqlParameter("@Drop_valueDepto", ""),
                        new SqlParameter("@Drop_valuePuesto", ""),
                        new SqlParameter("@Drop_valueCliente", ValorDropCliente),
                        MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                        );

                        drop_puestoCliente.DataSource = sp_DropCliente;
                        drop_puestoCliente.DataValueField = "IdPuestoClientes";
                        drop_puestoCliente.DataTextField = "TipoTractorMovimiento";
                        drop_puestoCliente.DataBind();
                    }

                    if (sp_DropCliente2["@stored_vacio"].ToString() == "0")
                    {
                        drop_puestoCliente.Items.Clear();
                        drop_puestoCliente.Items.Insert(0, new ListItem("--Sin puestos--", ""));
                    }
                }

                if (puestoContienePlanta == true)
                {
                    drop_puestoCliente.Items.Clear();
                    drop_puestoCliente.Enabled = false;
                }
            }
            else
            {
                drop_puestoCliente.Items.Clear();
                drop_puestoCliente.Enabled = false;
            }
        }

        protected void drop_paisDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorPais = drop_paisDir.SelectedValue;

            if (valorPais != "")
            {
                drop_edoDir.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", valorPais),
                    new SqlParameter("@Drop_valueEstado", "")
                    );

                drop_edoDir.DataSource = sp_LoadDropPais;
                drop_edoDir.DataTextField = "descEstado";
                drop_edoDir.DataValueField = "idestado";
                drop_edoDir.DataBind();

                drop_cdDir.Items.Clear();
                drop_cdDir.Enabled = false;
            }
            else
            {
                drop_edoDir.Items.Clear();
                drop_edoDir.Enabled = false;
                drop_cdDir.Items.Clear();
                drop_cdDir.Enabled = false;
                Drop_codPostDir.Items.Clear();
                Drop_codPostDir.Enabled = false;
                Drop_colonia.Items.Clear();
                Drop_colonia.Enabled = false;
                Drop_calleDir.Items.Clear();
                Drop_calleDir.Enabled = false;
            }
        }

        protected void drop_edoDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorEstado = drop_edoDir.SelectedValue;

            if (valorEstado != "")
            {
                drop_cdDir.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", ""),
                    new SqlParameter("@Drop_valueEstado", valorEstado)
                    );

                drop_cdDir.DataSource = sp_LoadDropPais;
                drop_cdDir.DataTextField = "descCiudad";
                drop_cdDir.DataValueField = "idciudad";
                drop_cdDir.DataBind();

                drop_cdDir.Items.Add(new ListItem("-- Seleccionar --", ""));
                drop_cdDir.SelectedValue = "";
            }
            else
            {
                drop_cdDir.Items.Clear();
                drop_cdDir.Enabled = false;
            }
        }

        protected void drop_paisNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorPais = drop_paisNac.SelectedValue;

            if (valorPais != "")
            {
                drop_edoNac.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", valorPais),
                    new SqlParameter("@Drop_valueEstado", "")
                    );

                drop_edoNac.DataSource = sp_LoadDropPais;
                drop_edoNac.DataTextField = "descEstado";
                drop_edoNac.DataValueField = "idestado";
                drop_edoNac.DataBind();

                drop_cdNac.Items.Clear();
                drop_cdNac.Enabled = false;
            }
            else
            {
                drop_edoNac.Items.Clear();
                drop_edoNac.Enabled = false;
                drop_cdNac.Items.Clear();
                drop_cdNac.Enabled = false;
            }
        }

        protected void drop_edoNac_SelectedIndexChanged(object sender, EventArgs e)
        {
            var valorEstado = drop_edoNac.SelectedValue;

            if (valorEstado != "")
            {
                drop_cdNac.Enabled = true;

                var sp_LoadDropPais = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropPais",
                    new SqlParameter("@Drop_valuePais", ""),
                    new SqlParameter("@Drop_valueEstado", valorEstado)
                    );

                drop_cdNac.DataSource = sp_LoadDropPais;
                drop_cdNac.DataTextField = "descCiudad";
                drop_cdNac.DataValueField = "idciudad";
                drop_cdNac.DataBind();
            }
            else
            {
                drop_cdNac.Items.Clear();
                drop_cdNac.Enabled = false;
            }
        }

        protected void btn_cerrarPopUp_cancelar_si_Click(object sender, EventArgs e)
        {
            // Devolvernos a la ventana principal de empleados
            Response.Redirect("empleados.aspx");
            Session["GetNumero"] = "";
        }

        protected void rb_sexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rb_sexo.Text == "F")
            {
                drop_tallaP.Items.Clear();

                for (int i = 1; i <= 15; i += 2)
                {
                    drop_tallaP.Items.Add(i.ToString());
                }
            }

            if (rb_sexo.Text == "M")
            {
                drop_tallaP.Items.Clear();

                for (int i = 28; i <= 42; i += 2)
                {
                    drop_tallaP.Items.Add(i.ToString());
                }
            }
        }

        protected void btn_cerrarPopUp_guardar_si_Click(object sender, EventArgs e)
        {
            var numeroTabla = Session["GetNumero"].ToString();

            /*
            if (drop_puesto.SelectedValue == "6.1.1.5")
            {
                lb_mensaje.Text = "No se permite el Cliente o Tipo de Movimiento vacío cuando se selecciona el puesto de chofer.";
            }
            */

            if (drop_depto.SelectedValue != "" && drop_puesto.SelectedValue == "6.1.1.5"  ||
                drop_depto.SelectedValue != "" && drop_puesto.SelectedValue != "6.1.1.5" )
            {
                string Colonia = "";
                if (Drop_colonia.SelectedValue != "na")
                {
                    Colonia = Drop_colonia.SelectedItem.Text;
                }

                string direccion;
                if (tb_calleInt.Text == "")
                {
                    direccion = tb_CalleNo.Text;
                }
                else
                {
                    direccion = tb_CalleNo.Text + " - " + tb_calleInt.Text;
                }

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


                if (tb_telefono.Text == "+__(___)___-____")
                {
                    tb_telefono.Text = "";
                }

                if (Drop_calleDir.SelectedItem.Text == "-- Seleccionar --")
                {
                    string script = "alert(\"Seleccionar calle!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    return;
                }

                string codigo_postal, calle, colonia;
                if (drop_cdDir.SelectedValue == "61" || drop_cdDir.SelectedValue == "1464")
                {
                    colonia = Drop_colonia.SelectedItem.Text;
                    calle = Drop_calleDir.SelectedItem.Text;
                    codigo_postal = Drop_codPostDir.SelectedItem.Text;

                }
                else
                {
                    colonia = tb_colonia.Text;
                    calle = tb_calle.Text;
                    codigo_postal = tb_codPos.Text;
                }

                    var sp_UpdateDatos = DbUtil.ExecuteProc("sp_recursos_empleado_editar_UpdateDatos",
                    new SqlParameter("@numero", numeroTabla),
                    new SqlParameter("@departamento", drop_depto.SelectedValue),
                    new SqlParameter("@puesto", drop_puesto.SelectedValue),
                    new SqlParameter("@puesto_cliente", !string.IsNullOrEmpty(drop_puestoCliente.SelectedValue) ? drop_puestoCliente.SelectedValue : (object)DBNull.Value),
                    new SqlParameter("@supervisor", drop_supervisor.SelectedValue),
                    new SqlParameter("@nom_padre", tb_nomPat.Text),
                    new SqlParameter("@nom_madre", tb_nomMat.Text),
                    new SqlParameter("@email", tb_correo.Text),
                    new SqlParameter("@tallaC", !string.IsNullOrEmpty(drop_tallaC.SelectedValue) ? drop_tallaC.SelectedValue : (object)DBNull.Value),
                    new SqlParameter("@tallaP", drop_tallaP.SelectedValue),
                    new SqlParameter("@telefono", tb_telefono.Text),
                    new SqlParameter("@sexo", rb_sexo.SelectedValue),
                    new SqlParameter("@fec_nac", tb_fecNac.Text),
                    new SqlParameter("@cd_nac", drop_cdNac.SelectedValue),
                    new SqlParameter("@edo_nac", drop_edoNac.SelectedValue),
                    new SqlParameter("@pais_nac", drop_paisNac.SelectedValue),
                    new SqlParameter("@colonia_dir", colonia),
                    new SqlParameter("@calle_dir", calle),
                    new SqlParameter("@num_dir", direccion),
                    new SqlParameter("@codpost_dir", codigo_postal),
                    new SqlParameter("@ciudad", drop_cdDir.SelectedValue),
                    new SqlParameter("@estado", drop_edoDir.SelectedValue),
                    new SqlParameter("@pais", drop_paisDir.SelectedValue),
                    new SqlParameter("@edo_civil", drop_civil.SelectedValue),
                    new SqlParameter("@hijos", tb_noNinos.Text),
                    new SqlParameter("@rfc", tb_rfc.Text),
                    //new SqlParameter("@imss", tb_imss.Text),
                    new SqlParameter("@servicio", drop_servicioTipo.SelectedValue),
                    new SqlParameter("@servicio_num", tb_servNo.Text),
                    new SqlParameter("@servicio_vig", !string.IsNullOrEmpty(tb_servNoVig.Text) ? tb_servNoVig.Text : (object)DBNull.Value),
                    new SqlParameter("@pasaporte", tb_pasaporte.Text),
                    new SqlParameter("@pasaporte_vig", !string.IsNullOrEmpty(tb_pasaporteVig.Text) ? tb_pasaporteVig.Text : (object)DBNull.Value),
                    new SqlParameter("@visa", tb_visa.Text),
                    new SqlParameter("@visa_vig", !string.IsNullOrEmpty(tb_visaVig.Text) ? tb_visaVig.Text : (object)DBNull.Value),
                    new SqlParameter("@licencia", tb_licencia.Text),
                    new SqlParameter("@licencia_vig", !string.IsNullOrEmpty(tb_licenciaVig.Text) ? tb_licenciaVig.Text : (object)DBNull.Value),
                    new SqlParameter("@lic_fast", tb_fast.Text),
                    new SqlParameter("@lic_fast_vig", !string.IsNullOrEmpty(tb_fastVig.Text) ? tb_fastVig.Text : (object)DBNull.Value),
                    new SqlParameter("@carta_penal", tb_penal.Text),
                    new SqlParameter("@carta_penal_vig", !string.IsNullOrEmpty(tb_penalVig.Text) ? tb_penalVig.Text : (object)DBNull.Value),
                    //new SqlParameter("@carta_policial", tb_policial.Text),
                    new SqlParameter("@carta_policial_vig", !string.IsNullOrEmpty(tb_policialVig.Text) ? tb_policialVig.Text : (object)DBNull.Value),
                    new SqlParameter("@escolaridad", drop_escolaridad.SelectedValue),
                    new SqlParameter("@escolaridad_documento", drop_escolaridad_documento.SelectedValue),
                    new SqlParameter("@escolaridad_institucion", drop_escolaridad_institucion.SelectedValue),
                    new SqlParameter("@carrera", tb_carrera.Text),
                    new SqlParameter("@ingles", tb_ingles.Text),
                    new SqlParameter("@zona_economica", drop_zonas.SelectedValue),

                    //***** Prestaciones *****
                    //new SqlParameter("@imss", txtIMSS.Text),
                    new SqlParameter("@validarINFONAVIT", chkINFONAVIT.Checked ? 1 : 0),
                    new SqlParameter("@noINFONAVIT", !string.IsNullOrEmpty(txtNoCredito.Text) ? txtNoCredito.Text : (object)DBNull.Value),
                    new SqlParameter("@tipoINFONAVIT", ddlINFONAVIT.SelectedValue),
                    new SqlParameter("@factorINFONAVIT", txtFactorINFONAVIT.Text),
                    new SqlParameter("@validarFONACOT", chkFONACOT.Checked ? 1 : 0),
                    new SqlParameter("@noFONACOT", !string.IsNullOrEmpty(txtNoFONACOT.Text) ? txtNoFONACOT.Text : (object)DBNull.Value),
                    new SqlParameter("@descuentoFONACOT", txtRetencionFONACOT.Text),
                    new SqlParameter("@totalFONACOT", txtTotalFONACOT.Text),
                    new SqlParameter("@inicioFONACOT", !string.IsNullOrEmpty(txtFechaFONACOT.Text) ? txtFechaFONACOT.Text : (object)DBNull.Value),
                    new SqlParameter("@validarPension", chkPension.Checked ? 1 : 0),
                    new SqlParameter("@descuentoPension", txtPension.Text),

                    new SqlParameter("@apto", !string.IsNullOrEmpty(tb_apto.Text) ? tb_apto.Text : (object)DBNull.Value),
                    new SqlParameter("@apto_inicio", !string.IsNullOrEmpty(tb_apto_inicio.Text) ? tb_apto_inicio.Text : (object)DBNull.Value),
                    new SqlParameter("@apto_fin", !string.IsNullOrEmpty(tb_apto_vigencia.Text) ? tb_apto_vigencia.Text : (object)DBNull.Value)
                    ,

                    new SqlParameter("@constancia_inicio", !string.IsNullOrEmpty(tb_constancia_inicio.Text) ? tb_constancia_inicio.Text : (object)DBNull.Value),
                    new SqlParameter("@constancia_fin", !string.IsNullOrEmpty(tb_constancia_fin.Text) ? tb_constancia_fin.Text : (object)DBNull.Value),
                    new SqlParameter("@folio_constancia", !string.IsNullOrEmpty(tb_constancia.Text) ? tb_constancia.Text : (object)DBNull.Value),
                    new SqlParameter("@tipo_licencia", !string.IsNullOrEmpty(tipo_licencia) ? tipo_licencia : (object)DBNull.Value)
                    //new SqlParameter("@puesto")
                    );

                if (chklBonos.Visible == true)
                {
                    foreach (ListItem item in chklBonos.Items)
                    {
                        var sp_recursos_empleado_editar_bonos_add = DbUtil.ExecuteProc("sp_recursos_empleado_editar_bonos_add",
                        new SqlParameter("@no_empleado", numeroTabla),
                        new SqlParameter("@puesto_clientes", drop_puestoCliente.SelectedValue),
                        new SqlParameter("@bono_operador", item.Value),
                        new SqlParameter("@activo", item.Selected ? '1' : '0')
                        );
                    }
                }

                if (tb_contacto_telefono.Text != "" && tb_contacto.Text != "")
                {

                    var sp_recursos_empleado_editar_contacto = DbUtil.ExecuteProc("sp_recursos_empleado_editar_editarContacto",
                    new SqlParameter("@no_empleado", numeroTabla),
                    new SqlParameter("@contacto", tb_contacto.Text),
                    new SqlParameter("@telefono", tb_contacto_telefono.Text)
                    );

                }

                if (tb_gafete_unico.Text != "" && tb_gafete_unicoVig.Text != "")
                {

                    var sp_recursos_empleado_editar_contacto = DbUtil.ExecuteProc("sp_recursos_empleado_editar_editarGafeteUnico",
                    new SqlParameter("@no_empleado", numeroTabla),
                    new SqlParameter("@gafete_unico", tb_gafete_unico.Text),
                    new SqlParameter("@gafete_unicoVig", tb_gafete_unicoVig.Text)
                    );

                }

                editar_titulo.Text = "Empleado ingresado con exito en la base de datos..";

                MV_EditarEmpleados.ActiveViewIndex = 1;

                titulo_confirmacion.Text = "El empleado con el número " + numeroTabla + " ha sido actualizado exitosamente.";
            }
        }

        protected void chkINFONAVIT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkINFONAVIT.Checked == true)
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

        private void mostrarReactivar()
        {
            var numerotabla = Session["GetNumero"].ToString();

            var sp_mostrarReactivar = DbUtil.ExecuteProc("sp_recursos_empleado_editar_mostrarBoton",
                new SqlParameter("@no_empleado", numerotabla),
                MsBarco.DbUtil.NewSqlParam("@reactivar", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_mostrarReactivar["@reactivar"].ToString() == "0")
            {
                btn_guardar.Visible = true;
                btn_reactivar.Visible = false;
            }

            if (sp_mostrarReactivar["@reactivar"].ToString() == "1")
            {
                btn_guardar.Visible = false;
                btn_reactivar.Visible = true;
                Load_Baja();
                tabla_editar_baja.Visible = true;
            }
            if (sp_mostrarReactivar["@reactivar"].ToString() == "2")
            {
                btn_guardar.Visible = false;
                btn_reactivar.Visible = false;
            }
        }

        protected void btn_cerrarPopUp_reactivar_si_Click(object sender, EventArgs e)
        {
            var numeroTabla = Session["GetNumero"].ToString();

            /*
            if (drop_puesto.SelectedValue == "6.1.1.5" )
            {
                lb_mensaje.Text = "No se permite el Cliente o Tipo de Movimiento vacío cuando se selecciona el puesto de chofer.";
            }
            */

            if (drop_depto.SelectedValue != "" && drop_puesto.SelectedValue == "6.1.1.5"  ||
                drop_depto.SelectedValue != "" && drop_puesto.SelectedValue != "6.1.1.5" )
            {

                string Colonia = "";
                if (Drop_colonia.SelectedValue != "na")
                {
                    Colonia = Drop_colonia.SelectedItem.Text;
                }

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

                var sp_UpdateReingreso = DbUtil.ExecuteProc("sp_recursos_empleado_editar_Reingreso",
                    new SqlParameter("@no_emp", numeroTabla)
                    );


                var sp_UpdateDatos = DbUtil.ExecuteProc("sp_recursos_empleado_editar_UpdateDatos",
                    new SqlParameter("@numero", numeroTabla),
                    new SqlParameter("@departamento", drop_depto.SelectedValue),
                    new SqlParameter("@puesto", drop_puesto.SelectedValue),
                    new SqlParameter("@puesto_cliente", !string.IsNullOrEmpty(drop_puestoCliente.SelectedValue) ? drop_puestoCliente.SelectedValue : (object)DBNull.Value),
                    new SqlParameter("@supervisor", drop_supervisor.SelectedValue),
                    new SqlParameter("@nom_padre", tb_nomPat.Text),
                    new SqlParameter("@nom_madre", tb_nomMat.Text),
                    new SqlParameter("@email", tb_correo.Text),
                    new SqlParameter("@tallaC", !string.IsNullOrEmpty(drop_tallaC.SelectedValue) ? drop_tallaC.SelectedValue : (object)DBNull.Value),
                    new SqlParameter("@tallaP", drop_tallaP.SelectedValue),
                    new SqlParameter("@telefono", tb_telefono.Text),
                    new SqlParameter("@sexo", rb_sexo.SelectedValue),
                    new SqlParameter("@fec_nac", tb_fecNac.Text),
                    new SqlParameter("@cd_nac", drop_cdNac.SelectedValue),
                    new SqlParameter("@edo_nac", drop_edoNac.SelectedValue),
                    new SqlParameter("@pais_nac", drop_paisNac.SelectedValue),
                    new SqlParameter("@colonia_dir", !string.IsNullOrEmpty(Colonia) ? Drop_colonia.SelectedItem.Text : (object)DBNull.Value),
                    new SqlParameter("@calle_dir", Drop_calleDir.SelectedItem.Text),
                    new SqlParameter("@num_dir", tb_CalleNo.Text + " - " + tb_calleInt.Text),
                    new SqlParameter("@codpost_dir", Drop_codPostDir.SelectedItem.Text),
                    new SqlParameter("@ciudad", drop_cdDir.SelectedValue),
                    new SqlParameter("@estado", drop_edoDir.SelectedValue),
                    new SqlParameter("@pais", drop_paisDir.SelectedValue),
                    new SqlParameter("@edo_civil", drop_civil.SelectedValue),
                    new SqlParameter("@hijos", tb_noNinos.Text),
                    new SqlParameter("@rfc", tb_rfc.Text),
                    //new SqlParameter("@imss", tb_imss.Text),
                    new SqlParameter("@servicio", drop_servicioTipo.SelectedValue),
                    new SqlParameter("@servicio_num", tb_servNo.Text),
                    new SqlParameter("@servicio_vig", !string.IsNullOrEmpty(tb_servNoVig.Text) ? tb_servNoVig.Text : (object)DBNull.Value),
                    new SqlParameter("@pasaporte", tb_pasaporte.Text),
                    new SqlParameter("@pasaporte_vig", !string.IsNullOrEmpty(tb_pasaporteVig.Text) ? tb_pasaporteVig.Text : (object)DBNull.Value),
                    new SqlParameter("@visa", tb_visa.Text),
                    new SqlParameter("@visa_vig", !string.IsNullOrEmpty(tb_visaVig.Text) ? tb_visaVig.Text : (object)DBNull.Value),
                    new SqlParameter("@licencia", tb_licencia.Text),
                    new SqlParameter("@licencia_vig", !string.IsNullOrEmpty(tb_licenciaVig.Text) ? tb_licenciaVig.Text : (object)DBNull.Value),
                    new SqlParameter("@lic_fast", tb_fast.Text),
                    new SqlParameter("@lic_fast_vig", !string.IsNullOrEmpty(tb_fastVig.Text) ? tb_fastVig.Text : (object)DBNull.Value),
                    new SqlParameter("@carta_penal", tb_penal.Text),
                    new SqlParameter("@carta_penal_vig", !string.IsNullOrEmpty(tb_penalVig.Text) ? tb_penalVig.Text : (object)DBNull.Value),
                    //new SqlParameter("@carta_policial", tb_policial.Text),
                    new SqlParameter("@carta_policial_vig", !string.IsNullOrEmpty(tb_policialVig.Text) ? tb_policialVig.Text : (object)DBNull.Value),
                    new SqlParameter("@escolaridad", drop_escolaridad.SelectedValue),
                    new SqlParameter("@escolaridad_documento", drop_escolaridad_documento.SelectedValue),
                    new SqlParameter("@escolaridad_institucion", drop_escolaridad_institucion.SelectedValue),
                    new SqlParameter("@carrera", tb_carrera.Text),
                    new SqlParameter("@ingles", tb_ingles.Text),
                    new SqlParameter("@zona_economica", drop_zonas.SelectedValue),
                    //***** Prestaciones *****
                    //new SqlParameter("@imss", txtIMSS.Text),
                    new SqlParameter("@validarINFONAVIT", chkINFONAVIT.Checked ? 1 : 0),
                    new SqlParameter("@noINFONAVIT", !string.IsNullOrEmpty(txtNoCredito.Text) ? txtNoCredito.Text : (object)DBNull.Value),
                    new SqlParameter("@tipoINFONAVIT", ddlINFONAVIT.SelectedValue),
                    new SqlParameter("@factorINFONAVIT", txtFactorINFONAVIT.Text),
                    new SqlParameter("@validarFONACOT", chkFONACOT.Checked ? 1 : 0),
                    new SqlParameter("@noFONACOT", !string.IsNullOrEmpty(txtNoFONACOT.Text) ? txtNoFONACOT.Text : (object)DBNull.Value),
                    new SqlParameter("@descuentoFONACOT", txtRetencionFONACOT.Text),
                    new SqlParameter("@totalFONACOT", txtTotalFONACOT.Text),
                    new SqlParameter("@inicioFONACOT", !string.IsNullOrEmpty(txtFechaFONACOT.Text) ? txtFechaFONACOT.Text : (object)DBNull.Value),
                    new SqlParameter("@validarPension", chkPension.Checked ? 1 : 0),
                    new SqlParameter("@descuentoPension", txtPension.Text),
                    new SqlParameter("@apto", !string.IsNullOrEmpty(tb_apto.Text) ? tb_apto.Text : (object)DBNull.Value),
                    new SqlParameter("@apto_inicio", !string.IsNullOrEmpty(tb_apto_inicio.Text) ? tb_apto_inicio.Text : (object)DBNull.Value),
                    new SqlParameter("@apto_fin", !string.IsNullOrEmpty(tb_apto_vigencia.Text) ? tb_apto_vigencia.Text : (object)DBNull.Value),
                    new SqlParameter("@constancia_inicio", !string.IsNullOrEmpty(tb_constancia_inicio.Text) ? tb_constancia_inicio.Text : (object)DBNull.Value),
                    new SqlParameter("@constancia_fin", !string.IsNullOrEmpty(tb_constancia_fin.Text) ? tb_constancia_fin.Text : (object)DBNull.Value),
                    new SqlParameter("@folio_constancia", !string.IsNullOrEmpty(tb_constancia.Text) ? tb_constancia.Text : (object)DBNull.Value),
                    new SqlParameter("@tipo_licencia", !string.IsNullOrEmpty(tipo_licencia) ? tipo_licencia : (object)DBNull.Value)
                    );

                if (chklBonos.Visible == true)
                {
                    foreach (ListItem item in chklBonos.Items)
                    {
                        var sp_recursos_empleado_editar_bonos_add = DbUtil.ExecuteProc("sp_recursos_empleado_editar_bonos_add",
                        new SqlParameter("@no_empleado", numeroTabla),
                        new SqlParameter("@puesto_clientes", drop_puestoCliente.SelectedValue),
                        new SqlParameter("@bono_operador", item.Value),
                        new SqlParameter("@activo", item.Selected ? '1' : '0')
                        );
                    }
                }

                editar_titulo.Text = "Empleado ingresado con exito en la base de datos..";

                MV_EditarEmpleados.ActiveViewIndex = 1;

                titulo_confirmacion.Text = "El empleado con el número " + numeroTabla + " ha sido actualizado exitosamente.";
            }
        }

        protected void btn_confirmacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            MaxDate();
            if (drop_paisDir.SelectedItem.Text =="" || drop_paisDir.SelectedValue == "" ||
                drop_edoDir.SelectedItem.Text == "" || drop_edoDir.SelectedValue == "" ||
                drop_cdDir.SelectedItem.Text == "" || drop_cdDir.SelectedValue == "" ||
                Drop_codPostDir.SelectedItem.Text == "" || Drop_codPostDir.SelectedValue == "" ||
                Drop_colonia.SelectedItem.Text == "" || Drop_colonia.SelectedValue == "" ||
                Drop_calleDir.SelectedItem.Text == "" )
            {
                btn_cerrarPopUp_guardar_si.Visible = false;
                lbl_alerta_editar.Text = "Completar datos de la dirección del empleado";
            }
            else
            {
                btn_cerrarPopUp_guardar_si.Visible = true;
                lbl_alerta_editar.Text = "";
            }

            popUp_editar.Show();
        }

        protected void drop_cdDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_cdDir.SelectedValue != "")
            {
                if (drop_cdDir.SelectedValue == "61" || drop_cdDir.SelectedValue == "1464")
                {

                    tb_calle.Visible = false;
                    Drop_calleDir.Visible = true;
                    Drop_calleDir.SelectedValue = "";

                    tb_colonia.Visible = false;
                    Drop_colonia.Visible = true;
                    Drop_colonia.Items.Clear();
                    Drop_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));
                    Drop_colonia.SelectedValue = "";
                    Drop_colonia.Enabled = false;

                    tb_codPos.Visible = false;
                    Drop_codPostDir.Visible = true;
                    Drop_codPostDir.Enabled = true;

                    var sp_loadCod = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropCodigosPostales",
                        new SqlParameter("@idCiudad", drop_cdDir.SelectedValue)
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

                    tb_codPos.Visible = true;
                    Drop_codPostDir.Visible = false;
                }
            }
            else
            {
                Drop_codPostDir.SelectedValue = "";
                Drop_codPostDir.Enabled = false;
            }
        }

        protected void Drop_codPostDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Drop_codPostDir.SelectedValue != "")
            {

                var sp_loadColonia = DbUtil.GetCursor("sp_recursos_empleado_add_LoadDropColonias",
                    new SqlParameter("@codPostal", Drop_codPostDir.SelectedValue)
                    );

                Drop_colonia.DataSource = sp_loadColonia;
                Drop_colonia.DataTextField = "descripcion";
                Drop_colonia.DataValueField = "descripcion";
                Drop_colonia.DataBind();               

                Drop_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));
                Drop_colonia.Enabled = true;

                Drop_colonia.SelectedValue = "";

            }
            else
            {

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
                    new SqlParameter("@idCiudad", drop_cdDir.Text),
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


                //Drop_colonia.Enabled = false;

                Drop_calleDir.SelectedValue = "";
                Drop_calleDir.Enabled = false;
            }
        }


    }
}