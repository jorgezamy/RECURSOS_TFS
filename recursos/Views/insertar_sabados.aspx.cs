using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using MsBarco;

namespace recursos.Views
{
    public partial class insertar_sabados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sp_load = DbUtil.ExecuteProc("sp_recursos_insertar_sabados"
                );

            //Servidor 
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}