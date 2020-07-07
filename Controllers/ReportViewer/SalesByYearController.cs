using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers.ReportViewer
{
    public class SalesByYearController : PreviewController
    {
        // GET: ProductList
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}