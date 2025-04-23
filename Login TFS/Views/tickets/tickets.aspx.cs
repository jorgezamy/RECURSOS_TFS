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
    public partial class tickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadMenuRoles();

                    loadDepto();
                    loadDeptos();
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

            var sp_loadRol = DbUtil.ExecuteProc("sp_gcdm_tickets_tickets_roles_permitirAcceso",
                new SqlParameter("@usuario", usuario),
                MsBarco.DbUtil.NewSqlParam("@acceso_modulo", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_loadRol["@acceso_modulo"].ToString() == "1")
            {
                //Se tiene acceso al modulo
            }
            if (sp_loadRol["@acceso_modulo"].ToString() == "0")
            {
                Response.Redirect("~/views/menu.aspx");
            }
        }

        private void loadDepto()
        {
            var sp_loadDepto = DbUtil.ExecuteProc("sp_gcdm_tickets_tickets_loadDepto",
                new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@idDepto", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            Session.Add("idDepto", sp_loadDepto["@idDepto"].ToString());
        }

        private void loadDeptos()
        {
            drop_tickets_departamentos.Items.Clear();
            
            var sp_loadDeptos = DbUtil.GetCursor("sp_gcdm_tickets_tickets_loadDeptos",
                new SqlParameter("@idDepto", Session["idDepto"].ToString())
                );

            drop_tickets_departamentos.Items.Add(new ListItem("-- Seleccionar --", ""));

            drop_tickets_departamentos.DataSource = sp_loadDeptos;
            drop_tickets_departamentos.DataValueField = "idDepto";
            drop_tickets_departamentos.DataTextField = "descripcion";
            drop_tickets_departamentos.DataBind();
        }
        
        private void loadTickets()
        {
            var sp_loadTickets = DbUtil.GetCursor("sp_gcdm_tickets_tickets_loadGridView",
                new SqlParameter("@texto", txt_buscar.Text),
                new SqlParameter("@fecha_inicio", !string.IsNullOrEmpty(txt_fecha_inicio.Text) ? txt_fecha_inicio.Text : (object)DBNull.Value),
                new SqlParameter("@fecha_fin", !string.IsNullOrEmpty(txt_fecha_fin.Text) ? txt_fecha_fin.Text : (object)DBNull.Value),
                new SqlParameter("@idDeptoDrop", drop_tickets_departamentos.SelectedValue),
                new SqlParameter("@idDepto", Session["idDepto"].ToString())
                );

            grid_tickets.DataSource = sp_loadTickets;
            grid_tickets.DataBind();
        }

        protected void drop_tickets_departamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_tickets_departamentos.SelectedValue != "")
            {
                txt_buscar.Text = "";

                loadTickets();
                
                div_tickets.Visible = true;
                grid_tickets.Visible = true;
                btn_crearTIcket.Visible = true;
            }
            else
            {
                div_tickets.Visible = false;
                grid_tickets.Visible = false;
                btn_crearTIcket.Visible = false;
            }
        }

        protected void radio_buscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radio_buscar.SelectedValue == "1")
            {
                txt_buscar.Focus();

                Panel2.Visible = true;
                div_fecha.Visible = false;
            }
            else
            {
                Panel2.Visible = false;
                div_fecha.Visible = true;
            }
        }

        protected void bt_btnBuscarFecha_Click(object sender, ImageClickEventArgs e)
        {
            loadTickets();
        }

        protected void bt_btnbuscar_Click(object sender, ImageClickEventArgs e)
        {
            loadTickets();

            txt_buscar.Focus();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func_Calle()", true);
        }

        protected void grid_tickets_DataBound(object sender, EventArgs e)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[8].Visible = false;
            }

            foreach (GridViewRow row in grid_tickets.Rows)
            {
                row.Cells[8].Visible = false;
            }
        }

        protected void grid_tickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn_ver_ticket = (Button)e.Row.FindControl("btn_ver_ticket");

                if (e.Row.Cells[8].Text == "1")
                {
                    if (e.Row.Cells[1].Text == "Abierto" || e.Row.Cells[1].Text == "En proceso")
                    {
                        btn_ver_ticket.Text = "Editar";
                    }

                    else if (e.Row.Cells[1].Text == "Cerrado")
                    {
                        btn_ver_ticket.Text = "Ver";
                        e.Row.CssClass = "folioCerrado";
                    }

                    else
                    {
                        btn_ver_ticket.Text = "Ver";
                        e.Row.CssClass = "folioCancelado";
                    }
                }
                else
                {
                    btn_ver_ticket.Text = "Ver";

                    if (e.Row.Cells[1].Text == "Cerrado")
                    {
                        e.Row.CssClass = "folioCerrado";
                    }

                    if (e.Row.Cells[1].Text == "Cancelado")
                    {
                        e.Row.CssClass = "folioCancelado";
                    }
                }
            }
        }

        protected void grid_tickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_tickets.PageIndex = e.NewPageIndex;
            grid_tickets.DataBind();

            loadTickets();
        }

        protected void btn_ver_ticket_Click(object sender, EventArgs e)
        {
            var Link = (Control)sender;
            GridViewRow row = (GridViewRow)Link.NamingContainer;

            Button btn_ver_ticket = (Button)row.FindControl("btn_ver_ticket");

            GridViewRow row1 = grid_tickets.SelectedRow;
            var folio = row.Cells[2].Text;

            Session.Add("folioTicket", folio);
            Session.Add("idDeptoDestino", drop_tickets_departamentos.SelectedValue);
            
            if (btn_ver_ticket.Text == "Editar")
            {
                Session.Add("editarTicket", "1");
            }
            else
            {
                Session.Add("editarTicket", "0");
            }

            //Session.Remove("idDepto");
            Response.Redirect("tickets_editar.aspx");
        }

        protected void btn_crearTIcket_Click(object sender, EventArgs e)
        {
            Session["idDeptoDestino"] = Session["idDepto"].ToString();
            //Session.Remove("idDepto");
            Response.Redirect("tickets_crear.aspx");
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/views/menu.aspx");
        }
    }
}