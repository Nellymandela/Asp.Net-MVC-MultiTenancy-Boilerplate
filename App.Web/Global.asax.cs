using App.Core.Configurations;
using App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace App.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = System.Security.Claims.ClaimTypes.NameIdentifier;
            try
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Infrastructure.Migrations.Configuration>());
            }
            catch (Exception)
            {

            }
            //Service configurations starts here
            AutoMapperConfig.RegisterMappingObjects();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log the error!
            //_Logger.Error(ex);
        }
    }
}
