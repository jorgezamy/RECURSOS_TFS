using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Login_TFS.Content
{
    public class SessionStateServerSharedHelper
    {
        public static void ChangeAppDomainAppId(string name)
        {
            FieldInfo runtimeInfo = typeof(HttpRuntime).GetField("_theRuntime", BindingFlags.Static | BindingFlags.NonPublic);
            if (runtimeInfo == null) return;
            HttpRuntime theRuntime = (HttpRuntime)runtimeInfo.GetValue(null);
            if (theRuntime == null) return;
            FieldInfo appNameInfo = typeof(HttpRuntime).GetField("_appDomainAppId", BindingFlags.Instance | BindingFlags.NonPublic);
            if (appNameInfo == null) return;
            var appName = (String)appNameInfo.GetValue(theRuntime);
            if (appName != "applicationName") appNameInfo.SetValue(theRuntime, name);
        }
    }
}