using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportsMVCSamples.Models;

namespace ReportsMVCSamples.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            dynamic sampleData = SampleData.getSampleData();
            string defaultPath = sampleData.samples[0].routerPath.Value;
            return RedirectToActionPermanent("Index", defaultPath);
        }
    }
}