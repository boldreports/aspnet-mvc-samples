using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportsMVCSamples.Models;

namespace ReportsMVCSamples.Controllers.ReportViewer
{
    public class ExternalParameterReportController : PreviewController
    {
        // GET: ExternalParameterReport
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