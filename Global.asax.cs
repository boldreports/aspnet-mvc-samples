using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bold.Licensing;
using BoldReports.Web;
using Newtonsoft.Json;
using BoldReports.Base.Logger;
using System.Reflection;

namespace ReportsMVCSamples
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string License = File.ReadAllText(Server.MapPath("BoldLicense.txt"), Encoding.UTF8);
            log4net.GlobalContext.Properties["LogPath"] = this.GetAppDataFolderPath();
            BoldReports.Base.Logger.LogExtension.RegisterLog4NetConfig();
            BoldLicenseProvider.RegisterLicense(License);
            ReportConfig.DefaultSettings = new ReportSettings()
            {
                MapSetting = this.GetMapSettings()
            };

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public string GetAppDataFolderPath()
        {
            try
            {
                return System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory );
            }
            catch
            {
                return null;
            }
        }
        private BoldReports.Web.MapSetting GetMapSettings()
        {
            try
            {
                string basePath = HttpContext.Current.Server.MapPath("~/Scripts");
                return new MapSetting()
                {   
                    ShapePath = basePath + "\\ShapeData\\",
                    MapShapes = JsonConvert.DeserializeObject<List<MapShape>>(System.IO.File.ReadAllText(basePath + "\\ShapeData\\mapshapes.txt"))
                };
            }
            catch (Exception ex)
            {
                LogExtension.LogError("Failed to Load Map Settings", ex, MethodBase.GetCurrentMethod());
            }
            return null;
        }

    }
}
