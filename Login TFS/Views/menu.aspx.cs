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

namespace Login_TFS.Views
{
    public partial class menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Add("nombreUsuario", "JORGE ALFONSO ZAMORA BELLO");
            //Session.Add("numeroUsuario", "2868");
            //Session.Add("usuario", "jzamora");

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                LoadMenuRoles();

                loadTicketsPendientes();

                nombreUsuario.Text = Session["nombreUsuario"].ToString();
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["userId"] as string))
            {
                Session.Add("usuario", Decrypt(Request.QueryString["userId"]));
                Session.Add("nombreUsuario", Decrypt(Request.QueryString["userName"]));

                LoadMenuRoles();

                loadTicketsPendientes();

                nombreUsuario.Text = Session["nombreUsuario"].ToString();
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        private void LoadMenuRoles()
        {
            var usuario = Session["usuario"].ToString();

            var sp_loadRol = DbUtil.ExecuteProc("sp_login_menu_roles_permitirAcceso",
               new SqlParameter("@usuario", usuario),
               new SqlParameter("id_empresa", Session["idEmpresa"].ToString()),
               MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@cadenaAccesos_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
               );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                a.Value = sp_loadRol["@cadenaAccesos_modulo"].ToString();
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("login.aspx");
            }
        }

        private void loadTicketsPendientes()
        {
            var sp_loadTickets = DbUtil.ExecuteProc("sp_gcdm_tickets_tickets_pendientes_loadConteo",
                new SqlParameter("@usuario", Session["usuario"].ToString()),
                new SqlParameter("@id_empresa", Session["idEmpresa"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@conteoTickets", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_loadTickets["@conteoTickets"].ToString() == "0")
                mensaje_error.CssClass = "mensaje_no_error";
            else
                mensaje_error.CssClass = "mensaje_error";

            mensaje_error.Text = "Hay " + sp_loadTickets["@conteoTickets"].ToString() + " ticket(s) pendientes en el departamento por resolver.";
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "gcdm8726135242";
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
            string EncryptionKey = "gcdm8726135242";
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

        protected void b0_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:92/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }

        protected void b1_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:86/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }

        protected void b4_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:82/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }

        protected void b5_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:83/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }

        protected void b6_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:93/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }

        protected void b7_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:85/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }

        protected void b9_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:84/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }
        protected void b10_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            Response.Redirect("http://18.219.12.59:91/views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }
        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("login.aspx");
        }

        protected void b11_Click(object sender, ImageClickEventArgs e)
        {
            Session.Add("usuario", Session["usuario"].ToString());

            //Response.Redirect("http://18.219.12.59:91/views/tickets/tickets.aspx");
            Response.Redirect("tickets/tickets.aspx");

            //Response.Redirect("http://localhost:57414/Views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
        }
    }
}