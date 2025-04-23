using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using MsBarco;
using TemplateEngine.Docx;
using System.Diagnostics;
using System.IO;
using NetOffice.WordApi;
using System.Globalization;

namespace recursos.Views
{
    public partial class plantillas : System.Web.UI.Page
    {
        Panel pnlDropDownList;
        protected void Page_PreInit(object sender, EventArgs e)
        {
                //ddlDynamic();
        }
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

        protected void ddlDynamic()
        {
            //Get ContentPlaceHolder
            ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");


            Literal lt;
            Label lb;

            //Dynamic DropDownList Panel
            pnlDropDownList = new Panel();
            pnlDropDownList.ID = "pnlDropDownList";
            pnlDropDownList.BorderWidth = 1;
            pnlDropDownList.Width = 300;
            pnlDropDownList.CssClass = "tabla_registrar";
            content.Controls.Add(pnlDropDownList);
            lt = new Literal();
            lt.Text = "<br />";
            content.Controls.Add(lt);
            lb = new Label();
            lb.Text = "Dynamic DropDownList<br />";
            pnlDropDownList.Controls.Add(lb);

            //Button To add DropDownlist
            Button btnAddDdl = new Button();
            btnAddDdl.ID = "btnAddDdl";
            btnAddDdl.Text = "Agregar empleado";
            //btnAddDdl.CssClass = "btn_guardarCancelar";
            btnAddDdl.Click += new System.EventHandler(btnAdd_Click);
            //btnAddDdl.CssClass = "tabla_registrar";
            content.Controls.Add(btnAddDdl);

            if (IsPostBack)
            {
                RecreateControls("ddlDynamic", "DropDownList");
            }

            //Dummy Button To do PostBack
            Button btnSubmit = new Button();
            btnSubmit.ID = "btnSubmit";
            btnSubmit.Text = "Submit";
            btnSubmit.Click += new System.EventHandler(btnSubmit_Click);
            btnSubmit.CssClass = "tabla_registrar";
            content.Controls.Add(btnSubmit);

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string plantilla = @"C:\GCDM\RH\cartas\Baja gafete unico\carta baja gafete unico.docx";
            //string carta = @"C:\GCDM\RH\cartas\Baja gafete unico\" + ddlEmpleado.SelectedValue + ".docx";
            string carta = @"C:\GCDM\RH\cartas\Baja gafete unico\carta baja gafete unico2.docx";

            File.Delete(carta);
            File.Copy(plantilla, carta);

            List<String> list = new List<String>();
            int i = 0;

            foreach (Control p in pnlDropDownList.Controls)
            //for (int i = 0; i < pnlDropDownList.Controls.Count; i++)
            {
                if (p is DropDownList)
                {
                    //{
                    i += 1;
                    DropDownList ddlPCountry = (DropDownList)pnlDropDownList.FindControl("ddlDynamic-" + i);
                    //Response.Write(ddlPCountry.SelectedValue + ",");
                    Response.Write(ddlPCountry.ID + ",");
                    //Response.Write(i);
                    //list.Add(ddlPCountry.SelectedValue + ",");

                    //}
                }


            }

            var sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                    new SqlParameter("@opcion", ddlTramite.SelectedValue),
                    new SqlParameter("@no_empleado", ""),//ddlEmpleado.SelectedValue),
                    new SqlParameter("@empleados_baja", "3035,"),
                    MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                    MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 1000),
                    MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                    MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                    MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                    MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                    MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                    MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                    MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                    MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                    MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                    MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                    );

            string lista = sp_loadEmpleados["@nombre"].ToString();
            string[] nombres = lista.Split(',');

            ListContent listContent = new ListContent("lista");
            foreach (string nombre in nombres)
            {
                List<IContentItem> items = new List<IContentItem>();
                items.Add(new FieldContent("nombre", nombre));
                listContent.AddItem(items.ToArray());
            }

            var valuesToFill = new TemplateEngine.Docx.Content(
               // Add list.
               listContent
            );


            using (var outputDocument = new TemplateProcessor(carta).SetRemoveContentControls(true))
            {
                outputDocument.FillContent(valuesToFill);
                outputDocument.SaveChanges();
                //Process.Start(carta);

                //System.Diagnostics.Process.Start(carta);
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            btn_guardar.Visible = true;
            Button btn = (Button)sender;

            if (btn.ID == "btnAddDdl")
            {
                int cnt = FindOccurence("ddlDynamic");
                DropDownList ddl = new DropDownList();
                ddl.ID = "ddlDynamic-" + Convert.ToString(cnt + 1);
                var sp_loadEmpleados = DbUtil.GetCursor("sp_recursos_cartas_loadEmpleados",
                        new SqlParameter("@opcion", "0")
                        );

                ddl.Items.Clear();
                ddl.Items.Add(new System.Web.UI.WebControls.ListItem("-- Seleccionar --", ""));
                ddl.DataSource = sp_loadEmpleados;
                ddl.DataValueField = "no_empleado";
                ddl.DataTextField = "nombre";
                ddl.DataBind();
                ddl.Attributes.Add("runat", "server");
                pnlDropDownList.Controls.Add(ddl);

                Literal lt = new Literal();
                lt.Text = "<br />";
                pnlDropDownList.Controls.Add(lt);
            }
        }

        private int FindOccurence(string substr)
        {
            string reqstr = Request.Form.ToString();
            return ((reqstr.Length - reqstr.Replace(substr, "").Length) / substr.Length);
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void ddlTramite_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {

                if (ddlTramite.SelectedValue != "")
                {
                    LoadDDLEmpleados();
                    rowEmpleado.Visible = true;

                    //if (ddlTramite.SelectedValue == "2")
                    //{
                    //    rowEmpleado.Visible = false;
                    //}

                    if (ddlTramite.SelectedValue == "14")
                    {
                        lblEmpleado.Text = "Chofer escuelita: ";
                    }

                    if (ddlTramite.SelectedValue == "14")
                    {
                        rowFecha.Visible = true;
                    }

                    else
                    {
                        lblEmpleado.Text = "Empleado: ";
                        rowCapacitador.Visible = false;
                    }

                    if (ddlEmpleado.SelectedValue != "")
                    {
                        btn_guardar.Visible = true;
                    }

                    else
                    {
                        btn_guardar.Visible = false;
                    }
                }

                else
                {
                    rowEmpleado.Visible = false;
                    rowCapacitador.Visible = false;
                }
            }

            catch { }
        }

        private void RecreateControls(string ctrlPrefix, string ctrlType)
        {
            string[] ctrls = Request.Form.ToString().Split('&');
            int cnt = FindOccurence(ctrlPrefix);
            if (cnt > 0)
            {
                Literal lt;
                for (int k = 1; k <= cnt; k++)
                {
                    for (int i = 0; i < ctrls.Length; i++)
                    {
                        if (ctrls[i].Contains(ctrlPrefix + "-" + k.ToString()))
                        {
                            string ctrlName = ctrls[i].Split('=')[0];
                            string ctrlValue = ctrls[i].Split('=')[1];

                            //Decode the Value
                            ctrlValue = Server.UrlDecode(ctrlValue);

                            if (ctrlType == "DropDownList")
                            {
                                DropDownList ddl = new DropDownList();
                                ddl.ID = ctrlName;

                                //Rebind Data
                                var sp_loadEmpleados = DbUtil.GetCursor("sp_recursos_cartas_loadEmpleados",
                                        new SqlParameter("@opcion", "0")
                                        );

                                ddl.Items.Clear();
                                ddl.Items.Add(new System.Web.UI.WebControls.ListItem("-- Seleccionar --", ""));
                                ddl.DataSource = sp_loadEmpleados;
                                ddl.DataValueField = "no_empleado";
                                ddl.DataTextField = "nombre";
                                ddl.DataBind();

                                //Select the Preselected Item
                                ddl.Items.FindByValue(ctrlValue).Selected = true;
                                pnlDropDownList.Controls.Add(ddl);
                                lt = new Literal();
                                lt.Text = "<br />";
                                pnlDropDownList.Controls.Add(lt);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void LoadDDLEmpleados()
        {
            var sp_loadEmpleados = DbUtil.GetCursor("sp_recursos_cartas_loadEmpleados",
                    new SqlParameter("@opcion", ddlTramite.SelectedValue)
                    );

            ddlEmpleado.Items.Clear();
            ddlEmpleado.Items.Add(new System.Web.UI.WebControls.ListItem("-- Seleccionar --", ""));
            ddlEmpleado.DataSource = sp_loadEmpleados;
            ddlEmpleado.DataValueField = "no_empleado";
            ddlEmpleado.DataTextField = "nombre";
            ddlEmpleado.DataBind();
        }



        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpleado.SelectedValue != "")
            {
                btn_guardar.Visible = true;

                if (ddlTramite.SelectedValue == "14")
                {
                    rowCapacitador.Visible = true;

                    var sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    lblCapacitador.Text = sp_loadEmpleados["@chofer_capacitador"].ToString();
                }
            }

            else
            {
                btn_guardar.Visible = false;
                rowCapacitador.Visible = false;
            }
        }

        private void Download_File(string FilePath)
        {
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
            Response.WriteFile(FilePath);
            Response.End();
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            int opcion = Int32.Parse(ddlTramite.SelectedValue);

            switch (opcion)
            {
                case 1:
                    string plantilla = @"C:\GCDM\RH\cartas\Baja gafete aduana mexicana\BAJA GAFETE ADUANA MEXICANA.docx";
                    string carta = @"C:\GCDM\RH\cartas\Baja gafete aduana mexicana\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Delete(carta + ".pdf");

                    File.Copy(plantilla, carta + ".docx");

                    var sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)

                            );
                    var valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("gafete", sp_loadEmpleados["@IMSS"].ToString())

                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();

                    }

                    using (var word = new Application())
                    {
                        var document = word.Documents.Open(carta + ".docx");
                        document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                        document.Close(false);

                        word.Quit();
                    }

                    File.Delete(carta + ".docx");

                    Download_File(carta + ".pdf");


                    break;

                case 2:
                    plantilla = @"C:\GCDM\RH\cartas\Baja gafete unico\BAJA GAFETE UNICO.docx";
                    carta = @"C:\GCDM\RH\cartas\Baja gafete unico\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Delete(carta + ".pdf");

                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                             MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)

                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();

                    }

                    using (var word = new Application())
                    {
                        var document = word.Documents.Open(carta + ".docx");
                        document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                        document.Close(false);

                        word.Quit();
                    }

                    File.Delete(carta + ".docx");

                    Download_File(carta + ".pdf");

                    break;
                case 3:
                    plantilla = @"C:\GCDM\RH\cartas\Guardería\CARTA DE GUARDERIA.docx";
                    carta = @"C:\GCDM\RH\cartas\Guardería\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)

                   );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),

                        new FieldContent("entrada_semana", sp_loadEmpleados["@startDate"].ToString()),
                        new FieldContent("salida_semana", sp_loadEmpleados["@domicilio"].ToString()),
                        new FieldContent("entrada_sabado", sp_loadEmpleados["@contrato"].ToString()),
                        new FieldContent("salida_sabado", sp_loadEmpleados["@chofer_capacitador"].ToString()),
                        new FieldContent("salario", sp_loadEmpleados["@salario"].ToString())

                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }
                    Download_File(carta + ".docx");

                    break;
                case 4:
                    plantilla = @"C:\GCDM\RH\cartas\IMSS\CARTA IMSS.docx";
                    carta = @"C:\GCDM\RH\cartas\IMSS\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)

                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),


                        new FieldContent("entrada_semana", sp_loadEmpleados["@startDate"].ToString()),
                        new FieldContent("salida_semana", sp_loadEmpleados["@domicilio"].ToString()),
                        new FieldContent("entrada_sabado", sp_loadEmpleados["@contrato"].ToString()),
                        new FieldContent("salida_sabado", sp_loadEmpleados["@chofer_capacitador"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }
                    Download_File(carta + ".docx");


                    break;
                case 5:
                    plantilla = @"C:\GCDM\RH\cartas\BECA\CARTA PARA BECA.docx";
                    carta = @"C:\GCDM\RH\cartas\BECA\" + ddlEmpleado.SelectedValue;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),
                        new FieldContent("salario", sp_loadEmpleados["@salario"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        Download_File(carta + ".docx");
                        //System.Diagnostics.Process.Start(carta);
                    }

                    break;
                case 6:
                    plantilla = @"C:\GCDM\RH\cartas\Juzgado familiar\JUZGADO FAMILIAR.docx";
                    carta = @"C:\GCDM\RH\cartas\Juzgado familiar\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),
                        new FieldContent("salario", sp_loadEmpleados["@salario"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }

                    Download_File(carta + ".docx");

                    break;
                case 7:
                    plantilla = @"C:\GCDM\RH\cartas\FONACOT\CARTA FONACOT.docx";
                    carta = @"C:\GCDM\RH\cartas\FONACOT\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("salario", sp_loadEmpleados["@salario"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        Download_File(carta + ".docx");
                        //System.Diagnostics.Process.Start(carta);
                    }

                    break;
                case 8:
                    plantilla = @"C:\GCDM\RH\cartas\VISA\CARTA PARA VISA.docx";
                    carta = @"C:\GCDM\RH\cartas\VISA\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),
                        new FieldContent("salario", sp_loadEmpleados["@salario"].ToString())

                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }

                    Download_File(carta + ".docx");


                    break;
                case 9:
                    plantilla = @"C:\GCDM\RH\cartas\Préstamo personal\CARTA PRESTAMO PERSONAL.docx";
                    carta = @"C:\GCDM\RH\cartas\Préstamo personal\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),
                        new FieldContent("domicilio", sp_loadEmpleados["@domicilio"].ToString()),
                        new FieldContent("contrato", sp_loadEmpleados["@contrato"].ToString()),
                        new FieldContent("salario", sp_loadEmpleados["@salario"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }

                    Download_File(carta + ".docx");


                    break;
                case 10:
                    plantilla = @"C:\GCDM\RH\cartas\Trabajo activos\CARTA TRABAJO ACTIVOS.docx";
                    carta = @"C:\GCDM\RH\cartas\Trabajo activos\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Delete(carta + ".pdf");

                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }

                    using (var word = new Application())
                    {
                        var document = word.Documents.Open(carta + ".docx");
                        document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                        document.Close(false);
                        word.Quit();
                    }

                    File.Delete(carta + ".docx");

                    Download_File(carta + ".pdf");

                    break;
                case 11:
                    plantilla = @"C:\GCDM\RH\cartas\Trámite FAST\CARTA TRAMITE FAST.docx";
                    carta = @"C:\GCDM\RH\cartas\Trámite FAST\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Delete(carta + ".pdf");

                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }

                    using (var word = new Application())
                    {
                        var document = word.Documents.Open(carta + ".docx");
                        document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                        document.Close(false);
                        word.Quit();
                    }

                    File.Delete(carta + ".docx");

                    Download_File(carta + ".pdf");

                    break;
                case 12:
                    plantilla = @"C:\GCDM\RH\cartas\Trabajo exempleados\CARTA TRABAJO EX-EMPLEADOS.docx";
                    carta = @"C:\GCDM\RH\cartas\Trabajo exempleados\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Delete(carta + ".pdf");

                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("fecha_egreso", sp_loadEmpleados["@fecha_egreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }

                    using (var word = new Application())
                    {
                        var document = word.Documents.Open(carta + ".docx");
                        document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                        document.Close(false);
                        word.Quit();
                    }

                    File.Delete(carta + ".docx");

                    Download_File(carta + ".pdf");


                    break;
                case 13:
                    plantilla = @"C:\GCDM\RH\cartas\Exámen apto\CARTA PARA EXAMEN APTO.docx";
                    carta = @"C:\GCDM\RH\cartas\Exámen apto\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Delete(carta + ".pdf");

                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString())
                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                    }

                    using (var word = new Application())
                    {
                        var document = word.Documents.Open(carta + ".docx");
                        document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                        document.Close(false);
                        word.Quit();
                    }

                    File.Delete(carta + ".docx");

                    Download_File(carta + ".pdf");


                    break;
                case 14:
                    if (tb_fecha_solicitud.Text != "")
                    {                   
                        plantilla = @"C:\GCDM\RH\cartas\Procesar aduana\CARTA PROCESAR ADUANA.docx";
                        carta = @"C:\GCDM\RH\cartas\Procesar aduana\" + ddlEmpleado.SelectedValue ;

                        File.Delete(carta + ".docx");
                        File.Delete(carta + ".pdf");
                        File.Copy(plantilla, carta + ".docx");

                        System.Globalization.DateTimeFormatInfo mfi = new
                        System.Globalization.DateTimeFormatInfo();

                        DateTime result = DateTime.ParseExact(tb_fecha_solicitud.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                        string strMonthName = mfi.GetMonthName(result.Month).ToString();

                        string date = strMonthName + " " + result.Day + ", " + result.Year;


                        DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
                        string fecha = result.Day + " de " + dtinfo.GetMonthName(result.Month) + " del " + result.Year ;


                        sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                                new SqlParameter("@opcion", ddlTramite.SelectedValue),
                                new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                                MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                                MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                                MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                                MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                                MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                                MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                                MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                                 MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                                );

                        valuesToFill = new TemplateEngine.Docx.Content(
                            new FieldContent("fecha", fecha),
                            new FieldContent("date", date),
                            new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                            //new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                            new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                            new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),
                            new FieldContent("tratoIngles", sp_loadEmpleados["@tratoIngles"].ToString()),
                            new FieldContent("chofer_capacitador", sp_loadEmpleados["@chofer_capacitador"].ToString()),
                            new FieldContent("treatment", sp_loadEmpleados["@treatment"].ToString())
                            //new FieldContent("startDate", sp_loadEmpleados["@startDate"].ToString())
                            );

                        using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                        {
                            outputDocument.FillContent(valuesToFill);
                            outputDocument.SaveChanges();
                            //Process.Start(carta);
                            //System.Diagnostics.Process.Start(carta);
                        }

                        using (var word = new Application())
                        {
                            var document = word.Documents.Open(carta + ".docx");
                            document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                            document.Close(false);
                            word.Quit();
                        }

                        File.Delete(carta + ".docx");

                        Download_File(carta + ".pdf");

                        lbl_fecha.Text = "";


                    }
                    else
                    {
                        lbl_fecha.Text = "Seleccione una fecha";
                    }
                    break;
                case 15:
                    plantilla = @"C:\GCDM\RH\cartas\Permiso EU\CARTA PARA PERMISO EU.docx";
                    carta = @"C:\GCDM\RH\cartas\Permiso EU\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("IMSS", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("fecha_ingreso", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("puesto", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("trato", sp_loadEmpleados["@trato"].ToString()),
                        new FieldContent("salario", sp_loadEmpleados["@salario"].ToString())
                    );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();

                    }

                    Download_File(carta + ".docx");

                    break;

                case 16:
                    plantilla = @"C:\GCDM\RH\cartas\Alta gafete aduana mexicana\SOLICITUD GAFETE ADUANA.docx";
                    carta = @"C:\GCDM\RH\cartas\Alta gafete aduana mexicana\" + ddlEmpleado.SelectedValue ;

                    File.Delete(carta + ".docx");
                    File.Delete(carta + ".pdf");

                    File.Copy(plantilla, carta + ".docx");

                    sp_loadEmpleados = DbUtil.ExecuteProc("sp_recursos_cartas_loadDatos",
                            new SqlParameter("@opcion", ddlTramite.SelectedValue),
                            new SqlParameter("@no_empleado", ddlEmpleado.SelectedValue),
                            MsBarco.DbUtil.NewSqlParam("@fecha", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@nombre", null, SqlDbType.VarChar, ParameterDirection.Output, 45),
                            MsBarco.DbUtil.NewSqlParam("@IMSS", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@RFC", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_ingreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@fecha_egreso", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@puesto", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@trato", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@tratoIngles", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@domicilio", null, SqlDbType.VarChar, ParameterDirection.Output, 90),
                            MsBarco.DbUtil.NewSqlParam("@contrato", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@chofer_capacitador", null, SqlDbType.VarChar, ParameterDirection.Output, 50),
                            MsBarco.DbUtil.NewSqlParam("@treatment", null, SqlDbType.VarChar, ParameterDirection.Output, 15),
                            MsBarco.DbUtil.NewSqlParam("@startDate", null, SqlDbType.VarChar, ParameterDirection.Output, 30),
                            MsBarco.DbUtil.NewSqlParam("@salario", null, SqlDbType.VarChar, ParameterDirection.Output, 30)
                            );

                    valuesToFill = new TemplateEngine.Docx.Content(
                        new FieldContent("fecha", sp_loadEmpleados["@fecha"].ToString()),
                        new FieldContent("nombre", sp_loadEmpleados["@nombre"].ToString()),
                        new FieldContent("domicilio", sp_loadEmpleados["@domicilio"].ToString()),
                        new FieldContent("colonia", sp_loadEmpleados["@IMSS"].ToString()),
                        new FieldContent("RFC", sp_loadEmpleados["@RFC"].ToString()),
                        new FieldContent("vigencia", sp_loadEmpleados["@fecha_ingreso"].ToString()),
                        new FieldContent("no_licencia", sp_loadEmpleados["@fecha_egreso"].ToString()),
                        new FieldContent("telefono", sp_loadEmpleados["@puesto"].ToString()),
                        new FieldContent("fecha_expedicion", sp_loadEmpleados["@contrato"].ToString())

                        );

                    using (var outputDocument = new TemplateProcessor(carta + ".docx").SetRemoveContentControls(true))
                    {
                        outputDocument.FillContent(valuesToFill);
                        outputDocument.SaveChanges();
                        //Process.Start(carta);
                        //System.Diagnostics.Process.Start(carta);
                    }

                    using (var word = new Application())
                    {
                        var document = word.Documents.Open(carta + ".docx");
                        document.ExportAsFixedFormat(carta + ".pdf", NetOffice.WordApi.Enums.WdExportFormat.wdExportFormatPDF);
                        document.Close(false);
                        word.Quit();
                    }

                    File.Delete(carta + ".docx");

                    Download_File(carta + ".pdf");

                    break;
                default:
                    break;

            }
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cartas.aspx");
        }
    }
}