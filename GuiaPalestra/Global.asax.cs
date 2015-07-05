using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace GuiaPalestra
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            var Formater = GlobalConfiguration.Configuration.Formatters;
            Formater.Remove(Formater.XmlFormatter);

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

           // WebApiConfig.Register(GlobalConfiguration.Configuration);

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);           
        }
    }
}