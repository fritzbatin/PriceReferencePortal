using PriceReferencePortal.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PriceReferencePortal.Controllers
{
    public class DeliveryController : Controller
    {
        public DataTable GetUserID(string UserEmail)
        {
            DataTable dt = new DataTable();
            string strcon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from  AspNetUsers where Email = '" + UserEmail + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        [HttpGet] // Set the attribute to Read
        public ActionResult display()
        {
            using (var context = new orderEntities())
            {

                var data = context.Orders.Where(x => x.procurement_approval == "Approved").ToList(); // Return the list of data from the database
                return View(data);
            }
        }
        // GET: Accounting/Details/5
        public ActionResult deliveryDetails(int id)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.Where(x => x.Id == id).SingleOrDefault();
                Session["OrderLastID"] = data.Id;
                return View(data);
            }

        }
        [ChildActionOnly]
        public ActionResult ViewOrderDetail_order(int id)
        {
            //int id = (int)Session["OrderLastID"];

            //id = GetLastOrderID();
            int grandTotal = 0;
            using (var context = new orderDetailEntities()) //To open a connection to the database
            {
                //var data = context.orderDetails.ToList(); // Return the list of data from the database
                var data = context.orderDetails.Where(x => x.ordernumber == id).ToList();
                foreach (var item in context.orderDetails.Where(x => x.ordernumber == id).ToList())
                {
                    grandTotal += Int32.Parse(item.totalAmount);
                }
                Session["grandTotal"] = grandTotal;
                return PartialView("ViewOrderDetail_order", data);
            }

        }

        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        }

        // GET: Delivery/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Delivery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Delivery/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Delivery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Delivery/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Delivery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Delivery/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
