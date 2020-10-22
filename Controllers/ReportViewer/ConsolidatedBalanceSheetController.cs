using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers.ReportViewer
{
    public class ConsolidatedBalanceSheetController : PreviewController
    {
        // GET: ConsolidatedBalanceSheet
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}