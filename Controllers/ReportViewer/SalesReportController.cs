using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers
{
    public class SalesReportController : PreviewController
    {
        // GET: SalesReport
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}