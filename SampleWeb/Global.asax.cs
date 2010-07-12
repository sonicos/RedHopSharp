using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using RedHopSharp;
using System.Diagnostics;

namespace SampleWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

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
            HttpApplication thisapp = HttpContext.Current.ApplicationInstance;
            string message = DoError(HttpContext.Current.ApplicationInstance); 
            //string message = new HoptoadClient().Send(thisapp.Server.GetLastError().GetBaseException(),thisapp);
            if  ( ! message.Contains("RedHopToad Error:"))
           Response.Redirect("Error.aspx?e=" + message);
        }
        
        public static string DoError(HttpApplication application) 
        {
            return new HoptoadClient().Send(application.Server.GetLastError().GetBaseException(),application);
        
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}