﻿using System;
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

          

            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);         
        }
    }
}