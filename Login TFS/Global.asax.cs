using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.Web.Optimization;
using MsBarco;
using Login_TFS.Content;

namespace Login_TFS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            String Constr = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            DbUtil.Init(Constr);
            //SessionStateServerSharedHelper.ChangeAppDomainAppId("YODADemoApp");
        }

        protected void Application_PostMapRequestHandler(object sender, EventArgs e)
        {
            //System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}