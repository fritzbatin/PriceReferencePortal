using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceReferencePortal.Context;

namespace PriceReferencePortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new supplyEntity())
            {

                var data = context.supplies.Count(); // Return the list of data from the database
                Session["Supply Count"] = data.ToString();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}