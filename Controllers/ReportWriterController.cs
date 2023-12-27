using BoldReports.Web;
using BoldReports.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace ReportsMVCSamples.Controllers.ReportViewer
{
    public class ReportWriterController : PreviewController
    {
        internal ExternalServer EServer { get; set; }
        public string ServerURL { get; set; }

        public string getName(string name)
        {
            string[] splittedNames = name.Split('-');
            string sampleName = "";
            foreach (string splittedName in splittedNames)
            {
                sampleName += (char.ToUpper(splittedName[0]) + splittedName.Substring(1));
            }
            return sampleName;
        }

        // GET: ReportWriter
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void generate(string reportName, string type)
        {
            try
            {
                string fileName = reportName.Contains("-") ? getName(reportName) : (char.ToUpper(reportName[0]) + reportName.Substring(1));
                WriterFormat format;
                HttpContext httpContext = System.Web.HttpContext.Current;
                string resourcesPath = Server.MapPath("~/Scripts");
                ReportWriter reportWriter = new ReportWriter();
                
                ExternalServer externalServer = new ExternalServer();
                this.EServer = externalServer;
                this.ServerURL = "Sample";
                externalServer.ReportServerUrl = this.ServerURL;
                reportWriter.ReportingServer = this.EServer;
                reportWriter.ReportServerUrl = this.ServerURL;
                reportWriter.ReportServerCredential = System.Net.CredentialCache.DefaultCredentials;

                reportWriter.ReportProcessingMode = ProcessingMode.Remote;
                reportWriter.ExportSettings = new customBrowsertype(resourcesPath);
                reportWriter.ExportResources.BrowserType = ExportResources.BrowserTypes.External;
                reportWriter.ExportResources.ResourcePath = resourcesPath + @"\puppeteer\";
                reportWriter.ReportPath = Server.MapPath("~/Resources/Report/" + reportName + ".rdl");

                reportWriter.ExportResources.Scripts = new List<string>
                {
                    "../../bold-reports/v2.0/common/bold.reports.common.min.js",
                    "../../bold-reports/v2.0/common/bold.reports.widgets.min.js",
                    //Report Viewer Script
                    "../../bold-reports/v2.0/bold.report-viewer.min.js"
                };

                reportWriter.ExportResources.DependentScripts = new List<string>
                {
                    "../../dependent/jquery.min.js"
                };

                if (type == "pdf")
                {
                    fileName += ".pdf";
                    format = WriterFormat.PDF;
                }
                else if (type == "word")
                {
                    fileName += ".docx";
                    format = WriterFormat.Word;
                }
                else if (type == "csv")
                {
                    fileName += ".csv";
                    format = WriterFormat.CSV;
                }
                else if (type == "html")
                {
                    fileName += ".html";
                    format = WriterFormat.HTML;
                }
                else if (type == "ppt")
                {
                    fileName += ".ppt";
                    format = WriterFormat.PPT;
                }
                else if (type == "xml")
                {
                    fileName += ".xml";
                    format = WriterFormat.XML;
                }
                else
                {
                    fileName += ".xlsx";
                    format = WriterFormat.Excel;
                }
                reportWriter.Save(fileName, format, httpContext.Response);
            }
            catch { }
        }
    }

    public class customBrowsertype : ExportSettings
    {
        string resourcesPath;

        public customBrowsertype(string hostingEnvironment)
        {
            resourcesPath = hostingEnvironment;
        }
        public override string GetImageFromHTML(string url)
        {
            return ConvertBase64(url).Result;
        }
        public async Task<string> ConvertBase64(string url)
        {
            string puppeteerChromeExe = resourcesPath + @"\puppeteer\Win-901912\chrome-win\chrome.exe";
            var browser = await PuppeteerSharp.Puppeteer.LaunchAsync(new PuppeteerSharp.LaunchOptions { Headless = true, ExecutablePath = puppeteerChromeExe });
            var page = await browser.NewPageAsync();
            await page.GoToAsync(url);
            return await page.WaitForSelectorAsync("#imagejsonData").Result.GetPropertyAsync("innerText").Result.JsonValueAsync<string>();
        }
    }
}