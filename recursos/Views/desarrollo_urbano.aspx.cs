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
    public partial class desarrollo_urbano : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["usuario"] as string))
                {
                    nombreUsuario.Text = Session["nombreUsuario"].ToString();
                    //SetInitialRow();


                }
                else
                {
                    Response.Redirect("http://18.219.12.59:81/Views/login.aspx");
                }

                

            }


            if ( MultiView.ActiveViewIndex == 0 && tb_Colonia.Text != "")
            {
                tb_Colonia.Focus();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func()", true);
            }
            if (MultiView.ActiveViewIndex == 1 && tb_calle.Text != "")
            {
                tb_calle.Focus();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func_Calle()", true);
            }
            if (MultiView.ActiveViewIndex == 2 && tb_codigo_postal.Text != "")
            {
                tb_codigo_postal.Focus();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Func_Codigo()", true);
            }
        }

        protected void drop_pais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_pais.SelectedValue != "")
            {
                drop_estado.Enabled = true;
                drop_estado.Items.Clear();

                var sp_loadEstados = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_pais.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "1")
                    );

                drop_estado.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_estado.DataSource = sp_loadEstados;
                drop_estado.DataValueField = "idEstado";
                drop_estado.DataTextField = "descripcion";
                drop_estado.DataBind();
            }
            else
            {
                drop_estado.SelectedValue = "";
                drop_estado.Enabled = false;
            }
        }

        protected void drop_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_estado.SelectedValue != "")
            {
                drop_ciudad.Enabled = true;
                drop_ciudad.Items.Clear();

                var sp_loadCiudades = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_estado.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "2")
                    );

                drop_ciudad.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_ciudad.DataSource = sp_loadCiudades;
                drop_ciudad.DataValueField = "idCiudad";
                drop_ciudad.DataTextField = "descripcion";
                drop_ciudad.DataBind();
            }
            else
            {
                drop_ciudad.SelectedValue = "";
                drop_ciudad.Enabled = false;
            }
        }

        protected void drop_ciudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_ciudad.SelectedValue != "")
            {
                drop_codigoPostal.Enabled = true;
                drop_codigoPostal.Items.Clear();

                var sp_loadCodigoPostal = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_ciudad.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "3")
                    );

                drop_codigoPostal.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_codigoPostal.DataSource = sp_loadCodigoPostal;
                drop_codigoPostal.DataValueField = "codigoPostal";
                drop_codigoPostal.DataTextField = "descripcion";
                drop_codigoPostal.DataBind();
            }
            else
            {
                drop_codigoPostal.SelectedValue = "";
                drop_codigoPostal.Enabled = false;
            }
        }

        protected void drop_codigoPostal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_codigoPostal.SelectedValue != "")
            {
                drop_colonia.Enabled = true;
                drop_colonia.Items.Clear();

                var sp_loadColonias = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_codigoPostal.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "4")
                    );

                drop_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_colonia.DataSource = sp_loadColonias;
                drop_colonia.DataValueField = "idColonia";
                drop_colonia.DataTextField = "descripcion";
                drop_colonia.DataBind();
            }
            else
            {
                drop_colonia.SelectedValue = "";
                drop_colonia.Enabled = false;
            }
        }

        protected void drop_colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_colonia.SelectedValue != "")
            {
                drop_calles.Enabled = true;
                drop_calles.Items.Clear();

                var sp_loadCalles = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_ciudad.SelectedValue),
                    new SqlParameter("@idColonia", drop_colonia.SelectedValue),
                    new SqlParameter("@seccion", "5")
                    );

                drop_calles.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_calles.DataSource = sp_loadCalles;
                drop_calles.DataValueField = "idCalle";
                drop_calles.DataTextField = "descripcion";
                drop_calles.DataBind();
            }
            else
            {
                drop_calles.SelectedValue = "";
                drop_calles.Enabled = false;
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            if (drop_calles.SelectedValue != "")
            {
                var sp_updateCalles = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_updateCalles",
                    new SqlParameter("@idColonia", drop_colonia.SelectedValue),
                    new SqlParameter("@idCalle", drop_calles.SelectedValue)
                    );

                btn_guardar.Visible = true;
            }
            else
            {
                btn_guardar.Visible = false;
            }
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }

        protected void tab1_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 0;
            tab1.CssClass = "clicked_desarrollo";
            tab2.CssClass = "initial_desarrollo";
            tab3.CssClass = "initial_desarrollo";
            tab4.CssClass = "initial_desarrollo";

        }

        protected void tab2_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 1;
            tab1.CssClass = "initial_desarrollo";
            tab2.CssClass = "clicked_desarrollo";
            tab3.CssClass = "initial_desarrollo";
            tab4.CssClass = "initial_desarrollo";

        }

        protected void tab3_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 2;
            tab1.CssClass = "initial_desarrollo";
            tab2.CssClass = "initial_desarrollo";
            tab3.CssClass = "clicked_desarrollo";
            tab4.CssClass = "initial_desarrollo";

        }


        protected void tab4_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 3;
            tab1.CssClass = "initial_desarrollo";
            tab2.CssClass = "initial_desarrollo";
            tab3.CssClass = "initial_desarrollo";
            tab4.CssClass = "clicked_desarrollo";
        }


        protected void drop_pais_colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_pais_colonia.SelectedValue != "")
            {
                drop_estado_colonia.Enabled = true;
                drop_estado_colonia.Items.Clear();

                var sp_loadEstados = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_pais_colonia.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "1")
                    );

                drop_estado_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_estado_colonia.DataSource = sp_loadEstados;
                drop_estado_colonia.DataValueField = "idEstado";
                drop_estado_colonia.DataTextField = "descripcion";
                drop_estado_colonia.DataBind();

                drop_ciudad_colonia.SelectedValue = "";
                drop_ciudad_colonia.Enabled = false;

                drop_codigoPostal_colonia.SelectedValue = "";
                drop_codigoPostal_colonia.Enabled = false;

                tb_Colonia.Enabled = false;
            }
            else
            {
                drop_estado_colonia.SelectedValue = "";
                drop_estado_colonia.Enabled = false;

                drop_ciudad_colonia.SelectedValue = "";
                drop_ciudad_colonia.Enabled = false;

                drop_codigoPostal_colonia.SelectedValue = "";
                drop_codigoPostal_colonia.Enabled = false;

                tb_Colonia.Enabled = false;
            }
        }

        protected void drop_estado_colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_estado_colonia.SelectedValue != "")
            {
                drop_ciudad_colonia.Enabled = true;
                drop_ciudad_colonia.Items.Clear();

                var sp_loadCiudades = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_estado_colonia.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "2")
                    );

                drop_ciudad_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_ciudad_colonia.DataSource = sp_loadCiudades;
                drop_ciudad_colonia.DataValueField = "idCiudad";
                drop_ciudad_colonia.DataTextField = "descripcion";
                drop_ciudad_colonia.DataBind();

                drop_codigoPostal_colonia.SelectedValue = "";
                drop_codigoPostal_colonia.Enabled = false;

                tb_Colonia.Enabled = false;
            }
            else
            {
                drop_ciudad_colonia.SelectedValue = "";
                drop_ciudad_colonia.Enabled = false;

                drop_codigoPostal_colonia.SelectedValue = "";
                drop_codigoPostal_colonia.Enabled = false;

                tb_Colonia.Enabled = false;
            }
        }

        protected void drop_ciudad_colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_ciudad_colonia.SelectedValue != "")
            {
                drop_codigoPostal_colonia.Enabled = true;
                drop_codigoPostal_colonia.Items.Clear();

                var sp_loadCodigoPostal = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_ciudad_colonia.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "3")
                    );

                drop_codigoPostal_colonia.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_codigoPostal_colonia.DataSource = sp_loadCodigoPostal;
                drop_codigoPostal_colonia.DataValueField = "codigoPostal";
                drop_codigoPostal_colonia.DataTextField = "descripcion";
                drop_codigoPostal_colonia.DataBind();
            }
            else
            {
                drop_codigoPostal_colonia.SelectedValue = "";
                drop_codigoPostal_colonia.Enabled = false;

            }
        }

        protected void Load_Colonias()
        {
            var sp_loadColonias = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadColonias",
                    new SqlParameter("@id_ciudad", drop_ciudad_colonia.SelectedValue),
                    new SqlParameter("@codigo_postal", drop_codigoPostal_colonia.SelectedValue),
                    new SqlParameter("@Colonia", tb_Colonia.Text)
                    );

            gridview_colonias.DataSource = sp_loadColonias;
            gridview_colonias.DataBind();

            gridview_colonias.Visible = true;

            int rowCount = gridview_colonias.Rows.Count; // returns zero

            if (rowCount == 0)
            {
                btn_guardar_colonia.CssClass = "btn_guardarCancelar";
                btn_guardar.Enabled = true;
            }
            else
            {
                btn_guardar_colonia.CssClass = "btn_guardarCancelar_disabled";
                btn_guardar.Enabled = false;

            }

        }

        protected void Gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview_colonias.PageIndex = e.NewPageIndex;
            Load_Colonias();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gridview_colonias.DataSource = null;
            Load_Colonias();
            gridview_colonias_parecidas.DataSource = null;
            Load_Sugerencias_colonias();

        }

        protected void drop_codigoPostal_colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_codigoPostal_colonia.SelectedValue != "")
            {
                tb_Colonia.Enabled = true;
                Hidden_Valor.Value = "0";

            }
            else
            {
                tb_Colonia.Enabled = false;
            }
        }

        protected void btn_guardar_colonia_Click(object sender, EventArgs e)
        {
            if (tb_Colonia.Text != "")
            {
                modalpop_guardar_colonia.Show();       
            }
            else
            {
                lbl_alerta_colonia.Text = "Ingrese nombre de la colonia";
                lbl_alerta_colonia.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void drop_pais_calle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_pais_calle.SelectedValue != "")
            {
                drop_estado_calle.Enabled = true;
                drop_estado_calle.Items.Clear();

                var sp_loadEstados = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_pais_calle.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "1")
                    );

                drop_estado_calle.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_estado_calle.DataSource = sp_loadEstados;
                drop_estado_calle.DataValueField = "idEstado";
                drop_estado_calle.DataTextField = "descripcion";
                drop_estado_calle.DataBind();

                drop_ciudad_calle.SelectedValue = "";
                drop_ciudad_calle.Enabled = false;

                drop_codigo_postal_calle.SelectedValue = "";
                drop_codigo_postal_calle.Enabled = false;
            }
            else
            {
                drop_estado_calle.SelectedValue = "";
                drop_estado_calle.Enabled = false;

                drop_ciudad_calle.SelectedValue = "";
                drop_ciudad_calle.Enabled = false;

                drop_codigo_postal_calle.SelectedValue = "";
                drop_codigo_postal_calle.Enabled = false;

            }
        }

        protected void drop_estado_calle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_estado_calle.SelectedValue != "")
            {
                drop_ciudad_calle.Enabled = true;
                drop_ciudad_calle.Items.Clear();

                var sp_loadCiudades = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_estado_calle.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "2")
                    );

                drop_ciudad_calle.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_ciudad_calle.DataSource = sp_loadCiudades;
                drop_ciudad_calle.DataValueField = "idCiudad";
                drop_ciudad_calle.DataTextField = "descripcion";
                drop_ciudad_calle.DataBind();

                drop_codigo_postal_calle.SelectedValue = "";
                drop_codigo_postal_calle.Enabled = false;

            }
            else
            {
                drop_ciudad_calle.SelectedValue = "";
                drop_ciudad_calle.Enabled = false;

                drop_codigo_postal_calle.SelectedValue = "";
                drop_codigo_postal_calle.Enabled = false;

            }
        }

        protected void drop_ciudad_calle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_ciudad_calle.SelectedValue != "")
            {
                drop_codigo_postal_calle.Enabled = true;
                drop_codigo_postal_calle.Items.Clear();

                var sp_loadCodigoPostal = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_ciudad_calle.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "3")
                    );

                drop_codigo_postal_calle.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_codigo_postal_calle.DataSource = sp_loadCodigoPostal;
                drop_codigo_postal_calle.DataValueField = "codigoPostal";
                drop_codigo_postal_calle.DataTextField = "descripcion";
                drop_codigo_postal_calle.DataBind();
            }
            else
            {
                drop_codigo_postal_calle.SelectedValue = "";
                drop_codigo_postal_calle.Enabled = false;

            }
        }

        protected void drop_codigo_postal_calle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_codigo_postal_calle.SelectedValue != "")
            {
                drop_colonia_calle.Enabled = true;
                drop_colonia_calle.Items.Clear();

                var sp_loadCodigoPostal = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_codigo_postal_calle.SelectedItem.Text),
                    new SqlParameter("@idColonia", drop_ciudad_calle.SelectedValue),
                    new SqlParameter("@seccion", "6")
                    );

                drop_colonia_calle.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_colonia_calle.DataSource = sp_loadCodigoPostal;
                drop_colonia_calle.DataValueField = "idColonia";
                drop_colonia_calle.DataTextField = "descripcion";
                drop_colonia_calle.DataBind();
            }
            else
            {
                drop_colonia_calle.SelectedValue = "";
                drop_colonia_calle.Enabled = false;

            }
        }

        protected void drop_colonia_calle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_colonia_calle.SelectedValue != "")
            {
                tb_calle.Enabled = true;
                Hidden_Valor.Value = "1";

            }
            else
            {
                tb_calle.Enabled = false;
            }
        }

        protected void gridview_calle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview_calle.PageIndex = e.NewPageIndex;
            LoadCalles();
        }

        protected void LoadCalles()
        {
            var sp_loadColonias = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadCalles",
                    new SqlParameter("@id_colonia", drop_colonia_calle.SelectedValue),
                    new SqlParameter("@id_ciudad", drop_ciudad_calle.SelectedValue),
                    new SqlParameter("@calle", tb_calle.Text)
                    );

            gridview_calle.DataSource = sp_loadColonias;
            gridview_calle.DataBind();

            gridview_calle.Visible = true;

            int rowCount = gridview_calle.Rows.Count; // returns zero

            if (rowCount == 0)
            {
                btn_guardar_calle.CssClass = "btn_guardarCancelar";
                btn_guardar_calle.Enabled = true;
                drop_down_tipo_calle.Enabled = true;
            }
            else
            {
                btn_guardar_calle.CssClass = "btn_guardarCancelar_disabled";
                btn_guardar_calle.Enabled = false;
                drop_down_tipo_calle.Enabled = false;

            }

        }

        protected void btn_buscar_calle_Click(object sender, EventArgs e)
        {
            gridview_calle.DataSource = null;
            LoadCalles();

            gridview_parecido.DataSource = null; 
            Load_Sugerencias();
        }

        protected void btn_guardar_calle_Click(object sender, EventArgs e)
        {
            if (tb_calle.Text != "" )
            {
                modal_popup_guardar_calle.Show();             
            }
            else
            {
                lbl_alerta_colonia.Text = "Ingrese nombre de la colonia";
                lbl_alerta_colonia.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_colonia_modal_agregar_Click(object sender, EventArgs e)
        {
            var sp_guardar_colonia = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_GuardarColonias",
             new SqlParameter("@descripcion", tb_Colonia.Text),
             new SqlParameter("@cod_pos", drop_codigoPostal_colonia.SelectedItem.Text),
             new SqlParameter("@id_ciudad", drop_ciudad_colonia.SelectedValue),
             new SqlParameter("@usuario", Session["usuario"].ToString()),
             MsBarco.DbUtil.NewSqlParam("@alerta", null, SqlDbType.VarChar, ParameterDirection.Output, 20)
             );

            if (sp_guardar_colonia["@alerta"].ToString() == "0")
            {
                lbl_alerta_colonia.Text = "Ya existe un registro de la colonia con ese codigo postal";
                lbl_alerta_colonia.ForeColor = System.Drawing.Color.Red;
            }

            else
            {
                lbl_alerta_colonia.ForeColor = System.Drawing.Color.Green;
                lbl_alerta_colonia.Text = "Colonia agregada exitosamente";

                gridview_colonias.Visible = false;

            }
        }

        protected void btn_guardar_popUp_Calle_Click(object sender, EventArgs e)
        {
            var sp_guardar_calle = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_GuardarCalle",
                             new SqlParameter("@id_colonia", drop_colonia_calle.SelectedValue),
                             new SqlParameter("@nom_col", drop_colonia_calle.SelectedItem.Text),
                             new SqlParameter("@id_ciudad", drop_ciudad_calle.SelectedValue),
                             new SqlParameter("@calle", tb_calle.Text),
                             new SqlParameter("@tipo", drop_down_tipo_calle.SelectedItem.Text),
                             new SqlParameter("@usuario", Session["usuario"].ToString()),
                             MsBarco.DbUtil.NewSqlParam("@alerta", null, SqlDbType.VarChar, ParameterDirection.Output, 20)
                             );

            if (sp_guardar_calle["@alerta"].ToString() == "0")
            {
                lbl_alerta_calle.Text = "Ya existe un registro de la calle con esa colonia";
                lbl_alerta_calle.ForeColor = System.Drawing.Color.Red;
            }

            else
            {
                lbl_alerta_calle.ForeColor = System.Drawing.Color.Green;
                lbl_alerta_calle.Text = "Calle agregada exitosamente";
                gridview_calle.Visible = false;

            }
        }

        protected void drop_down_pais_codigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_down_pais_codigo.SelectedValue != "")
            {
                drop_down_estado_codigo.Enabled = true;
                drop_down_estado_codigo.Items.Clear();

                var sp_loadEstados = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_down_pais_codigo.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "1")
                    );

                drop_down_estado_codigo.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_down_estado_codigo.DataSource = sp_loadEstados;
                drop_down_estado_codigo.DataValueField = "idEstado";
                drop_down_estado_codigo.DataTextField = "descripcion";
                drop_down_estado_codigo.DataBind();

                drop_down_ciudad_codigo.SelectedValue = "";
                drop_down_ciudad_codigo.Enabled = false;

                drop_codigo_postal_calle.SelectedValue = "";
                drop_codigo_postal_calle.Enabled = false;

                tb_codigo_postal.Text = "";
                tb_codigo_postal.Enabled = false;
            }
            else
            {
                drop_down_estado_codigo.SelectedValue = "";
                drop_down_estado_codigo.Enabled = false;

                drop_down_ciudad_codigo.SelectedValue = "";
                drop_down_ciudad_codigo.Enabled = false;

                tb_codigo_postal.Text = "";
                tb_codigo_postal.Enabled = false;

            }
        }

        protected void drop_down_estado_codigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_down_estado_codigo.SelectedValue != "")
            {
                drop_down_ciudad_codigo.Enabled = true;
                drop_down_ciudad_codigo.Items.Clear();

                var sp_loadCiudades = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_down_estado_codigo.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "2")
                    );

                drop_down_ciudad_codigo.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_down_ciudad_codigo.DataSource = sp_loadCiudades;
                drop_down_ciudad_codigo.DataValueField = "idCiudad";
                drop_down_ciudad_codigo.DataTextField = "descripcion";
                drop_down_ciudad_codigo.DataBind();

                tb_codigo_postal.Text = "";
                tb_codigo_postal.Enabled = false;

            }
            else
            {
                drop_down_ciudad_codigo.SelectedValue = "";
                drop_down_ciudad_codigo.Enabled = false;

                tb_codigo_postal.Text = "";
                tb_codigo_postal.Enabled = false;

            }
        }

        protected void drop_down_ciudad_codigo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drop_down_ciudad_codigo.SelectedValue != "")
            {


                lbl_alerta_codigoPostal.Text = "";

                var sp_loadPrefijo = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_loadPrefijo",
                    new SqlParameter("@id_ciudad", drop_down_ciudad_codigo.SelectedValue),
                    MsBarco.DbUtil.NewSqlParam("@prefijo", null, SqlDbType.VarChar, ParameterDirection.Output, 20));


                if (sp_loadPrefijo["@prefijo"].ToString() != "")
                {
                    tb_codigo_postal.Text = sp_loadPrefijo["@prefijo"].ToString();
                    tb_codigo_postal.Enabled = true;

                }
                else
                {
                    tb_codigo_postal.Enabled = false;
                    lbl_alerta_codigoPostal.Text = "La ciudad seleccionada no cuenta con un prefijo de codigo postal";
                    lbl_alerta_codigoPostal.ForeColor = System.Drawing.Color.Red;
                }


            }
            else
            {
                tb_codigo_postal.Enabled = false;
                tb_codigo_postal.Text = "";
            }
        }

        protected void btn_buscar_codigo_Click(object sender, EventArgs e)
        {
            gridview_calle.DataSource = null;
            LoadCodigosPostales();
        }

        protected void LoadCodigosPostales()
        {
            var sp_loadColonias = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadCodigosPostales",
                    new SqlParameter("@id_ciudad", drop_down_ciudad_codigo.SelectedValue),
                    new SqlParameter("@codigo_postal", tb_codigo_postal.Text)
                    );
            gridview_codigos.DataSource = sp_loadColonias;
            gridview_codigos.DataBind();

            gridview_codigos.Visible = true;

            int rowCount = gridview_codigos.Rows.Count; // returns zero

            if (rowCount == 0)
            {
                btn_guardar_codigo.CssClass = "btn_guardarCancelar";
                btn_guardar_codigo.Enabled = true;
            }
            else
            {
                btn_guardar_codigo.CssClass = "btn_guardarCancelar_disabled";
                btn_guardar_codigo.Enabled = false;
            }
        }

        protected void gridview_codigos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview_codigos.PageIndex = e.NewPageIndex;
            LoadCodigosPostales();
        }

        protected void btn_guardar_popUp_codigoPostal_Click(object sender, EventArgs e)
        {
            var sp_loadPrefijo = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_loadPrefijo",
             new SqlParameter("@id_ciudad", drop_down_ciudad_codigo.SelectedValue),
            MsBarco.DbUtil.NewSqlParam("@prefijo", null, SqlDbType.VarChar, ParameterDirection.Output, 20));

            if (tb_codigo_postal.Text.StartsWith(sp_loadPrefijo["@prefijo"].ToString()))
            {
                var sp_guardar_calle = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_GuardarCodigoPostal",
                                             new SqlParameter("@id_ciudad", drop_down_ciudad_codigo.SelectedValue),
                                             new SqlParameter("@codigo_postal", tb_codigo_postal.Text),
                                             new SqlParameter("@usuario", Session["usuario"].ToString()),
                                             MsBarco.DbUtil.NewSqlParam("@alerta", null, SqlDbType.VarChar, ParameterDirection.Output, 20)
                                             );

                if (sp_guardar_calle["@alerta"].ToString() == "0")
                {
                    lbl_alerta_codigoPostal.Text = "Ya existe un registro del codigo postal con ese numero";
                    lbl_alerta_codigoPostal.ForeColor = System.Drawing.Color.Red;
                }

                else
                {
                    lbl_alerta_codigoPostal.ForeColor = System.Drawing.Color.Green;
                    lbl_alerta_codigoPostal.Text = "Codigo Postal agregado exitosamente";
                    gridview_codigos.Visible = false;

                }
            }
            else
            {
                lbl_alerta_codigoPostal.ForeColor = System.Drawing.Color.Red;
                lbl_alerta_codigoPostal.Text = "El codigo postal no inicia con el prefijo correcto: " + sp_loadPrefijo["@prefijo"].ToString();
            }
            
        }

        protected void btn_guardar_codigo_Click(object sender, EventArgs e)
        {
            if (tb_codigo_postal.Text != "")
            {
                modal_popup_guardar_codigoPostal.Show();
            }
            else
            {
                lbl_alerta_codigoPostal.Text = "Ingrese nombre de la colonia";
                lbl_alerta_codigoPostal.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void drop_pais_prefijo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_alerta_prefijo.Text = "";

            if (drop_pais_prefijo.SelectedValue != "")
            {
                drop_estado_prefijo.Enabled = true;
                drop_estado_prefijo.Items.Clear();

                var sp_loadEstados = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_pais_prefijo.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "1")
                    );

                drop_estado_prefijo.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_estado_prefijo.DataSource = sp_loadEstados;
                drop_estado_prefijo.DataValueField = "idEstado";
                drop_estado_prefijo.DataTextField = "descripcion";
                drop_estado_prefijo.DataBind();

                drop_ciudad_prefijo.SelectedValue = "";
                drop_ciudad_prefijo.Enabled = false;

                //drop_codigo_postal_calle.SelectedValue = "";
                //drop_codigo_postal_calle.Enabled = false;

                //tb_codigo_postal.Text = "";
                //tb_codigo_postal.Enabled = false;
            }
            else
            {
                drop_estado_prefijo.SelectedValue = "";
                drop_estado_prefijo.Enabled = false;

                drop_ciudad_prefijo.SelectedValue = "";
                drop_ciudad_prefijo.Enabled = false;

                //tb_codigo_postal.Text = "";
                //tb_codigo_postal.Enabled = false;

            }
        }

        protected void drop_estado_prefijo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_alerta_prefijo.Text = "";

            if (drop_estado_prefijo.SelectedValue != "")
            {
                drop_ciudad_prefijo.Enabled = true;
                drop_ciudad_prefijo.Items.Clear();

                var sp_loadCiudades = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadPais",
                    new SqlParameter("@idDropdown", drop_estado_prefijo.SelectedValue),
                    new SqlParameter("@idColonia", ""),
                    new SqlParameter("@seccion", "2")
                    );

                drop_ciudad_prefijo.Items.Add(new ListItem("-- Seleccionar --", ""));

                drop_ciudad_prefijo.DataSource = sp_loadCiudades;
                drop_ciudad_prefijo.DataValueField = "idCiudad";
                drop_ciudad_prefijo.DataTextField = "descripcion";
                drop_ciudad_prefijo.DataBind();



            }
            else
            {
                drop_ciudad_prefijo.SelectedValue = "";
                drop_ciudad_prefijo.Enabled = false;

            }
        }

        protected void drop_ciudad_prefijo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_alerta_prefijo.Text = "";
            if (drop_ciudad_prefijo.SelectedValue != "")
            {
                tb_prefijo.Enabled = true;
                var sp_loadPrefijo = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_loadPrefijo",
                                    new SqlParameter("@id_ciudad", drop_ciudad_prefijo.SelectedValue),
                                    MsBarco.DbUtil.NewSqlParam("@prefijo", null, SqlDbType.VarChar, ParameterDirection.Output, 20));

                tb_prefijo.Text = sp_loadPrefijo["@prefijo"].ToString();
                btn_guardar_prefijo.Enabled = true;
                btn_guardar_prefijo.CssClass = "btn_guardarCancelar";

            }
            else
            {
                tb_prefijo.Text = "";
                tb_prefijo.Enabled = false;
                btn_guardar_prefijo.Enabled = false;
                btn_guardar_prefijo.CssClass = "btn_guardarCancelar_disabled";

            }
        }

        protected void btn_guardar_prefijo_Click(object sender, EventArgs e)
        {
            if (tb_prefijo.Text != "" && tb_prefijo.Text.Length == 2)
            {
                modal_popup_guardar_prefijo.Show();

            }
            else
            {
                lbl_alerta_prefijo.Text = "Ingrese los 2 digitos del prefijo de codigo postal";
                lbl_alerta_prefijo.ForeColor = System.Drawing.Color.Red;

            }
        }

        protected void btn_guardar_popUp_prefijo_Click(object sender, EventArgs e)
        {
            if (tb_prefijo.Text != "" && tb_prefijo.Text.Length == 2)
            {
                var sp_guardar_calle = DbUtil.ExecuteProc("sp_recursos_desarrollo_urbano_guardarPrefijo",
                                        new SqlParameter("@id_ciudad", drop_ciudad_prefijo.SelectedValue),
                                        new SqlParameter("@prefijo", tb_prefijo.Text));

                lbl_alerta_prefijo.Text = "Prefijo actualizado correctamente";
                lbl_alerta_prefijo.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lbl_alerta_prefijo.Text = "Ingrese los 2 digitos del prefijo de codigo postal";
                lbl_alerta_prefijo.ForeColor = System.Drawing.Color.Red;

            }

            //sp_loadPrefijo["@alerta"].ToString() == "0"
        }

        protected void gridview_parecido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview_parecido.PageIndex = e.NewPageIndex;
            Load_Sugerencias();
        }

        protected void Load_Sugerencias()
        {
            var sp_loadColonias = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadSugerencias",
                    new SqlParameter("@id_ciudad", drop_ciudad_calle.SelectedValue),
                    new SqlParameter("@calle", tb_calle.Text)
                    );

            gridview_parecido.DataSource = sp_loadColonias;
            gridview_parecido.DataBind();

            gridview_parecido.Visible = true;

        }

        protected void gridview_colonias_parecidas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridview_colonias_parecidas.PageIndex = e.NewPageIndex;
            Load_Sugerencias_colonias();
        }

        protected void Load_Sugerencias_colonias()
        {
            var sp_loadColonias = DbUtil.GetCursor("sp_recursos_desarrollo_urbano_loadSugerencias_colonias",
                    new SqlParameter("@id_ciudad", drop_ciudad_colonia.SelectedValue),
                    new SqlParameter("@Colonia", tb_Colonia.Text)
                    );

            gridview_colonias_parecidas.DataSource = sp_loadColonias;
            gridview_colonias_parecidas.DataBind();

            gridview_colonias_parecidas.Visible = true;

        }

    }
}