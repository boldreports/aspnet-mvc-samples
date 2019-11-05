using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers
{
    public class ConditionalRowFormattingController : PreviewController
    {
        // GET: ConditionalRowFormatting
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}