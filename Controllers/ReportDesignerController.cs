using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportsMVCSamples.Models;

namespace ReportsMVCSamples.Controllers
{
    public class ReportDesignerController : Controller
    {
        // GET: Designer
        public ActionResult Index()
        {
            ViewBag.action = "Preview";
            updateMetaData("RDL");
            return View();
        }

        public ActionResult RDLC()
        {
            ViewBag.action = "Preview";
            updateMetaData("RDLC");
            return View("~/Views/RDLC/Index.cshtml");
        }



        dynamic getReportSampleData(string routerPath)
        {
            dynamic samples = SampleData.getSampleData().samples;
            dynamic sampleData = null;
            foreach (dynamic sample in samples)
            {
                if (sample.routerPath == routerPath)
                {
                    sampleData = sample;
                    break;
                }
            }

            return sampleData;
        }

        public void updateMetaData(string rdlcType)
        {
            string reportName = this.HttpContext.Request.QueryString["report-name"];
            dynamic sampleData;
            if (!string.IsNullOrEmpty(reportName))
            {
                string formattedName = "";
                string[] splittedNames = reportName.Split('.')[0].Split('-');
                for (int i = 0; i < splittedNames.Length; i++)
                {
                    formattedName += Char.ToUpper(splittedNames[i][0]) + splittedNames[i].Substring(1);
                }
                sampleData = getReportSampleData(formattedName.Trim());
            }
            else
            {
                sampleData = new { sampleName = rdlcType + " sample", metaData = new { title = "" } };
            }

            updateDesignerMetaData(sampleData);

        }

        public void updateDesignerMetaData(dynamic sampleData)
        {
            string title = String.IsNullOrEmpty((string)sampleData.metaData.title) ? sampleData.sampleName : sampleData.metaData.title;
            string metaContent = "The ASP.NET MVC bold report designer allows the end-users to arrange/customize the reports appearance in browsers." +
                        "It helps to edit the " + title + " for customer\"s application needs.";
            title += " | ASP.NET MVC Report Designer";
            ViewBag.Title = title.Length < 45 ? title += " | Bold Reports" : title;
            ViewBag.Description = metaContent;
        }
    }
}