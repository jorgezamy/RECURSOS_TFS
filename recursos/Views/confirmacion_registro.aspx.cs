﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace recursos.Views
{
    public partial class confirmacion_registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_continuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}