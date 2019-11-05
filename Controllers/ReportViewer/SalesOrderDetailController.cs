using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportsMVCSamples.Controllers
{
    public class SalesOrderDetailController : PreviewController
    {
        // GET: SalesOrderDetail
        public ActionResult Index()
        {
            this.updateMetaData();
            return View();
        }
    }
}