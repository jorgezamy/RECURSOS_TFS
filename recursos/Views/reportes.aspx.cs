using MsBarco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;
using recursos.Content;

namespace recursos.Views
{
    public partial class reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }


        protected void tab1_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 0;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "clicked";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";


            lbl_cantidad.Text = "";
        }

        protected void tab2_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 1;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "clicked";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";

        }

        protected void tab3_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 2;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;
            btn_buscar_Click(sender, e);


            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "clicked";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";

        }

        protected void tab4_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 3;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;
            drop_fecha_aniversario.SelectedValue = "all";
            btn_buscar_Click(sender, e);


            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "clicked";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";

        }

        protected void tab5_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 4;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;
            drop_down_cumpleaños.SelectedValue = "all";
            btn_buscar_Click(sender, e);

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "clicked";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";

        }

        protected void tab6_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 5;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "clicked";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";

        }

        void loadReporte(string op,string fecha_inicio, string fecha_fin, string tipo, string mes, string bono, string tipoEmpleado)
        {
            var sp_reporte = DbUtil.GetCursor("sp_recursos_reportes_loadDatos",
                new SqlParameter("@opcion", op),
                new SqlParameter("@fecha_inicio", fecha_inicio),
                new SqlParameter("@fecha_fin", fecha_fin),
                new SqlParameter("@tipo", !string.IsNullOrEmpty(tipo) ? tipo : (object)DBNull.Value),
                new SqlParameter("@mes", !string.IsNullOrEmpty(mes) ? mes : (object)DBNull.Value),
                new SqlParameter("@bono", !string.IsNullOrEmpty(bono) ? bono : (object)DBNull.Value),
                new SqlParameter("@tipoEmpleado", !string.IsNullOrEmpty(tipoEmpleado) ? tipoEmpleado : (object)DBNull.Value));

            gridview_reporte.DataSource = sp_reporte;
            gridview_reporte.DataBind();

        }

        void loadBuscar(string op, string busqueda)
        {
            var sp_reporte = DbUtil.GetCursor("sp_recursos_reportes_busqueda_LoadDatos",
            new SqlParameter("@opcion", op),
            new SqlParameter("@busqueda", busqueda));

            gridview_reporte.DataSource = sp_reporte;
            gridview_reporte.DataBind();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {



            gridview_reporte.Visible = true;

            BindGrid();

        }

        protected void gridview_reporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview_reporte.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        void BindGrid()
        {
            lb_alerta.Text = "";
            if (multi_view.ActiveViewIndex == 0)
            {
                if (tb_busqueda_alta.Text != "" && tb_fecha_inicio_altas.Text!="" && tb_fecha_fin_altas.Text !="")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (tb_busqueda_alta.Text == "")
                        loadReporte("1", tb_fecha_inicio_altas.Text, tb_fecha_fin_altas.Text, null, null, null, null);
                    else
                        loadBuscar("1", tb_busqueda_alta.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 1)
            {
                if (tb_busqueda_baja.Text != "" && tb_fecha_inicio_bajas.Text != "" && tb_fecha_fin_bajas.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (tb_busqueda_baja.Text == "")
                        loadReporte("2", tb_fecha_inicio_bajas.Text, tb_fecha_fin_bajas.Text, null, null, null, null);
                    else
                        loadBuscar("2", tb_busqueda_baja.Text);
                }

            }

            if (multi_view.ActiveViewIndex == 6)
            {
                if (tb_busqueda_cambios.Text != "" && tb_fecha_inicio_cambios.Text != "" && tb_fecha_fin_cambios.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (tb_busqueda_cambios.Text == "")
                        loadReporte("7", tb_fecha_inicio_cambios.Text, tb_fecha_fin_cambios.Text, null, null, null, null);
                    else
                        loadBuscar("7", tb_busqueda_cambios.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 2)
            {
                if (tb_busqueda_activo.Text == "")
                    loadReporte("3", "", "", null, null, null, null);
                else
                    loadBuscar("3", tb_busqueda_activo.Text);
            }

            if (multi_view.ActiveViewIndex == 3)
                loadReporte("4", "", "", null, drop_fecha_aniversario.SelectedValue, null, null);
            if (multi_view.ActiveViewIndex == 4)
                loadReporte("5", "", "", null, drop_down_cumpleaños.SelectedValue, null, null);

            if (multi_view.ActiveViewIndex == 5)
            {
                if (drop_documento.SelectedValue != "" && tb_busqueda_vencimiento.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (tb_busqueda_vencimiento.Text == "")
                        loadReporte("6", "", "", drop_documento.SelectedValue, null, null, null);
                    else
                        loadBuscar("6", tb_busqueda_vencimiento.Text);
                }

            }

            if (multi_view.ActiveViewIndex == 7)
            {
                if (txtBuscarIncapacidades.Text != "" && txtInicioIncapacidades.Text != "" && txtFinIncapacidades.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (txtBuscarIncapacidades.Text == "")
                        loadReporte("8", txtInicioIncapacidades.Text, txtFinIncapacidades.Text, null, null, null, null);
                    else
                        loadBuscar("8", txtBuscarIncapacidades.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 8)
            {
                if (txtBuscarSuspensiones.Text != "" && txtInicioSuspensiones.Text != "" && txtFinSuspensiones.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (txtBuscarSuspensiones.Text == "")
                        loadReporte("9", txtInicioSuspensiones.Text, txtFinSuspensiones.Text, null, null, null, null);
                    else
                        loadBuscar("9", txtBuscarSuspensiones.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 9)
            {
                if (txtBuscarVacaciones.Text != "" && txtInicioVacaciones.Text != "" && txtFinVacaciones.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (txtBuscarVacaciones.Text == "")
                        loadReporte("10", txtInicioVacaciones.Text, txtFinVacaciones.Text, null, null, null, null);
                    else
                        loadBuscar("10", txtBuscarVacaciones.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 10)
            {
                if (txtBuscarPermisos.Text != "" && txtInicioPermisos.Text != "" && txtFinPermisos.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (txtBuscarPermisos.Text == "")
                        loadReporte("11", txtInicioPermisos.Text, txtFinPermisos.Text, null, null, null, null);
                    else
                        loadBuscar("11", txtBuscarPermisos.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 11)
            {
                if (txtBuscarHrsExtra.Text != "" && txtInicioHrsExtra.Text != "" && txtFinHrsExtra.Text != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (txtBuscarHrsExtra.Text == "")
                        loadReporte("12", txtInicioHrsExtra.Text, txtFinHrsExtra.Text, null, null, null, null);
                    else
                        loadBuscar("12", txtBuscarHrsExtra.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 12)
            {
                if (txtBuscarExtras.Text != "" && ddlExtras.SelectedValue != "")
                {
                    lb_alerta.Text = "Seleccionar solo una opción de busqueda.";
                }
                else
                {
                    if (txtBuscarExtras.Text == "")
                        loadReporte("13", "", "", null, null, ddlExtras.SelectedValue, null);
                    else
                        loadBuscar("13", txtBuscarExtras.Text);
                }
            }

            if (multi_view.ActiveViewIndex == 13)
            {
                if (ddlFestivo.SelectedValue == "" && txtInicioFestivo.Text == "" && txtFinFestivo.Text == "")
                {
                    lb_alerta.Text = "Complete todos los campos.";
                }
                else
                {
                    loadReporte("14", txtInicioFestivo.Text, txtFinFestivo.Text, null, null, null, ddlFestivo.SelectedValue);
                }
            }

            if (multi_view.ActiveViewIndex == 14)
            {
                // Get the current number checked.
                int totalcount = cheboxlist_avanzado.Items.Cast<System.Web.UI.WebControls.ListItem>().Where(item => item.Selected).Count();
                if (totalcount == 0)
                {
                    lb_alerta.Text = "Seleccione un campo de busqueda";
                }
                else
                {
                    int opcion = 0;
                    if (checkbox_activos.Checked)
                        opcion = 1;
                    loadReporte("15", "", "", tb_busqueda_avanzada.Text, null, null, opcion.ToString());
                }
            }

        }

        protected void gridview_reporte_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (multi_view.ActiveViewIndex == 14)
            //{
            //    int[] index = selectedIndexesOfCheckBoxList(cheboxlist_avanzado);

            //    for (int i = 0; i < index.Length; i++)
            //    {
            //        //respuesta += index[i];
            //        if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
            //        {
            //            e.Row.Controls[index[i]].Visible = false;
            //        }
            //    }
            //}
            if (multi_view.ActiveViewIndex == 14)
            {
                if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
                {
                    foreach (System.Web.UI.WebControls.ListItem item in cheboxlist_avanzado.Items)
                    {
                        if (!(item.Selected))
                        {
                            Console.WriteLine(item.Value);
                            e.Row.Controls[Int32.Parse(item.Value)].Visible = false;


                            //if (!(sender is GridView))
                            //    return;

                            //GridView gridView = (GridView)sender;
                            //var colCount = gridView.Columns.Count;

                            //for (int i = 0; i < gridview_reporte.Columns.Count; i++)
                            //{
                            //    //String header = gridview_reporte.Columns[i].HeaderText;
                            //    if (gridview_reporte.Columns[i].HeaderText == item.Text)
                            //        gridview_reporte.Columns[i].Visible = false;
                            //}


                        }
                    }
                }
            }
               


            int rowCount = gridview_reporte.Rows.Count;

            if (rowCount == 0)
            {
                btn_descargar.Visible = false;
            }
            else
            {
                btn_descargar.Visible = true;
            }
        }




        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gridview_reporte.AllowPaging = false;
                this.BindGrid();

                gridview_reporte.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gridview_reporte.HeaderRow.Cells)
                {
                    cell.BackColor = gridview_reporte.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gridview_reporte.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gridview_reporte.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gridview_reporte.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gridview_reporte.RenderControl(hw);

                //File.WriteAllText("/GCDM/fortiaReportes/Reporte" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", sw.ToString());
                //uploadFile.sftp.UploadSFTPFile("/GCDM/fortiaReportes/Reporte" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");


                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();



            }
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void gridview_reporte_DataBound(object sender, EventArgs e)
        {
            lbl_cantidad.Text = "Cantidad: " + (gridview_reporte.DataSource as DataTable).Rows.Count;


            //if (multi_view.ActiveViewIndex == 14)
            //{
            //    foreach (System.Web.UI.WebControls.ListItem item in cheboxlist_avanzado.Items)
            //    {
            //        if (item.Selected)
            //        {
            //            if (!(sender is GridView))
            //                return;

            //            GridView gridView = (GridView)sender;
            //            var colCount = gridView.Columns.Count;

            //            for (int i = 0; i < gridview_reporte.Columns.Count; i++)
            //            {
            //                //String header = gridview_reporte.Columns[i].HeaderText;
            //                if (gridview_reporte.Columns[i].HeaderText == item.Text)
            //                    gridview_reporte.Columns[i].Visible = false;
            //            }


            //            ////string selectedValue = item.Value;
            //            //for (int i = 0; i < colCount; i++)
            //            //{
            //            //    if (gridview_reporte.Columns[i].HeaderText == item.Text)
            //            //    {
            //            //        gridview_reporte.Columns[i].Visible = false;
            //            //    }
            //            //}
            //        }
            //    }
            //}
        }

        protected void tab7_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 6;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "clicked";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void tab8_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 7;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "clicked";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void tab9_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 8;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "clicked";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void tab10_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 9;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "clicked";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void tab11_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 10;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "clicked";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void tab12_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 11;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "clicked";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void tab13_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 12;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "clicked";
            tab14.CssClass = "initial";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void tab14_Click(object sender, EventArgs e)
        {
            multi_view.ActiveViewIndex = 13;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;

            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "clicked";
            tab15.CssClass = "initial";

            lbl_cantidad.Text = "";
        }

        protected void chkDiaFestivo_CheckedChanged(object sender, EventArgs e)
        {
            if (txtInicioFestivo.Text != "")
            {
                if (chkDiaFestivo.Checked == true)
                {
                    txtFinFestivo.Text = txtInicioFestivo.Text;
                    txtFinFestivo.Enabled = false;
                }

                else
                {
                    txtFinFestivo.Enabled = true;
                }
            }

            else
            {
                chkDiaFestivo.Checked = false;
            }
        }

        protected void txtInicioFestivo_TextChanged(object sender, EventArgs e)
        {
            if (chkDiaFestivo.Checked == true)
            {
                txtFinFestivo.Text = txtInicioFestivo.Text;
                txtFinFestivo.Enabled = false;
            }

            else
            {
                txtFinFestivo.Enabled = true;
            }
        }

        List<int> personalizado = new List<int>();

        public int[] selectedIndexesOfCheckBoxList(CheckBoxList chkList)
        {
            List<int> selectedIndexes = new List<int>();

            foreach (System.Web.UI.WebControls.ListItem item in chkList.Items)
            {
                if (item.Selected == false)
                {
                    selectedIndexes.Add(chkList.Items.IndexOf(item));
                }
            }

            return selectedIndexes.ToArray();
        }

        protected void tab15_Click(object sender, EventArgs e)
        {


        


            multi_view.ActiveViewIndex = 14;
            div_reporte.Visible = true;
            gridview_reporte.Visible = false;
            btn_descargar.Visible = false;
            //btn_buscar_Click(sender, e);


            tab1.CssClass = "initial";
            tab2.CssClass = "initial";
            tab3.CssClass = "initial";
            tab4.CssClass = "initial";
            tab5.CssClass = "initial";
            tab6.CssClass = "initial";
            tab7.CssClass = "initial";
            tab8.CssClass = "initial";
            tab9.CssClass = "initial";
            tab10.CssClass = "initial";
            tab11.CssClass = "initial";
            tab12.CssClass = "initial";
            tab13.CssClass = "initial";
            tab14.CssClass = "initial";

            tab15.CssClass = "clicked";

        }
    }
}