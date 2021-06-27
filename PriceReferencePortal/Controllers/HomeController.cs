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
            using (var context = new orderEntities())
            {
                //var data = context.Orders.Count(); // Return the list of data from the database
                var d = context.Orders.Where(dd => dd.procurement_approval == ""  ).Count();


                var procPendingCount = (from o in context.Orders
                             where o.procurement_approval == "Pending"
                             select o).Count();
                Session["procPendingCount"] = procPendingCount.ToString();

                var procApprovedCount = (from o in context.Orders
                                        where o.procurement_approval == "Approved"
                                        select o).Count();
                Session["procApprovedCount"] = procApprovedCount.ToString();

                var procDisapprovedCount = (from o in context.Orders
                                         where o.procurement_approval == "Disapproved"
                                         select o).Count();
                Session["procDisapprovedCount"] = procDisapprovedCount.ToString();


                var accPendingCount = (from a in context.Orders
                                       where a.accounting_approval == "Pending" &&
                                         a.procurement_approval == "Approved"
                                        select a).Count();
                Session["accPendingCount"] = accPendingCount.ToString();


                var accApprovalCount = (from a in context.Orders
                                   where a.accounting_approval == "Approved"
                                   select a).Count();
                Session["accApprovalCount"] = accApprovalCount.ToString();

                var accDisapprovedCount = (from a in context.Orders
                                        where a.accounting_approval == "Disapproved"
                                        select a).Count();
                Session["accDisapprovedCount"] = accDisapprovedCount.ToString();





                var deliveryPendingCount = (from dd in context.Orders
                                       where dd.delivery_status == "Pending" &&
                                       dd.accounting_approval == "Approved"
                                       select dd).Count();
                Session["deliveryPendingCount"] = deliveryPendingCount.ToString();


                var deliveryApprovalCount = (from dd in context.Orders
                                        where dd.delivery_status == "For Delivery"
                                        select d).Count();
                Session["deliveryApprovalCount"] = deliveryApprovalCount.ToString();

                var deliveryDisapprovedCount = (from dd in context.Orders
                                           where dd.delivery_status == "Disapproved"
                                           select dd).Count();
                Session["deliveryDisapprovedCount"] = deliveryDisapprovedCount.ToString();




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