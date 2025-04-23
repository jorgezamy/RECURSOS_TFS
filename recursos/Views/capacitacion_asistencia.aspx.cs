using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;

using System.Web.UI;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace recursos.Views
{
    public partial class capacitacion_asistencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Add("nombreUsuario", "Mayela");
            //Session.Add("numeroUsuario", "2173");
            //Session.Add("usuario", "mespinoza");

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();
                    load_dropdown_cursos();

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[4] { new DataColumn("No.Empleado"), new DataColumn("Nombre"), new DataColumn("Departamento"), new DataColumn("Puesto") });
                    ViewState["empleados"] = dt;
                    this.BindGrid();
                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }
            }
        }

        protected void BindGrid()
        {
            datagridview_asistencia.DataSource = (DataTable)ViewState["empleados"];
            datagridview_asistencia.DataBind();
        }

        protected void btn_cerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("capacitacion.aspx");
        }

        protected void load_dropdown_cursos()
        {
            var sp_DropPuesto1 = DbUtil.GetCursor("sp_recursos_capacitacion_asistencia_load_cursos"
                //new SqlParameter("@salario", "0"),
                //new SqlParameter("@Drop_valueDepto", ""),
                //new SqlParameter("@Drop_valuePuesto", "ChoferOPlanta1"),
                //new SqlParameter("@Drop_valueCliente", ""),
                //MsBarco.DbUtil.NewSqlParam("@stored_vacio", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                //MsBarco.DbUtil.NewSqlParam("@salarioCantidad", null, SqlDbType.VarChar, ParameterDirection.Output, 15)
            );

            dropdown_curso.Items.Clear();
            dropdown_curso.Items.Add(new System.Web.UI.WebControls.ListItem("-- Seleccionar --", ""));

            dropdown_curso.DataSource = sp_DropPuesto1;
            dropdown_curso.DataValueField = "id_curso";
            dropdown_curso.DataTextField = "descripcion";
            dropdown_curso.DataBind();
        }

        private void loadCheckboxlist_empleados()
        {
            var sp_mostrardato = DbUtil.ExecuteProc("sp_recursos_capacitacion_asistencia_load_empleados",
                new SqlParameter("@id_curso", dropdown_curso.SelectedValue),
                new SqlParameter("@id_compania", Session["companiaID"].ToString()),

                MsBarco.DbUtil.NewSqlParam("@tematica", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                MsBarco.DbUtil.NewSqlParam("@duracion", null, SqlDbType.VarChar, ParameterDirection.Output, 5),
                MsBarco.DbUtil.NewSqlParam("@objetivos", null, SqlDbType.VarChar, ParameterDirection.Output, 100),
                MsBarco.DbUtil.NewSqlParam("@modalidad", null, SqlDbType.VarChar, ParameterDirection.Output, 100)
            );

            lbl_duracion.Text = sp_mostrardato["@duracion"].ToString() + " hrs";
            lbl_tematica.Text = sp_mostrardato["@tematica"].ToString();
            lbl_modalidad.Text = sp_mostrardato["@modalidad"].ToString();
            lbl_objetivos.Text = sp_mostrardato["@objetivos"].ToString();

            chbox_empleados.Items.Clear();
            chbox_empleados.DataSource = null;

            var sp_loadColumnas = DbUtil.GetCursor("sp_recursos_capacitacion_asistencia_load_empleados",
                new SqlParameter("@id_curso", dropdown_curso.SelectedValue),
                new SqlParameter("@id_compania", Session["companiaID"].ToString()),
                MsBarco.DbUtil.NewSqlParam("@tematica", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                MsBarco.DbUtil.NewSqlParam("@duracion", null, SqlDbType.VarChar, ParameterDirection.Output, 5),
                MsBarco.DbUtil.NewSqlParam("@objetivos", null, SqlDbType.VarChar, ParameterDirection.Output, 100),
                MsBarco.DbUtil.NewSqlParam("@modalidad", null, SqlDbType.VarChar, ParameterDirection.Output, 100)
            );
            chbox_empleados.DataSource = sp_loadColumnas;
            chbox_empleados.DataTextField = "nombre";
            chbox_empleados.DataValueField = "no_empleado";
            chbox_empleados.DataBind();
        }

        protected void dropdown_curso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdown_curso.SelectedValue != "")
            {
                loadCheckboxlist_empleados();
                btnRegistrar_Asistencia.Visible = true;
            }
            else
            {
                btnRegistrar_Asistencia.Visible = false;
                chbox_empleados.Items.Clear();
                lbl_tematica.Text = "";
                lbl_duracion.Text = "";
                lbl_objetivos.Text = "";
                lbl_modalidad.Text = "";
            }
        }

        protected void btnRegistrar_Asistencia_Click(object sender, EventArgs e)
        {
            save_empleados();

            btnRegistrar.Visible = false;
            if (datagridview_asistencia.Rows.Count > 0)
            {
                btnRegistrar.Visible = true;
            }

            //create_pdf();

            modal_popup.Show();
        }

        protected void save_empleados()
        {
            datagridview_asistencia.DataSource = null;

            DataTable dtn = new DataTable();
            dtn.Columns.AddRange(new DataColumn[6] { new DataColumn("No.Empleado"), new DataColumn("Nombre"), new DataColumn("Departamento"), new DataColumn("Puesto"), new DataColumn("Firma"), new DataColumn("Observaciones") });
            ViewState["empleados"] = dtn;
            this.BindGrid();

            datagridview_asistencia.DataBind();
            //datagridview_asistencia.DataSource = null;


            foreach (System.Web.UI.WebControls.ListItem item in chbox_empleados.Items)
            {
                if (item.Selected)
                {
                    // Obtener departamento y puesto 
                    string nombre, departamento, puesto;

                    var sp_mostrardato = DbUtil.ExecuteProc("sp_recursos_capacitacion_asistencia_load_informacion",
                        new SqlParameter("@no_empleado", item.Value),
                        MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                        MsBarco.DbUtil.NewSqlParam("@departamento", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                        MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 100)
                    );

                    nombre = sp_mostrardato["@nombre"].ToString();
                    departamento = sp_mostrardato["@departamento"].ToString();
                    puesto = sp_mostrardato["@puesto"].ToString();

                    DataTable dt = (DataTable)ViewState["empleados"];
                    dt.Rows.Add(item.Value, nombre, departamento, puesto);
                    ViewState["empleados"] = dt;
                    this.BindGrid();
                }
            }
        }


        protected void datagridview_asistencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            datagridview_asistencia.PageIndex = e.NewPageIndex;
            BindGrid();
            modal_popup.Show();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            //btnRegistrar_Asistencia.Enabled = false;

            foreach (System.Web.UI.WebControls.ListItem item in chbox_empleados.Items)
            {
                if (item.Selected)
                {
                    guardar_asistencia(item.Value);
                }
            }

            btnRegistrar.Visible = false;
            btnClose.Visible = false;

            btnRegistrar_Asistencia.Visible = false;
            lbl_registro.Visible = true;
            btn_descargar.Visible = true;

            dropdown_curso.Enabled = false;

            chbox_empleados.Enabled = false;

            modal_popup.Hide();

        }

   

        protected void guardar_asistencia (String no_empleado)
        {
            var sp_recursos_empleado_add = DbUtil.ExecuteProc("sp_recursos_capacitacion_asistencia_guardar_asistencia",
             new SqlParameter("@id_curso", dropdown_curso.SelectedValue),
             new SqlParameter("@no_empleado", no_empleado),
             new SqlParameter("@usuario", Session["usuario"].ToString())
            );
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            modal_popup.Hide();
        }

        protected void create_pdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                     string fecha = DateTime.Now.ToString("d/MM/yyyy");

                    //To Export all pages
                    datagridview_asistencia.AllowPaging = false;
                    save_empleados();
                    //this.BindGrid();
                    datagridview_asistencia.ControlStyle.Font.Size = 14;

                    datagridview_asistencia.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                    Paragraph title = new Paragraph();
                    title.Alignment = Element.ALIGN_CENTER;
                    title.Font = FontFactory.GetFont("Arial", 26);
                    title.Add("\nLISTA DE ASISTENCIA CURSO/TALLER  \n\n\n Nombre del curso: " + dropdown_curso.SelectedItem.Text + "\n\nFecha: " + fecha + "    Duración: " + lbl_duracion.Text + "\n\n\n");



                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();


                    string imageURL = @"C:\inetpub\wwwroot\Sistemas\images\logo.png";
                    //string imageURL = @"‪‪C:\Users\GCDM\Desktop\IT\Logos\logo.png";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(300f, 280f);
                    //Give space before image
                    jpg.SpacingBefore = 10f;
                    
              
                    //Give some space after the image
                    //jpg.SpacingAfter = 1f;
                    jpg.Alignment = Element.ALIGN_LEFT;
                    //jpg.SetAbsolutePosition(72, 2300);
                    pdfDoc.Add(jpg);

                    pdfDoc.Add(title);


                    Font _standardFont = new iTextSharp.text.Font();
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                    Font titulo = new Font(bfTimes, 20, Font.BOLD, Color.BLACK);
                    Font subtitulo = new Font(bfTimes, 18, Font.NORMAL, Color.BLACK);

                    // Creamos una tabla que contendrá el nombre, apellido y país
                    // de nuestros visitante.
                    PdfPTable tblPrueba = new PdfPTable(5);
                    tblPrueba.WidthPercentage = 100;

                    float[] anchoDeColumnas = new float[] { 6f, 25f, 10f, 10f, 15f };
                    tblPrueba.SetTotalWidth(anchoDeColumnas);

                    Single tit = 60f;
                    Single sub = 55f;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clno = new PdfPCell(new Phrase("# Empleado", titulo));
                    clno.BorderWidth = 1;
                    //clno.BorderWidthBottom = 1.75f;
                    //clno.MinimumHeight = 20f;
                    clno.FixedHeight = tit;
                    clno.HorizontalAlignment = Element.ALIGN_CENTER;


                    PdfPCell clnombre = new PdfPCell(new Phrase("Nombre", titulo));
                    clnombre.BorderWidth = 1;
                    clnombre.FixedHeight = tit;
                    clnombre.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell cldepartamento = new PdfPCell(new Phrase("Departamento", titulo));
                    cldepartamento.BorderWidth = 1;
                    cldepartamento.FixedHeight = tit;
                    cldepartamento.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell clfirma = new PdfPCell(new Phrase("Firma", titulo));
                    clfirma.BorderWidth = 1;
                    clfirma.FixedHeight = tit;
                    clfirma.HorizontalAlignment = Element.ALIGN_CENTER;



                    PdfPCell clobs = new PdfPCell(new Phrase("Observaciones", titulo));
                    clobs.BorderWidth = 1;
                    clobs.FixedHeight = tit;
                    clobs.HorizontalAlignment = Element.ALIGN_CENTER;


                    // Añadimos las celdas a la tabla
                    tblPrueba.AddCell(clno);
                    tblPrueba.AddCell(clnombre);
                    tblPrueba.AddCell(cldepartamento);

                    tblPrueba.AddCell(clfirma);
                    tblPrueba.AddCell(clobs);

                    // Llenamos la tabla con información

                    foreach (GridViewRow row in datagridview_asistencia.Rows)
                    {
                        clno = new PdfPCell(new Phrase(row.Cells[0].Text, subtitulo));
                        clno.HorizontalAlignment = Element.ALIGN_CENTER;
                        clno.BorderWidth = 1;
                        clno.FixedHeight = sub;


                        clnombre = new PdfPCell(new Phrase(Server.HtmlDecode(row.Cells[1].Text), subtitulo));
                        clnombre.HorizontalAlignment = Element.ALIGN_CENTER;
                        clnombre.BorderWidth = 1;
                        clnombre.FixedHeight = sub;


                        cldepartamento = new PdfPCell(new Phrase(Server.HtmlDecode(row.Cells[3].Text), subtitulo));
                        cldepartamento.HorizontalAlignment = Element.ALIGN_CENTER;
                        cldepartamento.BorderWidth = 1;
                        cldepartamento.FixedHeight = sub;

                        clfirma = new PdfPCell(new Phrase("", subtitulo));
                        clfirma.HorizontalAlignment = Element.ALIGN_CENTER;
                        clfirma.BorderWidth = 1;
                        clfirma.FixedHeight = sub;


                        clobs = new PdfPCell(new Phrase("", subtitulo));
                        clobs.HorizontalAlignment = Element.ALIGN_CENTER;
                        clobs.BorderWidth = 1;
                        clobs.FixedHeight = sub;



                        //clno = new PdfPCell(new Phrase("Roberto", _standardFont));
                        //clno.BorderWidth = 0;

                        //clnombre = new PdfPCell(new Phrase("Torres", _standardFont));
                        //clnombre.BorderWidth = 0;

                        //clfirma = new PdfPCell(new Phrase("Puerto Rico", _standardFont));
                        //clfirma.BorderWidth = 0;

                        //clobs = new PdfPCell(new Phrase("Puerto Rico", _standardFont));
                        //clobs.BorderWidth = 0;

                        // Añadimos las celdas a la tabla
                        tblPrueba.AddCell(clno);
                        tblPrueba.AddCell(clnombre);
                        tblPrueba.AddCell(cldepartamento);

                        tblPrueba.AddCell(clfirma);
                        tblPrueba.AddCell(clobs);
                    }

                    pdfDoc.Add(tblPrueba);




                    Chunk c1 = new Chunk("\n");
                    pdfDoc.Add(c1);


                    //htmlparser.Parse(sr);

                    PdfPTable tblComentarios = new PdfPTable(2);
                    tblComentarios.WidthPercentage = 100;

                    float[] anchos = new float[] { 5f, 25f};
                    tblComentarios.SetTotalWidth(anchos);

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clnotas = new PdfPCell(new Phrase("NOTAS:", titulo));
                    clnotas.BorderWidth = 0;
                    //clno.BorderWidthBottom = 1.75f;
                    //clno.MinimumHeight = 20f;
                    clnotas.FixedHeight = tit;
                    clnotas.HorizontalAlignment = Element.ALIGN_CENTER;


                    PdfPCell clnotas_descripcion = new PdfPCell(new Phrase("", titulo));
                    clnotas_descripcion.BorderWidth = 1;
                    clnotas_descripcion.FixedHeight = tit;
                    clnotas_descripcion.HorizontalAlignment = Element.ALIGN_CENTER;

                    tblComentarios.AddCell(clnotas);
                    tblComentarios.AddCell(clnotas_descripcion);

                    pdfDoc.Add(tblComentarios);


                    //Paragraph notas = new Paragraph();
                    //notas.Alignment = Element.ALIGN_LEFT;
                    //notas.Font = FontFactory.GetFont("Arial", 18);
                    //notas.Add("\n\nNOTAS:  \n\n\n ");
                    //pdfDoc.Add(notas);

                    //Paragraph instructor = new Paragraph();
                    //instructor.Alignment = Element.ALIGN_CENTER;
                    //instructor.Font = FontFactory.GetFont("Arial", 18);
                    //instructor.Add("\n\n YO INSTRUCTOR ___________________________________ CERTIFICO QUE LAS PERSONAS MENCIONADAS Y QUE FIRMAN \n\n ");
                    //pdfDoc.Add(instructor);

                    Paragraph linea1 = new Paragraph();
                    linea1.Alignment = Element.ALIGN_CENTER;
                    linea1.Font = FontFactory.GetFont("Arial", 18);
                    linea1.Add("\n\nFIRMA INSTRUCTOR");
                    pdfDoc.Add(linea1);

                    Paragraph linea2 = new Paragraph();
                    linea2.Alignment = Element.ALIGN_CENTER;
                    linea2.Font = FontFactory.GetFont("Arial", 18);
                    linea2.Add("\n______________________________");
                    pdfDoc.Add(linea2);

                    Paragraph linea3 = new Paragraph();
                    linea3.Alignment = Element.ALIGN_CENTER;
                    linea3.Font = FontFactory.GetFont("Arial", 18);
                    linea3.Add("\n" + Session["nombreUsuario"].ToString());
                    pdfDoc.Add(linea3);

                    Paragraph transportadora = new Paragraph();
                    transportadora.Alignment = Element.ALIGN_CENTER;
                    transportadora.Font = FontFactory.GetFont("Arial", 18,1);
                    transportadora.Add("\n\nTRANSPORTADORA NORTE\nDE CHIHUAHUA S.A. DE C.V.");
                    pdfDoc.Add(transportadora);

                    Paragraph direccion = new Paragraph();
                    direccion.Alignment = Element.ALIGN_CENTER;
                    direccion.Font = FontFactory.GetFont("Arial", 18, 1);
                    direccion.Add("\nCopenhague #4210,\nFracc. Uranga Unzueta C.P.32310");
                    pdfDoc.Add(direccion);

                    Paragraph rfc = new Paragraph();
                    rfc.Alignment = Element.ALIGN_CENTER;
                    rfc.Font = FontFactory.GetFont("Arial", 18, 1);
                    rfc.Add("\nRFC: TNC970625PCA");
                    pdfDoc.Add(rfc);

                    Paragraph telefono = new Paragraph();
                    telefono.Alignment = Element.ALIGN_CENTER;
                    telefono.Font = FontFactory.GetFont("Arial", 18, 1);
                    telefono.Add("\nTel. 613-15-42");
                    pdfDoc.Add(telefono);

                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Reporte_Asistencia.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    //Response.End();

                }
            }
        }

        protected void btn_cerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("capacitacion.aspx");
        }

        protected void btn_descargar_Click(object sender, EventArgs e)
        {
            create_pdf();
        }

        protected void datagridview_asistencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (btn_descargar.Visible == false)
            {
                if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Controls[4].Visible = false;
                    e.Row.Controls[5].Visible = false;
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Controls[4].Visible = true;
                    e.Row.Controls[5].Visible = true;
                }
            }
        }
    }
}