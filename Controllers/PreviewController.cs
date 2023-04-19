using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportsMVCSamples.Models;

namespace ReportsMVCSamples.Controllers
{
    public class PreviewController : MetaData
    {
        // GET: Preview
        public ActionResult Preview()
        {
            string foderName = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.action = "Preview";
            ViewBag.isDesigner = false;
            if(foderName== "ExternalParameterReport") {
                string productCategoryData = SqlQuery.getProductCategory();
                ViewBag.productCategory = productCategoryData;
                string productSubCategoryData = SqlQuery.getProductSubCategory();
                ViewBag.productSubCategory = productSubCategoryData;
            }
            this.updateMetaData();
            return View("~/Views/" + foderName + "/Index.cshtml");

        }
    }
}