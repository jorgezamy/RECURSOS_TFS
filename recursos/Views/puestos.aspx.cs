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
    public partial class puestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    LoadMenuRoles();

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

            var sp_loadRol = DbUtil.ExecuteProc("sp_recursos_puestos_roles_permitirAcceso",
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

        private void LoadGridPuestos()
        {
            var sp_loadPuestos = DbUtil.GetCursor("sp_recursos_puestos_cargarPuestos",
                    new SqlParameter("@compania", drop_compania.SelectedValue)
                    );

            grid_buscar.DataSource = sp_loadPuestos;
            grid_buscar.DataBind();
            //grid_buscar.AutoResizeColumns();
            //grid_buscar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void ShowNoResultFound(DataTable source, GridView gv)
        {
            source.Rows.Add(source.NewRow()); // create a new blank row to the DataTable
                                              // Bind the DataTable which contain a blank row to the GridView
            gv.DataSource = source;
            gv.DataBind();
            // Get the total number of columns in the GridView to know what the Column Span should be
            int columnsCount = gv.Columns.Count;
            gv.Rows[0].Cells.Clear();// clear all the cells in the row
            gv.Rows[0].Cells.Add(new TableCell()); //add a new blank cell
            gv.Rows[0].Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

            //You can set the styles here
            gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gv.Rows[0].Cells[0].Font.Bold = true;
            //set No Results found to the new added cell
            gv.Rows[0].Cells[0].Text = "NO RESULT FOUND!";
        }

        protected void drop_compania_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drop_compania.SelectedValue != "")
            {
                lbl_compania.Text = drop_compania.SelectedItem.ToString();

                //LoadGridPuestos();
                Buscar();
                table_btnBuscar.Visible = true;
            }
            else
            {
                lbl_compania.Text = "";
            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control control = null;
            if (grid_buscar.FooterRow != null)
            {
                control = grid_buscar.FooterRow;
            }
            else
            {
                control = grid_buscar.Controls[0].Controls[0];
            }

            string depto = (control.FindControl("ddlDepartamento") as DropDownList).SelectedValue;
            //DropDownList depto = control.FindControl("ddlDepartamento") as DropDownList;
            DropDownList supervisor = control.FindControl("ddlSupervisor") as DropDownList;

            supervisor.Items.Clear();

            if (depto != "")
            {

                var sp_loadSupervisores = DbUtil.GetCursor("sp_direccion_puestos_cargarSupervisores",
                    new SqlParameter("@depto", depto)
                    );

                supervisor.Items.Add(new ListItem("-- Seleccionar --", ""));

                supervisor.DataSource = sp_loadSupervisores;
                supervisor.DataValueField = "idPuesto";
                supervisor.DataTextField = "Puesto";
                supervisor.DataBind();
            }
            else
            {
                supervisor.Items.Add(new ListItem("-- Seleccione supervisor --", ""));
            }
        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            btn_guardarEdit.CssClass = "btn_guardarCancelar_disabled";
            btn_guardarEdit.Enabled = false;

            var Link = (Control)sender;
            GridViewRow row = (GridViewRow)Link.NamingContainer;

            //Label lblPuesto = (Label)row.FindControl("lblPuesto");
            //Label lblPosition = (Label)row.FindControl("lblPosition");
            //Label lblBonos = (Label)row.FindControl("lblBonos");
            //Label lblCantidad = (Label)row.FindControl("lblCantidad");
            //Label lblSalario = (Label)row.FindControl("lblSalario");
            //Label lblTipo = (Label)row.FindControl("lblTipo");
            //Label lblTurno = (Label)row.FindControl("lblTurno");
            //Label lblCompania = (Label)row.FindControl("lblCompania");

            string id_puesto = Convert.ToString(grid_buscar.DataKeys[row.RowIndex].Value);

            HFeditar.Value = Convert.ToString(id_puesto);

            MultiView_editar.ActiveViewIndex = 0;

            //txtPuestoEdit.Text = lblPuesto.Text;
            //txtPositionEdit.Text = lblPosition.Text;
            //txtBonosEdit.Text = lblBonos.Text;
            //txtCantidadEdit.Text = lblCantidad.Text;
            //txtSalarioEdit.Text = lblSalario.Text;
            //ddlTipoEdit.SelectedIndex = ddlTipoEdit.Items.IndexOf(ddlTipoEdit.Items.FindByText(lblTipo.Text));
            //ddlTurnoEdit.SelectedValue = lblTurno.Text;
            //lblCompaniaEditVer.Text = lbl_compania.Text;

            var sp_loadPuesto = DbUtil.ExecuteProc("sp_recursos_puestos_editPuestos_cargarDatos",
               new SqlParameter("@id_puesto", id_puesto),
               new SqlParameter("@id_compania", drop_compania.SelectedValue),
               MsBarco.DbUtil.NewSqlParam("@descripcion", null, SqlDbType.VarChar, ParameterDirection.Output, 60),
               MsBarco.DbUtil.NewSqlParam("@description", null, SqlDbType.VarChar, ParameterDirection.Output, 60),
               //MsBarco.DbUtil.NewSqlParam("@bonos", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               //MsBarco.DbUtil.NewSqlParam("@cantidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@salario_integrado", null, SqlDbType.VarChar, ParameterDirection.Output, 25),
               MsBarco.DbUtil.NewSqlParam("@bono_despensa", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@bono_puntualidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@bono_asistencia", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               //MsBarco.DbUtil.NewSqlParam("@id_tipo", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@turno", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
               MsBarco.DbUtil.NewSqlParam("@compania", null, SqlDbType.VarChar, ParameterDirection.Output, 100)
               );

            lblPuestoEdit.Text = sp_loadPuesto["@descripcion"].ToString();
            lblPositionEdit.Text = sp_loadPuesto["@description"].ToString();
            //txtBonosEdit.Text = sp_loadPuesto["@bonos"].ToString();
            //txtCantidadEdit.Text = sp_loadPuesto["@cantidad"].ToString();
            txtSalarioEdit.Text = sp_loadPuesto["@salario"].ToString();
            txtSalarioIntegrado.Text = sp_loadPuesto["@salario_integrado"].ToString();
            //txtDespensaEdit.Text = sp_loadPuesto["@bono_despensa"].ToString();
            //txtPuntualidadEdit.Text = sp_loadPuesto["@bono_puntualidad"].ToString();
            //txtAsistenciaEdit.Text = sp_loadPuesto["@bono_asistencia"].ToString();
            //ddlTipoEdit.SelectedValue = sp_loadPuesto["@id_tipo"].ToString();
            ddlTurnoEdit.SelectedValue = sp_loadPuesto["@turno"].ToString();
            lblCompaniaEditVer.Text = sp_loadPuesto["@compania"].ToString();
        }

        protected void Add(object sender, EventArgs e)
        {
            Control control = null;
            if (grid_buscar.FooterRow != null)
            {
                control = grid_buscar.FooterRow;
            }

            else
            {
                control = grid_buscar.Controls[0].Controls[0];
            }

            //eliminar bonos
            //eliminar o cambiar cantidad
            //add cantidad a programar
            //add fecha a programar
            //eliminar salario
            //eliminar tipo
            //eliminar turno

            string supervisor = (control.FindControl("ddlSupervisor") as DropDownList).SelectedValue;
            string descripcion = (control.FindControl("txtPuesto") as TextBox).Text;
            string description = (control.FindControl("txtPosition") as TextBox).Text;
            string depto = (control.FindControl("ddlDepartamento") as DropDownList).SelectedValue;
            string cantidad = (control.FindControl("txtCantidadProgramar") as TextBox).Text;
            string fecha = (control.FindControl("txtFechaProgramar") as TextBox).Text;
            DateTime dt = Convert.ToDateTime(fecha);

            var sp_insertDatos = DbUtil.ExecuteProc("sp_direccion_puestos_insertarPuesto",
                        new SqlParameter("@supervisor", supervisor),
                        new SqlParameter("@descripcion", descripcion),
                        new SqlParameter("@description", description),
                        new SqlParameter("@compania", drop_compania.SelectedValue),
                        new SqlParameter("@depto", depto),
                        new SqlParameter("@cantidad", cantidad),
                        new SqlParameter("@fecha", dt)
                        //MsBarco.DbUtil.NewSqlParam("@exitoso", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                        );

            //Response.Redirect(Request.Url.AbsoluteUri);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Puesto insertado correctamente.');", true);

            //LoadGridPuestos();

            Buscar();

            //if (sp_insertDatos["@exitoso"].ToString() == "1")
            //{
            //    Response.Redirect(Request.Url.AbsoluteUri);
            //}

            //else
            //{
            //    errorInsertar.Text = "El departamento ya existe.";
            //}

        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void grid_buscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_buscar.PageIndex = e.NewPageIndex;
            grid_buscar.DataBind();

            //LoadGridPuestos();

            Buscar();
        }

        protected void btn_guardarEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string id_puesto;

                id_puesto = HFeditar.Value;

                var sp_insertDatos = DbUtil.ExecuteProc("sp_recursos_puestos_editPuestos",
                        new SqlParameter("@id_puesto", id_puesto),
                        new SqlParameter("@id_compania", drop_compania.SelectedValue),
                        new SqlParameter("@salario", txtSalarioEdit.Text),
                        new SqlParameter("@salario_integrado", txtSalarioIntegrado.Text),
                        //new SqlParameter("@bono_despensa", txtDespensaEdit.Text),
                        //new SqlParameter("@bono_puntualidad", txtPuntualidadEdit.Text),
                        //new SqlParameter("@bono_asistencia", txtAsistenciaEdit.Text),
                        new SqlParameter("@turno", ddlTurnoEdit.SelectedValue)
                        );

                MultiView_editar.ActiveViewIndex = 1;

                editar_mensaje.Text = "El puesto se ha editado correctamente.";

                //Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void editar_continuar_Click(object sender, EventArgs e)
        {
            //LoadGridPuestos();

            Buscar();
        }

        private void Buscar()
        {
            var sp_buscar = DbUtil.GetCursor("sp_recursos_puestos_cargarPuestos",
                new SqlParameter("@puesto", tb_buscarNumero.Text),
                new SqlParameter("@compania", drop_compania.SelectedValue)
                );

            grid_buscar.DataSource = sp_buscar;
            grid_buscar.DataBind();
        }
        protected void bt_btnbuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            tb_buscarNumero.Focus();
        }



        protected void btn_calcular_Click(object sender, EventArgs e)
        {
            if (txtSalarioEdit.Text != "0")
            {
                txtSalarioIntegrado.Text = String.Format("{0:0.00}", Double.Parse(txtSalarioEdit.Text) * 1.0452);
                //txtSalarioIntegrado.Text = (Double.Parse(txtSalarioEdit.Text) * 1.0452).ToString();
                if (txtSalarioIntegrado.Text != "0")
                {
                    btn_guardarEdit.Enabled = true;
                    btn_guardarEdit.CssClass = "btn_guardarCancelar";
                }
                else
                {
                    btn_guardarEdit.CssClass = "btn_guardarCancelar_disabled";
                    btn_guardarEdit.Enabled = false;

                }
            }
            else
            {
                btn_guardarEdit.CssClass = "btn_guardarCancelar_disabled";
                btn_guardarEdit.Enabled = false;
            }
        }
    }
}