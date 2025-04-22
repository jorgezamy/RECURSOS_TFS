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
    public partial class bonos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadGridBonos();
                }
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
            }

        }
        
        private void LoadGridBonos()
        {
            var sp_loadBonos = DbUtil.GetCursor("sp_recursos_bonos_loadDatos"
                    //new SqlParameter("@compania", drop_compania.SelectedValue)
                    );

            grid_bonos.DataSource = sp_loadBonos;
            grid_bonos.DataBind();
            //grid_buscar.AutoResizeColumns();
            //grid_buscar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        protected void grid_bonos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_bonos.PageIndex = e.NewPageIndex;
            grid_bonos.DataBind();

            LoadGridBonos();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != grid_bonos.EditIndex)
            {
                (e.Row.Cells[3].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('¿Desea eliminar este bono?');";
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            grid_bonos.EditIndex = e.NewEditIndex;
            this.LoadGridBonos();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            grid_bonos.EditIndex = -1;
            this.LoadGridBonos();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = grid_bonos.Rows[e.RowIndex];
            int idBonoOperador = Convert.ToInt32(grid_bonos.DataKeys[e.RowIndex].Values[0]);
            string descripcion = (row.FindControl("txtBono") as TextBox).Text;
            string quinta = (row.FindControl("txtQuinta") as TextBox).Text;
            string rabon = (row.FindControl("txtRabon") as TextBox).Text;

            var sp_actualizarBono = DbUtil.ExecuteProc("sp_recursos_bonos_editarDatos",
                new SqlParameter("@id_aportacion_deduccion_concepto", idBonoOperador),
                new SqlParameter("@descripcion", descripcion),
                new SqlParameter("@valorQuinta", quinta),
                new SqlParameter("@valorRabon", rabon)
                );

            grid_bonos.EditIndex = -1;
            this.LoadGridBonos();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idBonoOperador = Convert.ToInt32(grid_bonos.DataKeys[e.RowIndex].Values[0]);

            var sp_eliminarBono = DbUtil.ExecuteProc("sp_recursos_bonos_eliminar",
                new SqlParameter("@id_aportacion_deduccion_concepto", idBonoOperador)
                );

            this.LoadGridBonos();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string bono = txtBono.Text;
            string valorQuinta = txtQuinta.Text;
            string valorRabon = txtRabon.Text;

            var sp_registrarBono = DbUtil.ExecuteProc("sp_recursos_bonos_insertar",
                new SqlParameter("@descripcion", bono),
                new SqlParameter("@valorQuinta", valorQuinta),
                new SqlParameter("@valorRabon", valorRabon),
                MsBarco.DbUtil.NewSqlParam("@acceso", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_registrarBono["@acceso"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Bono insertado correctamente.');", true);
                txtBono.Text = "";
                txtQuinta.Text = "";
                txtRabon.Text = "";
            }

            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('El bono ya existe en la base de datos.');", true);
            }

            this.LoadGridBonos();
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("salarios.aspx");
        }
    }
}