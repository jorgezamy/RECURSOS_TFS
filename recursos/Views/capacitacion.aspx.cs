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
    public partial class capacitacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadMenuRoles();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        protected void LoadMenuRoles()
        {
            var usuario = Session["usuario"].ToString();

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_capacitacion_roles_permitirAcceso",
               new SqlParameter("@usuario", usuario),
               MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@cadenaAccesos_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
               );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                a.Value = sp_loadRol["@cadenaAccesos_modulo"].ToString();
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("~/views/inicio.aspx");
            }
        }

        protected void b1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("capacitacion_cursos.aspx");
        }

        protected void b2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("capacitacion_asistencia.aspx");
        }

        protected void b3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("capacitacion_layout.aspx");
        }

        protected void b4_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("capacitacion_reportes.aspx");
        }

        protected void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}