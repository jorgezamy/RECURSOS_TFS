using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Login_TFS.Views
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();

            string Password = passtxt.Text;
            passtxt.Attributes.Add("value", Password);

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    passtxt.Text = "";

                    //Session.Abandon();
                    //Session.RemoveAll();

                    usertxt.Focus();
                }

            }
            else
            {
                Session.Clear();
            }
        }

        protected void entrar_Click(object sender, EventArgs e)
        {
            if (usertxt.Text != "")
            {
                if (passtxt.Text != "")
                {
                    var res = DbUtil.ExecuteProc("sp_login",
                    new SqlParameter("@usertxt", usertxt.Text),
                    new SqlParameter("@passtxt", passtxt.Text),
                    new SqlParameter("@id_empresa", empresa.Text),
                    new SqlParameter("@cliente", chkCliente.Checked ? 1 : 0),
                    MsBarco.DbUtil.NewSqlParam("@usuario_allow", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@login_allow", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@bloqueado", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                    );

                    var usuario = res["@usuario_allow"].ToString();
                    var login = res["@login_allow"].ToString();
                    var bloqueado = res["@bloqueado"].ToString();

                    //Si existe el usuario
                    if (usuario == "1")
                    {
                        //Permite el acceso
                        if (login == "1")
                        {
                            var cargarNombre = DbUtil.ExecuteProc("sp_login_cargarNombre",
                                new SqlParameter("@usuario", usertxt.Text),
                                new SqlParameter("@id_empresa", empresa.Text),
                                new SqlParameter("@cliente", chkCliente.Checked ? 1 : 0),
                                MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                                MsBarco.DbUtil.NewSqlParam("@numero", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                                MsBarco.DbUtil.NewSqlParam("@empleado", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                                );

                            if (cargarNombre["@empleado"].ToString() == "0")
                            {
                                Lbloqueado.Text = "No se encontró el empleado o cliente asignado.";
                            }

                            if (cargarNombre["@empleado"].ToString() == "1")
                            {
                                Session.Add("usuario", usertxt.Text);
                                Session.Add("idEmpresa", empresa.Text);
                                Session.Add("numeroUsuario", cargarNombre["@numero"].ToString());
                                Session.Add("nombreUsuario", cargarNombre["@nombre"].ToString());

                                if (chkCliente.Checked)
                                {                            
                                    Response.Redirect("http://18.219.12.59:91/views/inicio.aspx?userId=" + Encrypt(Session["usuario"].ToString()) + "&userNumber=" + Encrypt(Session["numeroUsuario"].ToString()) + "&userName=" + Encrypt(Session["nombreUsuario"].ToString()));
                                }
                                else
                                Response.Redirect("menu.aspx");
                            }
                        }

                        //No permite el acceso
                        if (login == "0")
                        {
                            if (bloqueado == "1")
                            {
                                //Contrasena bloqueada
                                Lbloqueado.Text = "Tu cuenta ha sido bloqueada.";
                            }
                            else
                            {
                                //Contraseña incorrecta
                                Lbloqueado.Text = "Tu contraseña es incorrecta.";
                            }
                        }
                    }

                    //No existe el usuario
                    if (usuario == "0")
                    {
                        Lbloqueado.Text = "No se encuentra el usuario";
                    }
                }
                else
                {
                    Lbloqueado.Text = "Por favor introduce una contraseña.";
                }
            }
            else
            {
                Lbloqueado.Text = "Por favor introduce un usuario.";
            }
        }

        protected void chkCliente_CheckedChanged(object sender, EventArgs e)
        {
            passtxt.Text = passtxt.Text;
            if (chkCliente.Checked == true)
            {
                chkCliente.Text = "Si";
            }

            else
            {
                chkCliente.Text = "No";
            }
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "TNCH8726135242";
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
    }
}