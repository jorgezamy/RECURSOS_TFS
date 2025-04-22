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
    public partial class incidencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadGridIncidencias();

                    LoadDDLDepartamento();

                }
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
            }
        }

        private void LoadMenuRoles()
        {
            var usuario = Session["usuario"].ToString();

            var sp_loadRol = DbUtil.ExecuteProc("sp_direccion_roles_roles_permitirAcceso",
               new SqlParameter("@usuario", usuario),
               MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
               );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                //Se tiene acceso al modulo
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("inicio.aspx");
            }
        }

        private void LoadGridIncidencias()
        {
            var sp_loadIncidencias = DbUtil.GetCursor("sp_recursos_incidencias_loadDatos"
                    //new SqlParameter("@compania", drop_compania.SelectedValue)
                    );

            grid_incidencias.DataSource = sp_loadIncidencias;
            grid_incidencias.DataBind();
            //grid_buscar.AutoResizeColumns();
            //grid_buscar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void LoadDDLDepartamento()
        {
            var sp_loadDeptos = DbUtil.GetCursor("sp_recursos_incidencias_loadDatos_loadDDL"
                );

            ddlDepartamento.Items.Clear();
            ddlDepartamento.Items.Add(new ListItem("--Seleccionar--", ""));
            ddlDepartamento.AppendDataBoundItems = true;
            ddlDepartamento.DataSource = sp_loadDeptos;
            ddlDepartamento.DataTextField = "desc_esp";
            ddlDepartamento.DataValueField = "id_depto";
            ddlDepartamento.DataBind();

            

            //ddlDepartamento.Items.FindByValue("").Selected = true;
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != grid_incidencias.EditIndex)
            {
                (e.Row.Cells[3].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('¿Desea eliminar esta incidencia?');";
            }
            if (e.Row.RowType == DataControlRowType.DataRow && grid_incidencias.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlDepto = (DropDownList)e.Row.FindControl("ddlDepartamento");

                var sp_loadDeptos = DbUtil.GetCursor("sp_recursos_incidencias_loadDatos_loadDDL"
                    );

                ddlDepto.DataSource = sp_loadDeptos;
                ddlDepto.DataTextField = "desc_esp";
                ddlDepto.DataValueField = "id_depto";
                ddlDepto.DataBind();
                //string selectedDepto = DataBinder.Eval(e.Row.DataItem, "descripcion").ToString();
                //string myName = (string)DataBinder.Eval(e.Row.DataItem, "descripcion");
                //ddlDepto.Items.FindByValue(myName).Selected = true;
                string selectedCity = DataBinder.Eval(e.Row.DataItem, "departamento").ToString();
                ddlDepto.Items.FindByText(selectedCity).Selected = true;
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            grid_incidencias.EditIndex = e.NewEditIndex;
            this.LoadGridIncidencias();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            grid_incidencias.EditIndex = -1;
            this.LoadGridIncidencias();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = grid_incidencias.Rows[e.RowIndex];
            int idIncidencia = Convert.ToInt32(grid_incidencias.DataKeys[e.RowIndex].Values[0]);
            string depto = (row.FindControl("ddlDepartamento") as DropDownList).SelectedValue;
            string descripcion = (row.FindControl("txtIncidencia") as TextBox).Text;
            string no_permitidas = (row.FindControl("txtPermitidas") as TextBox).Text;

            var sp_actualizarIncidencia = DbUtil.ExecuteProc("sp_recursos_incidencias_editarDatos",
                new SqlParameter("@id_incidencia", idIncidencia),
                new SqlParameter("@id_depto", depto),
                new SqlParameter("@descripcion", descripcion),
                new SqlParameter("@no_permitidas", no_permitidas)
                );

            grid_incidencias.EditIndex = -1;
            this.LoadGridIncidencias();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idIncidencia = Convert.ToInt32(grid_incidencias.DataKeys[e.RowIndex].Values[0]);

            var sp_eliminarIncidencia = DbUtil.ExecuteProc("sp_recursos_incidencias_eliminar",
                new SqlParameter("@id_incidencia", idIncidencia)
                );

            this.LoadGridIncidencias();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string incidencia = txtIncidencia.Text;
            string no_permitidas = txtPermitidas.Text;

            var sp_registrarIncidencia = DbUtil.ExecuteProc("sp_recursos_incidencias_insertar",
                new SqlParameter("@id_depto", ddlDepartamento.SelectedValue),
                new SqlParameter("@descripcion", incidencia),
                new SqlParameter("@no_permitidas", no_permitidas),
                MsBarco.DbUtil.NewSqlParam("@acceso", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_registrarIncidencia["@acceso"].ToString() == "1")
             {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Incidencia insertada correctamente.');", true);
                txtIncidencia.Text = "";
                txtPermitidas.Text = "";
                ddlDepartamento.SelectedValue = "";
            }

            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('La incidencia ya existe en la base de datos.');", true);
            }

            this.LoadGridIncidencias();
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

    }
}