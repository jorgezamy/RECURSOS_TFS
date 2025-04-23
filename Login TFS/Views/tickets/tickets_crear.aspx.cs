using MsBarco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login_TFS.Views.tickets
{
    public partial class tickets_crear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Session.Add("nombreUsuario", "JORGE ALFONSO ZAMORA BELLO");
            //Session.Add("numeroUsuario", "343");
            //Session.Add("usuario", "erika");

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    Load_departamentos();
                    loadTickets();
                    //LoadMenuRoles();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }


        private void loadTickets()
        {
            var sp_loadTickets = DbUtil.GetCursor("sp_gcdm_tickets_tickets_crear_loadGridView",
                new SqlParameter("@usuario", Session["numeroUsuario"].ToString())                
                );

            grid_tickets.DataSource = sp_loadTickets;
            grid_tickets.DataBind();
        }

        protected void btn_ver_ticket_Click(object sender, EventArgs e)
        {
            var Link = (Control)sender;
            GridViewRow row = (GridViewRow)Link.NamingContainer;

            Button btn_ver_ticket = (Button)row.FindControl("btn_ver_ticket");

            GridViewRow row1 = grid_tickets.SelectedRow;
            var folio = row.Cells[2].Text;

            Session.Add("folioTicket", folio);

            if (btn_ver_ticket.Text == "Editar")
            {
                Session.Add("editarTicket", "1");
            }
            else
            {
                Session.Add("editarTicket", "0");
            }

            Response.Redirect("tickets_editar.aspx");

        }

        protected void Load_departamentos()
        {
            var sp_DropDownLists_departamentos = DbUtil.GetCursor("sp_gcdm_tickets_tickets_crear_loadDepartamentos");

            dropdown_departamento.DataSource = sp_DropDownLists_departamentos;
            dropdown_departamento.DataValueField = "id_depto";
            dropdown_departamento.DataTextField = "desc_esp";
            dropdown_departamento.Items.Add(new ListItem("-- Seleccionar --", ""));

            dropdown_departamento.DataBind();
        }

        protected void btn_generar_Click(object sender, EventArgs e)
        {
            popUp_editar.Show();
        }

        protected void btn_cerrarPopUp_guardar_si_Click(object sender, EventArgs e)
        {

            var sp_generarfolio = DbUtil.ExecuteProc("sp_gcdm_tickets_tickets_crear_generarfolio",
                            new SqlParameter("@requerimentos", tb_requerimentos.Text),
                            new SqlParameter("@asunto", tb_asunto.Text),
                            new SqlParameter("@depto_destino", dropdown_departamento.SelectedValue),
                            new SqlParameter("@usuario", Session["numeroUsuario"].ToString()),
                            MsBarco.DbUtil.NewSqlParam("@folio", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 35)
                            );


            no_folio.Text = sp_generarfolio["@folio"].ToString();

            //no_folio.Text = "5";

            requerimento.Text = tb_requerimentos.Text;

            fecha.Text = sp_generarfolio["@fecha"].ToString();

            departamento.Text = dropdown_departamento.SelectedItem.Text;

            asunto.Text = tb_asunto.Text;

            modal_exito.Show();
        }

        protected void btn_generar_folio_Click(object sender, EventArgs e)
        {
            //Response.Redirect("http://18.219.12.59:81/Views/tickets/tickets_crear.aspx");
            Response.Redirect("tickets_crear.aspx");

        }

        protected void btn_revisar_folio_Click(object sender, EventArgs e)
        {
            //Response.Redirect("http://18.219.12.59:81/Views/tickets/tickets.aspx");
            Session.Add("folioTicket", no_folio.Text);

            Session.Add("editarTicket", "0");
            Response.Redirect("tickets_editar.aspx");

        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("http://18.219.12.59:81/Views/tickets/tickets.aspx");
            Response.Redirect("tickets.aspx");

        }

        protected void grid_tickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            loadTickets();
            grid_tickets.PageIndex = e.NewPageIndex;
            grid_tickets.DataBind();
        }
    }
}