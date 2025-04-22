using MsBarco;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace recursos.Views
{
    public partial class actualizar_fortia : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    if (Request.QueryString["opcion"].ToString() == "1")
                    {
                        Response.Redirect("Fortia/fortia_altas.aspx?opcion=" + "1");
                    }

                    permitirFiniquitos();
                    loadEmpleados();
                    loadFiniquitos_sinProcesar();
                    
                    DataTable table = new DataTable();
                    table.Columns.Add("Descripción", typeof(string));

                    table.Rows.Add("Altas");
                    table.Rows.Add("Bajas");
                    table.Rows.Add("Modificaciones");
                    table.Rows.Add("Movimientos");


                    gridview_tipoReporte.DataSource = table;
                    gridview_tipoReporte.DataBind();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        protected void grid_buscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string opcion = "";
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument); // Get the current row
                String tipo = gridview_tipoReporte.Rows[rowIndex].Cells[1].Text;


                label_alerta.Text = tipo;

                if (tipo == "Altas")
                    opcion = "1";

                if (tipo == "Bajas")
                    opcion = "2";

                if (tipo == "Modificaciones")
                    opcion = "3";

                if (tipo == "Movimientos")
                {
                    var sp_loadDatos = DbUtil.GetCursor("sp_recursos_fortia_movimientos_loadDatos");

                    grid_alertas_display.DataSource = sp_loadDatos;
                    grid_alertas_display.DataBind();
                }
                else
                {
                    var sp_loadGrid1 = DbUtil.GetCursor("sp_recursos_fortia_reportes_loadDatos",
                        new SqlParameter("@opcion", opcion)
                        );

                    grid_alertas_display.DataSource = sp_loadGrid1;
                    grid_alertas_display.DataBind();
                }

                ModalDatos.Show();
            }

            if (e.CommandName == "edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int GetNumero = Convert.ToInt32(gridview_tipoReporte.Rows[rowIndex].Cells[2].Text);
            }
        }

        private void permitirFiniquitos()
        {
            var sp_permitirFiniquitos = DbUtil.ExecuteProc("sp_recursos_fortia_finiquitos_permitirFiniquitos",
                new SqlParameter("@companiaID", Session["companiaID"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@permitirFiniquitos", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                );

            if (sp_permitirFiniquitos["@permitirFiniquitos"].ToString() == "1")
            {
                check_finiquitos.Visible = true;

                mensaje_permitirFiniquitos.Text = "";
            }
            else
            {
                check_finiquitos.Visible = false;
                btn_finiquitos.Visible = false;

                mensaje_permitirFiniquitos.Text = "Ya se procesaron los finiquitos... No se permite procesar el dia de hoy.";
            }
        }
        
        private void loadEmpleados()
        {
            drop_empleados.Items.Clear();

            var sp_loadEmpleados = DbUtil.GetCursor("sp_recursos_fortia_finiquitos_loadEmpleados",
                new SqlParameter("@companiaID", Session["companiaID"].ToString())
                );

            drop_empleados.Items.Add(new ListItem("-- Seleccionar --", ""));

            drop_empleados.DataSource = sp_loadEmpleados;
            drop_empleados.DataValueField = "no_empleado";
            drop_empleados.DataTextField = "nombre";
            drop_empleados.DataBind();
        }

        private void loadFiniquitos_sinProcesar()
        {
            var sp_load_finiquitos_sinProcesar = DbUtil.GetCursor("sp_recursos_fortia_finiquitos_load_Finiquitos_sinProcesar",
                new SqlParameter("@companiaID", Session["companiaID"].ToString())
                );

            grid_finiquitos_sinProcesar.DataSource = sp_load_finiquitos_sinProcesar;
            grid_finiquitos_sinProcesar.DataBind();

            if (mensaje_permitirFiniquitos.Text == "")
            {
                if (grid_finiquitos_sinProcesar.Rows.Count != 0)
                {
                    check_finiquitos.Visible = true;
                    btn_finiquitos.Visible = true;
                }
                else
                {
                    check_finiquitos.Visible = false;
                    btn_finiquitos.Visible = false;
                }
            }
            else
            {
                check_finiquitos.Visible = false;
                btn_finiquitos.Visible = false;
            }
        }

        protected void drop_empleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_empleados.SelectedValue != "")
            {
                var sp_loadFiniquitoDatos = DbUtil.ExecuteProc("sp_recursos_fortia_finiquitos_loadFiniquitoDatos",
                    new SqlParameter("@companiaID", Session["companiaID"].ToString()),
                    new SqlParameter("@no_empleado", drop_empleados.SelectedValue),
                    MsBarco.DbUtil.NewSqlParam("@nombreNumero", null, SqlDbType.VarChar, ParameterDirection.Output, 40),
                    MsBarco.DbUtil.NewSqlParam("@deptoPuesto", null, SqlDbType.VarChar, ParameterDirection.Output, 40)
                    );

                lbl_finiquito_nombre.Text = sp_loadFiniquitoDatos["@nombreNumero"].ToString();
                lbl_finiquito_puesto.Text = sp_loadFiniquitoDatos["@deptoPuesto"].ToString();

                mensaje_error.Text = "";
                btn_finiquitoSinProcesar.Visible = true;
            }
            else
            {
                lbl_finiquito_nombre.Text = "";
                lbl_finiquito_puesto.Text = "";

                btn_finiquitoSinProcesar.Visible = false;
            }

            check_finiquitos.Checked = false;
            loadFiniquitos_sinProcesar();
        }

        protected void btn_finiquitoSinProcesar_Click(object sender, EventArgs e)
        {
            if (drop_empleados.SelectedValue != "")
            {
                var sp_insertTo_sinProcesar = DbUtil.ExecuteProc("sp_recursos_fortia_finiquitos_insertTo_Finiquitos_sinProcesar",
                    new SqlParameter("@companiaID", Session["companiaID"].ToString()),
                    new SqlParameter("@noEmpleado", drop_empleados.SelectedValue),
                    new SqlParameter("@usuario", Session["usuario"].ToString())
                    );

                loadEmpleados();
                loadFiniquitos_sinProcesar();

                drop_empleados.SelectedValue = "";
                lbl_finiquito_nombre.Text = "";
                lbl_finiquito_puesto.Text = "";

                btn_finiquitoSinProcesar.Visible = false;

                mensaje_error.Text = "El empleado se puso en lista de espera.";
            }
        }

        protected void check_finiquitos_CheckedChanged(object sender, EventArgs e)
        {
            if (check_finiquitos.Checked)
            {
                btn_finiquitos.Visible = true;
            }
            else
            {
                btn_finiquitos.Visible = false;
            }

            mensaje_error.Text = "";
            btn_finiquitoSinProcesar.Visible = false;
        }

        protected void btn_finiquitos_Click(object sender, EventArgs e)
        {
            if (check_finiquitos.Checked)
            {
                var sp_insert_toFiniquitos = DbUtil.ExecuteProc("sp_recursos_fortia_finiquitos_insertTo_Finiquitos",
                    new SqlParameter("@companiaID", Session["companiaID"].ToString()),
                    new SqlParameter("@usuario", Session["usuario"].ToString())
                    );

                loadFiniquitos_sinProcesar();

                check_finiquitos.Checked = false;
                btn_finiquitos.Visible = false;

                mensaje_error.Text = "Finiquitos procesados exitosamente.";
                mensaje_permitirFiniquitos.Text = "Ya se procesaron los finiquitos... No se permite procesar el dia de hoy.";
            }
            else
            {
                mensaje_error.Text = "Primero debes de seleccionar el recuadro de ACEPTO.";
            }
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}