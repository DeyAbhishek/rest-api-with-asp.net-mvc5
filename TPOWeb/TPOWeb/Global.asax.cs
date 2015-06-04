using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TPOWeb.Controllers.App_Start;

namespace TPOWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            ApplicationLogConfig.EnableLogging(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            ResourceConfig.LoadResources(this, ConfigurationManager.AppSettings["MessageResources"]);
        }
    }
}
