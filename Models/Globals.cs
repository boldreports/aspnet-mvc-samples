using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using BoldReports.Models.ReportViewer;

namespace ReportsMVCSamples.Models
{
    public class Globals
    {
        public static string basePath = HttpContext.Current.Request.ApplicationPath == "/" ? "/" : HttpContext.Current.Request.ApplicationPath + "/";
        public static string SERVICE_URL = basePath + "api/ReportViewerWebApi";
        public static string DESIGNER_SERVICE_URL = basePath + "api/ReportDesignerWebApi";
        public static bool isPhatomJSExist = File.Exists(HttpContext.Current.Server.MapPath("~/Scripts") + @"\PhantomJS\phantomjs.exe");
    }
}
