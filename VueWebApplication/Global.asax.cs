using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Optimization;
using VueWebApplication.Models;

namespace VueWebApplication
{
    public class Global : HttpApplication
    {
        void Application_Start()
        { 
            GlobalConfiguration.Configure(WebApiConfig.Register); 
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer(new StartDbInitializer());
            //MyDbContext.init();

        }
    }
}