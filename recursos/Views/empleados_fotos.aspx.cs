using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MsBarco;
using System.IO;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace recursos.Views
{
    public partial class empleados_fotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    var numero_empleado = "";

                    //loadNoEmpleados();

                    loadNoEmpleadosSinFoto();
                    loadNoEmpleadosConFoto();

                    if (!string.IsNullOrEmpty(HttpContext.Current.Session["Reloj"] as string))
                    {
                        numero_empleado = Session["Reloj"].ToString();

                        //drop_numero_empleado.SelectedValue = numero_empleado;
                        //drop_numero_empleado.Enabled = false;

                        LoadImage(numero_empleado);

                        if (drop_numero_empleado_con_foto.Items.FindByValue(numero_empleado) != null)
                        {
                            drop_numero_empleado_con_foto.SelectedValue = numero_empleado;
                        }


                        if (drop_numero_empleado_sin_foto.Items.FindByValue(numero_empleado) != null)
                        {
                            drop_numero_empleado_sin_foto.SelectedValue = numero_empleado;
                        }

                        drop_numero_empleado_con_foto.Enabled = false;
                        drop_numero_empleado_sin_foto.Enabled = false;





                        btn_selectEmpleado.Visible = false;
                        table_empleadosFotos.Visible = true;




                        var year = DateTime.Now.Year;
                        var imageName = numero_empleado;

                        string subPath = "~/Images/empleados_fotos/" + year + "/"; // your code goes here

                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        }
                        
                        if (Request.InputStream.Length > 0)
                        {
                            Registrar_foto(numero_empleado);
                            using (StreamReader reader = new StreamReader(Request.InputStream))
                            {
                                string hexString = Server.UrlEncode(reader.ReadToEnd());
                                //string imageName = drop_numero_empleado.SelectedValue;
                                string imagePath = subPath + Session["Reloj"].ToString() +".png";
                                File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                                Session["CapturedImage"] = ResolveUrl(imagePath);
                            }
                        }

                        //if (!string.IsNullOrEmpty(HttpContext.Current.Session["mostrarBoton"] as string))
                        //{
                            //if (Session["mostrarBoton"].ToString() == "2")
                            //{
                            //btn_capture.Visible = false;
                            //btn_captureNew.Visible = true;
                            //btn_empleadoNew.Visible = true;
                            //}
                        //}
                        //else
                        //{
                            btn_capture.Visible = true;
                            btn_captureNew.Visible = true;
                            btn_empleadoNew.Visible = true;
                        //}
                    }

                    else
                    {
                        //drop_numero_empleado.Enabled = true;

                        drop_numero_empleado_con_foto.Enabled = true;
                        drop_numero_empleado_sin_foto.Enabled = true;


                        btn_selectEmpleado.Visible = true;

                        table_empleadosFotos.Visible = false;

                        btn_capture.Visible = false;

                        btn_captureNew.Visible = false;
                        
                        btn_empleadoNew.Visible = false;
                    }
                }

                //Session.Add("mostrarBoton", vueltas);

                //vueltas += 1;
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
            }
        }

        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
            
            //byte[] bytes = new byte[hex.Length * sizeof(char)];
            //System.Buffer.BlockCopy(hex.ToCharArray(), 0, bytes, 0, bytes.Length);
            //return bytes;
        }

        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            string url = HttpContext.Current.Session["CapturedImage"].ToString();
            HttpContext.Current.Session["CapturedImage"] = null;
            return url;
        }

        //private void loadNoEmpleados()
        //{
        //    drop_numero_empleado.Items.Clear();
        //    drop_numero_empleado.Items.Add(new ListItem("-- Seleccionar --", ""));

        //    var load_noEmpleados = DbUtil.GetCursor("sp_recursos_empleado_fotos_LoadNoEmpleados"
                
        //        );

        //    drop_numero_empleado.DataSource = load_noEmpleados;
        //    drop_numero_empleado.DataTextField = "nombre";
        //    drop_numero_empleado.DataValueField = "numero";
        //    drop_numero_empleado.DataBind();


        //}

        private void loadNoEmpleadosSinFoto()
        {
            drop_numero_empleado_sin_foto.Items.Clear();
            drop_numero_empleado_sin_foto.Items.Add(new ListItem("-- Seleccionar --", "0"));

            var load_noEmpleados = DbUtil.GetCursor("sp_recursos_empleado_fotos_LoadNoEmpleados",
            new SqlParameter("@opcion", "1")
                );

            drop_numero_empleado_sin_foto.DataSource = load_noEmpleados;
            drop_numero_empleado_sin_foto.DataTextField = "nombre";
            drop_numero_empleado_sin_foto.DataValueField = "numero";
            drop_numero_empleado_sin_foto.DataBind();


        }

        private void loadNoEmpleadosConFoto()
        {
            drop_numero_empleado_con_foto.Items.Clear();
            drop_numero_empleado_con_foto.Items.Add(new ListItem("-- Seleccionar --", "0"));

            var load_noEmpleados = DbUtil.GetCursor("sp_recursos_empleado_fotos_LoadNoEmpleados",
            new SqlParameter("@opcion", "2")
                );

            drop_numero_empleado_con_foto.DataSource = load_noEmpleados;
            drop_numero_empleado_con_foto.DataTextField = "nombre";
            drop_numero_empleado_con_foto.DataValueField = "numero";
            drop_numero_empleado_con_foto.DataBind();


        }


        protected void btn_selectEmpleado_Click(object sender, EventArgs e)
        {

            if (drop_numero_empleado_con_foto.SelectedValue != "0" || drop_numero_empleado_sin_foto.SelectedValue != "0")
            {
                if (drop_numero_empleado_con_foto.SelectedValue != "0" && drop_numero_empleado_sin_foto.SelectedValue == "0")
                    Session.Add("Reloj", drop_numero_empleado_con_foto.SelectedValue);
                    //labelAlerta.Text = "Selecciono empleado con foto";
                if (drop_numero_empleado_con_foto.SelectedValue == "0" && drop_numero_empleado_sin_foto.SelectedValue != "0")
                    Session.Add("Reloj", drop_numero_empleado_sin_foto.SelectedValue);
                    //labelAlerta.Text = "Selecciono empleado sin foto";
                if (drop_numero_empleado_con_foto.SelectedValue != "0" && drop_numero_empleado_sin_foto.SelectedValue != "0")
                    labelAlerta.Text = "Seleccione una opción unicamente";
                else
                    Response.Redirect("empleados_fotos.aspx");

            }
            else
                labelAlerta.Text = "Seleccione una opción";


            //if (drop_numero_empleado.SelectedValue != "")
            //{

            //    Session.Add("Reloj", drop_numero_empleado.SelectedValue);
            //    Response.Redirect("empleados_fotos.aspx");
            //}
        }

        private void LoadImage(string empleado)
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
                        fotoActual.ImageUrl = "~/images/empleados_fotos/" + anio + "/" + empleado + ".png";
                        return;
                    }

                }
            }
            else
                fotoActual.ImageUrl = "~/images/empleados_fotos/no-foto.png";
        }
        //protected void btn_capture_Click(object sender, EventArgs e)
        //{
        //    Session.Add("mostrarBoton", "1");

        //    btn_capture.Visible = false;
        //    btn_captureNew.Visible = true;
        //    btn_empleadoNew.Visible = true;
        //}

        protected void btn_captureNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("empleados_fotos.aspx");
        }

        protected void btn_empleadoNew_Click(object sender, EventArgs e)
        {
            Session.Remove("Reloj");
            Session.Remove("mostrarBoton");
            Response.Redirect("empleados_fotos.aspx");
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Session.Remove("Reloj");
            Response.Redirect("empleados.aspx");
        }

         protected void Registrar_foto(string empleado)
        {
            
            var sp_registrar_foto = DbUtil.ExecuteProc("sp_recursos_registro_fotos",
                    new SqlParameter("@no_empleado", empleado));
        }


    }
}