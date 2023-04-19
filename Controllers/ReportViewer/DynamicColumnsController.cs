using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers.ReportViewer
{
    public class DynamicColumnsController : PreviewController
    {
        // GET: DynamicColumns
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}