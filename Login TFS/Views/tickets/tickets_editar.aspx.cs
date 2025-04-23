using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;

namespace Login_TFS.Views.tickets
{
    public partial class tickets_editar : System.Web.UI.Page
    {

        String estatus;
        String departamento_final;
        String status_ticket;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session.Add("folioTicket", "14");
                //Session.Add("editarTicket", "1");

                //Session.Add("nombreUsuario", "JORGE ALFONSO ZAMORA BELLO");
                //Session.Add("numeroUsuario", "2868");
                //Session.Add("usuario", "jzamora");


                //Session.Add("usuario", "jzamora");
                LoadInfo();

                nombreUsuario.Text = Session["nombreUsuario"].ToString();
            }
        }

        private void Bind_gridview()
        {
            var folio = Session["folioTicket"].ToString();
            var idDeptoDestino = Session["idDeptoDestino"].ToString();
            var editar = Session["editarTicket"].ToString();


            var sp_mostrarTickets = DbUtil.GetCursor("sp_gcdm_tickets_tickets_editar_loadGrid",
                new SqlParameter("@folio", folio),
                new SqlParameter("@idDeptoDestino", idDeptoDestino)
                );

            grid_editarTickets.DataSource = sp_mostrarTickets;
            grid_editarTickets.DataBind();


            var sp_info = DbUtil.ExecuteProc("sp_gcdm_tickets_tickets_editar_loadInfo",
                new SqlParameter("@folio", folio),
                MsBarco.DbUtil.NewSqlParam("@departamento_origen", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                MsBarco.DbUtil.NewSqlParam("@departamento_destino", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                MsBarco.DbUtil.NewSqlParam("@asunto", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                MsBarco.DbUtil.NewSqlParam("@requerimiento", null, SqlDbType.VarChar, ParameterDirection.Output, 1500),
                MsBarco.DbUtil.NewSqlParam("@estatus", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
                );

            info_departamento.Text = sp_info["@departamento_origen"].ToString();


            no_folio.Text = folio;
            info_asunto.Text = sp_info["@asunto"].ToString();
            info_requerimiento.Text = sp_info["@requerimiento"].ToString();
            estatus = sp_info["@estatus"].ToString();
        }

        private void LoadInfo()
        {
            var sp_info = DbUtil.ExecuteProc("sp_gcdm_tickets_tickets_editar_loadInfo",
                new SqlParameter("@folio", Session["folioTicket"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@departamento_origen", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                MsBarco.DbUtil.NewSqlParam("@departamento_destino", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                MsBarco.DbUtil.NewSqlParam("@asunto", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                MsBarco.DbUtil.NewSqlParam("@requerimiento", null, SqlDbType.VarChar, ParameterDirection.Output, 1500),
                MsBarco.DbUtil.NewSqlParam("@estatus", null, SqlDbType.VarChar, ParameterDirection.Output, 50)
                );

            departamento_final = sp_info["@departamento_destino"].ToString();




            Bind_gridview();

            if (Session["editarTicket"].ToString() == "0")
            {
                grid_editarTickets.ShowFooter = false;
                grid_editarTickets.FooterRow.Visible = false;

                grid_editarTickets.DataBind();
            }
            else
            {
                if (estatus == "0" || estatus == "3" || estatus == "4")
                {
                    grid_editarTickets.ShowFooter = false;
                    grid_editarTickets.FooterRow.Visible = false;

                    grid_editarTickets.DataBind();
                }
                else
                {
                    grid_editarTickets.ShowFooter = true;
                    grid_editarTickets.FooterRow.Visible = true;

                    grid_editarTickets.DataBind();
                }
            }
        }
        
        protected void grid_editarTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = Session["usuario"].ToString();
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlDep = (DropDownList)e.Row.FindControl("footer_ddlDepto");

                ddlDep.SelectedValue = departamento_final;
            }
        }

        protected void grid_editarTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Bind_gridview();
            grid_editarTickets.PageIndex = e.NewPageIndex;

            if ( grid_editarTickets.PageIndex == 0)
            {
                grid_editarTickets.ShowFooter = true;
            }
            else
            {
                grid_editarTickets.ShowFooter = false;
            }

            grid_editarTickets.DataBind();
        }

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            popUp_editar.Show();
        }

        protected void btn_cerrarPopUp_guardar_si_Click(object sender, EventArgs e)
        {
            //var link = (Control)sender;

            //GridViewRow row = //(GridViewRow)link.NamingContainer;
            //     grid_editarTickets.SelectedRow; 
            //HiddenField ticket_status = (HiddenField)row.FindControl("footer_ddlEstatus");
            TextBox txt_resolucion = (TextBox)grid_editarTickets.FooterRow.FindControl("txt_resolucion");
            DropDownList ticket_status = (DropDownList)grid_editarTickets.FooterRow.FindControl("footer_ddlEstatus");
            DropDownList depto_destino = (DropDownList)grid_editarTickets.FooterRow.FindControl("footer_ddlDepto");


            var sp_actualizar = DbUtil.ExecuteProc("sp_gcdm_tickets_tickets_editar_guardar",
                new SqlParameter("@folio", no_folio.Text),
                new SqlParameter("@id_tickets_status", ticket_status.SelectedValue),
                new SqlParameter("@revisado", Session["usuario"].ToString()),
                new SqlParameter("@resolucion", txt_resolucion.Text),
                new SqlParameter("@depto_destino", depto_destino.SelectedValue));


            status_ticket = ticket_status.SelectedValue;

            modal_exito.Show();
        }
        
        protected void btn_aceptar_Click(object sender, EventArgs e)
        {

            DropDownList ticket_status = (DropDownList)grid_editarTickets.FooterRow.FindControl("footer_ddlEstatus");


            if (ticket_status.SelectedValue == "4")
                Response.Redirect("tickets.aspx");
            else
                Response.Redirect("tickets_editar.aspx");
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Session.Remove("folioTicket");
            Session.Remove("editarTicket");
            Session.Remove("idDeptoDestino");

            Response.Redirect("tickets.aspx");
        }

        protected void footer_ddlEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            String id_departamento;


            DropDownList ddlDepartamento = (DropDownList)grid_editarTickets.FooterRow.FindControl("footer_ddlDepto");
            DropDownList ticket_status = (DropDownList)grid_editarTickets.FooterRow.FindControl("footer_ddlEstatus");

            id_departamento = ddlDepartamento.SelectedValue;


            if (ticket_status.SelectedValue == "4")
            {
                ListItem itm = ddlDepartamento.Items.FindByValue(id_departamento);
                ddlDepartamento.Enabled = true;
                ddlDepartamento.SelectedValue = "";
                //ddlDepartamento.Items.Remove(id_departamento);
                ddlDepartamento.Items.Remove(itm);
            }
            else
            {
                ddlDepartamento.Enabled = false;
            }


        }
    }
}