using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
//using System.Data.DataSetExtensions;

namespace recursos.Views
{
    public partial class capacitacion_cursos_asignacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    loadCheckboxlist();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        private void loadCheckboxlist()
        {
            chkListPuestos.DataSource = null;

            var sp_loadPlantilla = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_asignacion_loadPuestos_plantilla");

            chkListPuestos.DataSource = sp_loadPlantilla;
            chkListPuestos.DataTextField = "puesto";
            chkListPuestos.DataValueField = "id_puesto";
            chkListPuestos.DataBind();
        }

        private void loadCheckboxesSelected()
        {
            var sp_loadPuestos = DbUtil.GetCursor("sp_recursos_capacitacion_cursos_asignacion_loadPuestos",
                new SqlParameter("@id_capacitacion_cursos", ddlCursos.SelectedValue)
                );

            foreach (ListItem item in chkListPuestos.Items)
            {
                if (sp_loadPuestos.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("id_puesto")).ToList().Contains(item.Value))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }

        protected void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("capacitacion_cursos.aspx");
        }

        protected void ddlCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCursos.SelectedValue != "")
            {
                loadCheckboxesSelected();
                divPuestos.Visible = true;
            }

            else
            {
                divPuestos.Visible = false;
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            var sp_deletePuestos = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_asignacion_delete",
                new SqlParameter("@id_capacitacion_cursos", ddlCursos.SelectedValue)
                );

            foreach (ListItem item in chkListPuestos.Items)
            {
                if (item.Selected)
                {
                    var sp_insertPuestos = DbUtil.ExecuteProc("sp_recursos_capacitacion_cursos_asignacion_insert",
                        new SqlParameter("@id_capacitacion_cursos", ddlCursos.SelectedValue),
                        new SqlParameter("@puesto", item.Value),
                        new SqlParameter("@usuario", Session["usuario"].ToString())
                        );
                }
            }
            loadCheckboxlist();
            loadCheckboxlist();
            //btn_guardar.Visible = false;
            //ImgLoading.Visible = true;

            chkSelect.Checked = false;

            ddlCursos.SelectedValue = "";
            divPuestos.Visible = false;

            modal_popup.Show();

        }
    }
}