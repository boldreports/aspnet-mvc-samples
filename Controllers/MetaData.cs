using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ReportsMVCSamples.Models;

namespace ReportsMVCSamples.Controllers
{
    public class MetaData : Controller
    {
        dynamic getReportSampleData(string controllerName)
        {
            dynamic samples = SampleData.getSampleData().samples;
            dynamic sampleData = null;
            foreach (dynamic sample in samples)
            {
                if ((sample.routerPath == "" && sample.basePath == controllerName) || sample.routerPath == controllerName)
                {
                    sampleData = sample;
                    break;
                }
            }

            return sampleData;
        }

        public void updateMetaData()
        {
            string controllerName = this.RouteData.Values["Controller"] as string;
            dynamic sampleData = getReportSampleData(controllerName);
            MetaDataInfo metaData;
            if (ViewBag.action == "Preview")
            {
                metaData = this.updatePreviewMetaData(sampleData);
            }
            else
            {
                metaData = this.updateSampleMetaData(sampleData);
            }
            ViewBag.Title = metaData.title;
            ViewBag.Description = metaData.metaContent;
        }

        public MetaDataInfo updateSampleMetaData(dynamic sampleData)
        {
            string title = String.IsNullOrEmpty((string)sampleData.metaData.title) ? sampleData.sampleName : sampleData.metaData.title;
            string basePath = new Regex(@"(?<!^)(?=[A-Z])").Replace((string)sampleData.basePath, " ", 1);
            title += " | ASP.NET MVC " + basePath.Trim();
            title = title.Length < 45 ? title += " | Bold Reports" : title;
            return new MetaDataInfo(title, (string)sampleData.metaData.description);

        }

        public MetaDataInfo updatePreviewMetaData(dynamic sampleData)
        {
            string title = String.IsNullOrEmpty((string)sampleData.metaData.title) ? sampleData.sampleName : sampleData.metaData.title;
            string metaContent;
            switch ((string)sampleData.basePath)
            {
                case "ReportViewer":
                    metaContent = "The ASP.NET MVC Bold Report Viewer allows the end-users to visualize the " + title + " report in browsers.";
                    title += " | Preview | ASP.NET MVC Report Viewer";
                    break;
                case "ReportWriter":
                    title += " | Preview | ASP.NET MVC Report Writer";
                    metaContent = "The ASP.NET MVC Bold Report Writer allows the end-users to download the report in browsers without visualizing the report.";
                    break;
                default:
                    title = "";
                    metaContent = "";
                    break;
            }

            title = title.Length < 45 ? title += " | Bold Reports" : title;
            return new MetaDataInfo(title, metaContent);
        }

        public class MetaDataInfo
        {
            public string title { get; set; }
            public string metaContent { get; set; }

            public MetaDataInfo(string title, string metaContent)
            {
                this.title = title;
                this.metaContent = metaContent;
            }
        }
    }
}