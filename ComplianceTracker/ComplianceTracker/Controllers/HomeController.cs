using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplianceTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }

        public ActionResult HowItWorks()
        {
            return View("HowItWorks");
        }

        public ActionResult Pricing()
        {
            return View("Pricing");
        }
    }
}