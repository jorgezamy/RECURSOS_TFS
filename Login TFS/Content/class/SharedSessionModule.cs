using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using System.Web;

namespace Login_TFS.Content.clases
{
    public class SharedSessionModule
    {
        public void Init(HttpApplication context)
        {
            try
            {
                // Get the app name from config file...
                string appName = ConfigurationManager.AppSettings["ApplicationName"];
                if (!string.IsNullOrEmpty(appName))
                {
                    FieldInfo runtimeInfo = typeof(HttpRuntime).GetField("_theRuntime",
                                            BindingFlags.Static | BindingFlags.NonPublic);
                    HttpRuntime theRuntime = (HttpRuntime)runtimeInfo.GetValue(null);
                    FieldInfo appNameInfo = typeof(HttpRuntime).GetField("_appDomainAppId",
                                            BindingFlags.Instance | BindingFlags.NonPublic);
                    appNameInfo.SetValue(theRuntime, appName);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        /// <span class="code-SummaryComment"><summary></span>
        /// Disposes of the resources (other than memory) used by the module that
        /// implements <span class="code-SummaryComment"><see cref="T:System.Web.IHttpModule"/>.</span>
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><created date="5/31/2008" by="Peter Femiani"/></span>
        public void Dispose()
        {
        }
    }
}