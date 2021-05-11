using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers
{
    public class PaystubController : PreviewController
    {
        // GET: Barcode
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}