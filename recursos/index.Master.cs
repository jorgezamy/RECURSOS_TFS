﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace recursos
{
    public partial class index : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}