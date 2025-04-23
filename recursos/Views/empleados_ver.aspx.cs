using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.IO;

namespace recursos.Views
{
    public partial class empleados_ver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    var num = Session["GetNumero"].ToString();

                    if (num != "")
                    {
                        nombreUsuario.Text = Session["nombreUsuario"].ToString();
                        loadDatos();
                        loadImage(Session["GetNumero"].ToString());
                        Load_Baja();
                        loadEquipo(Session["GetNumero"].ToString());
                    }
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        private void loadDatos()
        {
            var numerotabla = Session["GetNumero"].ToString();

            var sp_ver_loadDatos = DbUtil.ExecuteProc("sp_recursos_empleado_seleccionar",
                new SqlParameter("@numero", numerotabla),
                MsBarco.DbUtil.NewSqlParam("@numeroout", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@ape_pat", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@ape_mat", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@cliente", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@puesto_cliente", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@supervisor", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@nom_padre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@nom_madre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@email", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                MsBarco.DbUtil.NewSqlParam("@tallaC", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@tallaP", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@telefono", null, SqlDbType.VarChar, ParameterDirection.Output, 18),
                MsBarco.DbUtil.NewSqlParam("@sexo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@fec_nac", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@cd_nac", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@edo_nac", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@pais_nac", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@colonia_dir", null, SqlDbType.VarChar, ParameterDirection.Output, 90),
                MsBarco.DbUtil.NewSqlParam("@calle_dir", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@num_dir", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@codpost_dir", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@ciudad", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@estado", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@pais", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@edo_civil", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@hijos", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@curp", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@rfc", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@imss", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@servicio", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@servicio_num", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@servicio_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@pasaporte", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@pasaporte_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@visa", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@visa_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@licencia", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@licencia_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@lic_fast", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@lic_fast_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@carta_penal", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@carta_penal_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                //MsBarco.DbUtil.NewSqlParam("@carta_policial", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@carta_policial_vig", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@escolaridad", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@carrera", null, SqlDbType.VarChar, ParameterDirection.Output, 35),

                MsBarco.DbUtil.NewSqlParam("@escolaridad_documento", null, SqlDbType.VarChar, ParameterDirection.Output, 35),
                MsBarco.DbUtil.NewSqlParam("@escolaridad_institucion", null, SqlDbType.VarChar, ParameterDirection.Output, 35),

                MsBarco.DbUtil.NewSqlParam("@ingles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@zona_economica", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),

                MsBarco.DbUtil.NewSqlParam("@apto", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@apto_vigencia", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@contacto", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                MsBarco.DbUtil.NewSqlParam("@contacto_telefono", null, SqlDbType.VarChar, ParameterDirection.Output, 25),

                MsBarco.DbUtil.NewSqlParam("@gafete_unico", null, SqlDbType.VarChar, ParameterDirection.Output, 35),
                MsBarco.DbUtil.NewSqlParam("@gafete_unicoVig", null, SqlDbType.VarChar, ParameterDirection.Output, 25)

                );

            lbl_noempleado.Text = sp_ver_loadDatos["@numeroout"].ToString();
            lbl_nombre.Text = sp_ver_loadDatos["@nombre"].ToString();
            lbl_apepat.Text = sp_ver_loadDatos["@ape_pat"].ToString();
            lbl_apemat.Text = sp_ver_loadDatos["@ape_mat"].ToString();
            lbl_depto.Text = sp_ver_loadDatos["@departamento"].ToString();
            lbl_puesto.Text = sp_ver_loadDatos["@puesto"].ToString();

            if (sp_ver_loadDatos["@cliente"].ToString() != "")
                lbl_cliente.Text = sp_ver_loadDatos["@cliente"].ToString();
            else
                row_cliente.Visible = false;

            if (sp_ver_loadDatos["@puesto_cliente"].ToString() != "")
                lbl_puestocliente.Text = sp_ver_loadDatos["@puesto_cliente"].ToString();
            else
                row_cliente_puesto.Visible = false;

            lbl_supervisor.Text = sp_ver_loadDatos["@supervisor"].ToString();
            lbl_fechaingreso.Text = sp_ver_loadDatos["@fecha_ingreso"].ToString();
            lbl_contrato.Text = sp_ver_loadDatos["@contrato"].ToString();

            lbl_telefono.Text = sp_ver_loadDatos["@telefono"].ToString();
            lbl_correo.Text = sp_ver_loadDatos["@email"].ToString();
            lbl_zonaeco.Text = sp_ver_loadDatos["@zona_economica"].ToString();
            lbl_pais.Text = sp_ver_loadDatos["@pais"].ToString();
            lbl_edo.Text = sp_ver_loadDatos["@estado"].ToString();
            lbl_ciudad.Text = sp_ver_loadDatos["@ciudad"].ToString();
            lbl_calle.Text = sp_ver_loadDatos["@calle_dir"].ToString();

            if (sp_ver_loadDatos["@num_dir"].ToString() != "")
            {
                if (sp_ver_loadDatos["@num_dir"].ToString().Contains(" - "))
                {
                    // Taking a string 
                    String numero = sp_ver_loadDatos["@num_dir"].ToString();

                    String[] separator = { " - " };
                    Int32 count = 2;

                    // using the method 
                    String[] strlist = numero.Split(separator, count,
                           StringSplitOptions.RemoveEmptyEntries);

                    if (numero != " - ")
                    {
                        lbl_noext.Text = strlist[0];
                        lbl_noint.Text = strlist[1];
                    }
                    else
                    {
                        lbl_noext.Text = "";
                        lbl_noint.Text = "";
                    }

                }
                else
                {
                    lbl_noext.Text = sp_ver_loadDatos["@num_dir"].ToString();
                }
            }

            //lbl_noint.Text = sp_ver_loadDatos["@"].ToString();
            //lbl_noext.Text = sp_ver_loadDatos["@num_dir"].ToString();
            lbl_cp.Text = sp_ver_loadDatos["@codpost_dir"].ToString();
            lbl_colonia.Text = sp_ver_loadDatos["@colonia_dir"].ToString();
            lbl_tallaC.Text = sp_ver_loadDatos["@tallaC"].ToString();
            lbl_tallaP.Text = sp_ver_loadDatos["@tallaP"].ToString();

            lbl_fechanac.Text = sp_ver_loadDatos["@fec_nac"].ToString();
            lbl_sexo.Text = sp_ver_loadDatos["@sexo"].ToString();
            lbl_edocivil.Text = sp_ver_loadDatos["@edo_civil"].ToString();
            lbl_nohijos.Text = sp_ver_loadDatos["@hijos"].ToString();
            lbl_paisnac.Text = sp_ver_loadDatos["@pais_nac"].ToString();
            lbl_edonac.Text = sp_ver_loadDatos["@edo_nac"].ToString();
            lbl_cdnac.Text = sp_ver_loadDatos["@cd_nac"].ToString();
            lbl_carrera.Text = sp_ver_loadDatos["@carrera"].ToString();

            lbl_documento.Text = sp_ver_loadDatos["@escolaridad_documento"].ToString();
            lbl_escolaridad_institucion.Text = sp_ver_loadDatos["@escolaridad_institucion"].ToString();

            lbl_ingles.Text = sp_ver_loadDatos["@ingles"].ToString();
            lbl_escolaridad.Text = sp_ver_loadDatos["@escolaridad"].ToString();
            lbl_padre.Text = sp_ver_loadDatos["@nom_padre"].ToString();
            lbl_madre.Text = sp_ver_loadDatos["@nom_madre"].ToString();

            lbl_servicio.Text = sp_ver_loadDatos["@servicio"].ToString();
            lbl_noservicio.Text = sp_ver_loadDatos["@servicio_num"].ToString();
            lbl_fechaservicio.Text = sp_ver_loadDatos["@servicio_vig"].ToString();
            lbl_curp.Text = sp_ver_loadDatos["@curp"].ToString();
            lbl_rfc.Text = sp_ver_loadDatos["@rfc"].ToString();
            lbl_imss.Text = sp_ver_loadDatos["@imss"].ToString();
            lbl_pasaporte.Text = sp_ver_loadDatos["@pasaporte"].ToString();
            lbl_fechapasaporte.Text = sp_ver_loadDatos["@pasaporte_vig"].ToString();
            lbl_visa.Text = sp_ver_loadDatos["@visa"].ToString();
            lbl_fechavisa.Text = sp_ver_loadDatos["@visa_vig"].ToString();
            lbl_licencia.Text = sp_ver_loadDatos["@licencia"].ToString();
            lbl_fechalicencia.Text = sp_ver_loadDatos["@licencia_vig"].ToString();
            lbl_fast.Text = sp_ver_loadDatos["@lic_fast"].ToString();
            lbl_fastfecha.Text = sp_ver_loadDatos["@lic_fast_vig"].ToString();
            lbl_penal.Text = sp_ver_loadDatos["@carta_penal"].ToString();
            lbl_penalfecha.Text = sp_ver_loadDatos["@carta_penal_vig"].ToString();
            //lbl_policial.Text = sp_ver_loadDatos["@carta_policial"].ToString();
            lbl_policialfecha.Text = sp_ver_loadDatos["@carta_policial_vig"].ToString();

            lbl_Apto.Text = sp_ver_loadDatos["@apto"].ToString();
            lbl_Apto_Vigencia.Text = sp_ver_loadDatos["@apto_vigencia"].ToString();

            lbl_contacto.Text = sp_ver_loadDatos["@contacto"].ToString();
            lbl_contacto_telefono.Text = sp_ver_loadDatos["@contacto_telefono"].ToString();

            lbl_gafete_unico.Text = sp_ver_loadDatos["@gafete_unico"].ToString();
            lbl_gafete_unicoVig.Text = sp_ver_loadDatos["@gafete_unicoVig"].ToString();


            var sp_loadSelected = DbUtil.ExecuteProc("sp_recursos_empleados_editar_loadCheckBoxList_tipo_licencia",
            new SqlParameter("@no_empleado", Session["GetNumero"].ToString()),
            MsBarco.DbUtil.NewSqlParam("@cadena", null, SqlDbType.VarChar, ParameterDirection.Output, 200)
            );

            string str = sp_loadSelected["@cadena"].ToString();

            if (str != "")
            {
                row_tipo_licencia.Visible = true;
                lbl_tipo_licencia.Text = str;
            }





            //***************************** ANTIDOPING
            //var sp_antidoping = DbUtil.ExecuteProc("sp_recursos_verEmpleado_antidoping",
            //    new SqlParameter("@no_empleado", numerotabla),
            //    MsBarco.DbUtil.NewSqlParam("@primer_antidoping", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
            //    MsBarco.DbUtil.NewSqlParam("@segundo_antidoping", null, SqlDbType.VarChar, ParameterDirection.Output, 40));

            //antidoping_1.Text = sp_antidoping["@primer_antidoping"].ToString();
            //antidoping_2.Text = sp_antidoping["@segundo_antidoping"].ToString();

        }

        private void loadImage(string empleado)
        {
            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_utimo_registro_fotos",
            new SqlParameter("@no_empleado", empleado),
            MsBarco.DbUtil.NewSqlParam("@anio", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
            );

            string anio = sp_loadRol["@anio"].ToString();
            if (anio != "" && Directory.Exists(@"C:\inetpub\wwwroot\Recursos\images\empleados_fotos\" + anio + ""))
            {
                foreach (string ruta in Directory.EnumerateFiles(@"C:\inetpub\wwwroot\Recursos\images\empleados_fotos\" + anio + "", "*.png"))
                {
                    string contents = File.ReadAllText(ruta);
                    //Buscar el año en que tiene registro la foto
                    string foto = Path.GetFileNameWithoutExtension(ruta);
                    if (string.Equals(foto, empleado))
                    {
                        marco_foto.ImageUrl = "~/images/empleados_fotos/" + anio + "/" + empleado + ".png";
                        return;
                    }
                }
            }
            else
                marco_foto.ImageUrl = "~/images/empleados_fotos/no-foto.png";
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

            if (sp_editar_loadDatos["@motivo"].ToString() != "" && sp_editar_loadDatos["@fecha_baja"].ToString() != "")
            {
                tabla_ver_baja.Visible = true;
            }

        }

        protected void loadEquipo(string emp)
        {
            var sp_equipo = DbUtil.GetCursor("sp_recursos_empleado_load_equipo",
           new SqlParameter("@no_empleado", emp)
           );

            datagridview_equipo.DataSource = sp_equipo;
            datagridview_equipo.DataBind();
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }



        protected void btn_equipo_Click(object sender, EventArgs e)
        {
            modal_equipo.Show();
        }

        protected void btnClose_equipo_Click(object sender, EventArgs e)
        {
            modal_equipo.Hide();
        }
    }
}