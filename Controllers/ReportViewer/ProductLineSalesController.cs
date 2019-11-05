using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers
{
    public class ProductLineSalesController : PreviewController
    {
        // GET: ProductLineSales
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}