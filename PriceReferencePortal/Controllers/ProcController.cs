using Microsoft.AspNet.Identity;
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
    public class ProcController : Controller
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

        // GET: Proc
        public ActionResult Index()
        {
            return View();
        }
        // GET: Proc/Display/5
        [HttpGet] // Set the attribute to Read
        public ActionResult display()
        {
            using (var context = new orderEntities())
            {

                var data = context.Orders.ToList(); // Return the list of data from the database
                return View(data);
            }

        }
        // GET: Proc/Details/5
        public ActionResult Details(int id)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.Where(x => x.Id == id).SingleOrDefault();
                Session["OrderLastID"] = data.Id;
                return View(data);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id, string submitButton,Order order)
        {
            var dt_userid = GetUserID(User.Identity.GetUserName());
            string val_FirstName = dt_userid.Rows[0]["FirstName"].ToString();
            string val_LastName = dt_userid.Rows[0]["LastName"].ToString();
            string username = $"{val_FirstName} {val_LastName}";

            switch (submitButton)
            {
                case "Approved":
                    // delegate sending to another controller action
                    return (Approved(id, username));
                case "Disapproved":
                    // call another action to perform the cancellation
                    return (Disapproved(id, username));
                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return (View());
            }

        }
        private ActionResult Approved(int id,string user)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.FirstOrDefault(y => y.Id == id); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    data.procurement_approval = "Approved";
                    data.proc_approval_date= DateTime.Now.ToString("dd-MMM-yyyy");
                    data.proc_approve_by = user;
                    context.SaveChanges();
                    return RedirectToAction("Display"); // It will redirect to the Read method
                }
                else
                    return View("Display");
            }
        }

        private ActionResult Disapproved(int id, string user)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.FirstOrDefault(y => y.Id == id); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    data.procurement_approval = "Disapproved";
                    data.proc_approval_date = DateTime.Now.ToString("dd-MMM-yyyy");
                    data.proc_approve_by = user;
                    context.SaveChanges();
                    return RedirectToAction("Display"); // It will redirect to the Read method
                }
                else
                    return View("Display");
            }
        }

        [ChildActionOnly]
        public ActionResult ViewOrderDetail_order(int id)
        {
            //int id = (int)Session["OrderLastID"];

            //id = GetLastOrderID();

            using (var context = new orderDetailEntities()) //To open a connection to the database
            {
                //var data = context.orderDetails.ToList(); // Return the list of data from the database
                var data = context.orderDetails.Where(x => x.ordernumber == id).ToList();
                return PartialView("ViewOrderDetail_order", data);
            }

        }

        // GET: Proc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proc/Create
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

        // GET: Proc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proc/Edit/5
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

        // GET: Proc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proc/Delete/5
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
