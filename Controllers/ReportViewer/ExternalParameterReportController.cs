using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ReportsMVCSamples.Models;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace ReportsMVCSamples.Controllers.ReportViewer
{
    public class ExternalParameterReportController : PreviewController
    {
        // GET: ExternalParameterReport
        [HttpPost]
        public ActionResult Index()
        {
            string productCategoryData = SqlQuery.getProductCategory();
            ViewBag.productCategory = productCategoryData;
            string productSubCategoryData = SqlQuery.getProductSubCategory();
            ViewBag.productSubCategory = productSubCategoryData;
            this.updateMetaData();
            return View();
        }
    }
}