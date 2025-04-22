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
using System.Text;
using System.Security.Cryptography;

namespace recursos.Views
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Add("idEmpresa", "GCDM01");
            Session.Add("nombreUsuario", "JORGE ALFONSO ZAMORA BELLO");
            Session.Add("numeroUsuario", "GCDM-2868");
            Session.Add("usuario", "jzamora");

            //Session.Add("nombreUsuario", "Hugo");
            //Session.Add("numeroUsuario", "2049");
            //Session.Add("usuario", "hacosta");

            //Session.Add("nombreUsuario", "Roque");
            //Session.Add("numeroUsuario", "2432");
            //Session.Add("usuario", "rroque");

            //Session.Add("nombreUsuario", "Mayela");
            //Session.Add("numeroUsuario", "2173");
            //Session.Add("usuario", "mespinoza");


            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                LoadMenuRoles();

                nombreUsuario.Text = Session["nombreUsuario"].ToString();
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["userId"] as string))
            {
                //Decrypt(HttpContext.Current.Request.QueryString["userId"])

                Session.Add("usuario", Decrypt(Request.QueryString["userId"]));
                Session.Add("numeroUsuario", Decrypt(Request.QueryString["userNumber"]));
                Session.Add("nombreUsuario", Decrypt(Request.QueryString["userName"]));

                if (!IsPostBack)
                {
                    LoadMenuRoles();
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();
                }
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                //Response.Redirect("http://localhost:52810/Views/login.aspx");
            }
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "GCDM8726135242";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "GCDM8726135242";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

        protected void LoadMenuRoles()
        {
            var usuario = Session["usuario"].ToString();
            var idEmpresa = Session["idEmpresa"].ToString();

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_inicio_roles_permitirAcceso",
               new SqlParameter("@usuario", usuario),
               new SqlParameter("@id_empresa", idEmpresa),
               MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@cadenaAccesos_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
               );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                a.Value = sp_loadRol["@cadenaAccesos_modulo"].ToString();
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("http://18.219.12.59:81/Views/menu.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            }
        }

        protected void b1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("salarios.aspx");
        }

        protected void b2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("empleados.aspx");
        }

        protected void b3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("permisos.aspx");
        }
        
        protected void b4_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("incapacidades.aspx");
        }

        protected void b5_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ausentismos.aspx");
        }
        
        protected void b6_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("suspensiones.aspx");
        }

        protected void b7_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("vacaciones.aspx");
        }

        protected void b10_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("puestos.aspx");
        }

        protected void b11_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("retardos.aspx");
        }

        protected void b12_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("horas_extra.aspx");
        }
        
        protected void b13_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("incidencias.aspx");
        }

        protected void b14_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("entradas_salidas.aspx");
        }

        protected void b15_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cartas.aspx");
        }
        
        protected void b16_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("horarios.aspx");
        }

        protected void b17_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("reportes.aspx");
        }

        protected void b18_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("desarrollo_urbano.aspx");
        }
        
        protected void b19_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("actualizar_fortia.aspx?opcion=" + "1");

        }

        //protected void b20_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect("antidoping.aspx");

        //}

        protected void b20_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void b21_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("capacitacion.aspx");
        }

        protected void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            //Session.Remove("nombreUsuario");
            //Session.Remove("numeroUsuario");
            //Session.Remove("usuario");
            Response.Redirect("http://18.219.12.59:81/Views/menu.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }
    }
}