using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;

namespace recursos.Views
{
    public partial class entradas_salidas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
            {
                if (!IsPostBack)
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();

                    enableDropClientes();
                    enableDropEmpleados();
                    load_departamentos();
                    load_noEmpleados();
                    //BindGrid();
                }
            }
            else
            {
                Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
            }
        }

        private void enableDropEmpleados()
        {
            var sp_enableClientes = DbUtil.ExecuteProc("sp_recursos_reloj_entradas_enableDropEmpleados",
                  new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
                  new SqlParameter("@companiaID", Session["companiaID"].ToString()),
                  MsBarco.DbUtil.NewSqlParam("@acceso", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
                  );

            if (sp_enableClientes["@acceso"].ToString() == "1")
            {
                load_clientes();
                drop_noEmpleado.Enabled = true;
            }
            else
            {
                drop_noEmpleado.Enabled = false;
            }
        }

        private void enableDropClientes()
        {
            var sp_enableClientes = DbUtil.ExecuteProc("sp_recursos_reloj_entradas_enableDropClientes",
               new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
               new SqlParameter("@companiaID", Session["companiaID"].ToString()),

               MsBarco.DbUtil.NewSqlParam("@acceso", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
               );

            if (sp_enableClientes["@acceso"].ToString() == "1")
            {
                load_clientes();
                drop_cliente.Enabled = true;
            }
            else
            {
                drop_cliente.Enabled = false;
            }


            //var sp_enableClientes = DbUtil.ExecuteProc("sp_recursos_reloj_entradas_enableDropClientes",
            //    new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
            //    MsBarco.DbUtil.NewSqlParam("@acceso", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
            //    );

            //if (sp_enableClientes["@acesso"].ToString() == "1")
            //{
            //    drop_cliente.Enabled = true;
            //}
            //else
            //{
            //    drop_cliente.Enabled = false;
            //}
        }

        private void load_departamentos()
        {
            try
            {
                var sp_loadDepartamentos = DbUtil.GetCursor("sp_recursos_reloj_entradas_loadDepartamentos",
                    new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
                    new SqlParameter("@companiaID", Session["companiaID"].ToString())
                    );

                drop_depto.DataSource = sp_loadDepartamentos;
                drop_depto.DataTextField = "descripcion";
                drop_depto.DataValueField = "id_depto";
                drop_depto.DataBind();
            }
            catch
            {
                drop_depto.Enabled = false;
            }
        }

        private void load_clientes()
        {
            try
            {
                var sp_loadClientes = DbUtil.GetCursor("sp_recursos_reloj_entradas_loadClientes",
                    new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
                    new SqlParameter("@companiaID", Session["companiaID"].ToString())

                    );

                drop_cliente.DataSource = sp_loadClientes;
                drop_cliente.DataTextField = "descripcion";
                drop_cliente.DataValueField = "id_cliente";
                drop_cliente.DataBind();
            }
            catch
            {
                drop_cliente.Enabled=false;
            }
        }

        private void load_noEmpleados()
        {
            try
            {
                drop_noEmpleado.Items.Clear();
                drop_noEmpleado.Items.Add(new ListItem("-- Seleccionar --",""));

                var sp_loadNoEmpleados = DbUtil.GetCursor("sp_recursos_reloj_entradas_loadNoEmpleado",
                    new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
                    new SqlParameter("@departamento_buscar", drop_depto.SelectedItem.Text),
                    new SqlParameter("@cliente", drop_cliente.SelectedItem.Text),
                    new SqlParameter("@companiaID", Session["companiaID"].ToString())

                    );

                drop_noEmpleado.DataSource = sp_loadNoEmpleados;
                drop_noEmpleado.DataTextField = "nombre";
                drop_noEmpleado.DataValueField = "numero";
                drop_noEmpleado.DataBind();
            }
            catch
            { }
        }

        protected void BindGrid()
        {
            //int value = DateTime.Compare(Convert.ToDateTime(fecha_inicial.Text), Convert.ToDateTime(fecha_final.Text));

            //// checking 
            //if (value > 0)
            //{
            //    txtAlerta.Text = "Fecha entrada es mayor que fecha salida";
            //}
            //else
            //{
            //    txtAlerta.Text = "";

            var sp_loadEntradas = DbUtil.GetCursor("sp_recursos_reloj_entradas",
                new SqlParameter("@no_empleado", Session["numeroUsuario"].ToString()),
                new SqlParameter("@departamento_buscar", !string.IsNullOrEmpty(drop_depto.SelectedItem.Text) ? drop_depto.SelectedItem.Text : (object)DBNull.Value),
                new SqlParameter("@no_empleado_buscar", !string.IsNullOrEmpty(drop_noEmpleado.SelectedValue) ? drop_noEmpleado.SelectedValue : (object)DBNull.Value),
                new SqlParameter("@cliente", drop_cliente.SelectedItem.Text),
                new SqlParameter("@FechaInicio", !string.IsNullOrEmpty(fecha_inicial.Text) ? fecha_inicial.Text : (object)DBNull.Value),
                new SqlParameter("@fechaFin", !string.IsNullOrEmpty(fecha_final.Text) ? fecha_final.Text : (object)DBNull.Value),
                new SqlParameter("@companiaID", Session["companiaID"].ToString())

                );

            grid_entradas.DataSource = sp_loadEntradas;
            grid_entradas.DataBind();

            //}

            //if (grid_entradas.Rows.Count != 0)
            //{
            //    btnExportar.Enabled = true;
            //    Visible = true;
            //}
            //else
            //{
            //    btnExportar.Enabled = false;
            //    btnExportar.Visible = false;
            //}
        }

        protected void drop_depto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_depto.SelectedValue != "")
            {
                if (drop_depto.SelectedValue == "6")
                {
                    load_clientes();

                    drop_cliente.Enabled = true;
                }
                else
                {
                    load_noEmpleados();

                    drop_cliente.SelectedValue = "";
                    drop_cliente.Enabled = false;
                    drop_noEmpleado.Enabled = true;
                }

                drop_noEmpleado.Text = "";
            }
            else
            {
                drop_cliente.Text = "";
                drop_noEmpleado.Text = "";

                drop_cliente.Enabled = false;
                drop_noEmpleado.Enabled = false;
            }
        }

        protected void drop_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_cliente.SelectedValue != "")
            {
                load_noEmpleados();

                drop_noEmpleado.Enabled = true;
            }
            else
            {
                drop_noEmpleado.Enabled = false;
            }

            drop_noEmpleado.SelectedValue = "";
        }

        protected void drop_noEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            BindGrid();
        }

        protected void grid_entradas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grid_entradas.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        
        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the run time error "  
            //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
        }
        
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Reporte-Reloj-" + DateTime.Now.ToString("M/d/yyyy") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grid_entradas.AllowPaging = false;
                this.BindGrid();

                grid_entradas.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grid_entradas.HeaderRow.Cells)
                {
                    cell.BackColor = grid_entradas.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grid_entradas.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grid_entradas.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grid_entradas.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grid_entradas.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }   
    }
}