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
    public class AccountingController : Controller
    {

        // GET: Accounting
        public ActionResult Index()
        {
            return View();
        }

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

                var data = context.Orders.Where(x => x.delivery_status == "For Delivery").ToList(); // Return the list of data from the database
                return View(data);
            }
        }





        // GET: Accounting/Details/5
        public ActionResult accDetails(int id)
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
        public ActionResult accDetails(int id, string submitButton, Order order)
        {
            //string message = "";
            int gT = Int32.Parse(Session["grandTotal"].ToString());
            string tac = order.terms_and_condition;
            int tP = 0;
            bool isNum = int.TryParse(order.totalPayment.ToString(), out tP); 
            if (isNum)
            {
                if (tP > gT)
                {
                    ViewBag.ErrorMessage = "The total payment is more than Grand total of the orders.";
                    return View(order);
                }
                else if (tP < gT && tac == "" || tac == null)
                {
                    ViewBag.ErrorMessage = "Please input terms and condition.";
                    return View(order);
                }
                else if (tP < 0)
                {
                    ViewBag.ErrorMessage = "Please input positive number in total payment.";
                    return View(order);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please input number total payment.";
            }
            

            
            if (ModelState.IsValid)
            {
                var dt_userid = GetUserID(User.Identity.GetUserName());
                string val_FirstName = dt_userid.Rows[0]["FirstName"].ToString();
                string val_LastName = dt_userid.Rows[0]["LastName"].ToString();
                string username = $"{val_FirstName} {val_LastName}";

                switch (submitButton)
                {
                    case "Approved":
                        // delegate sending to another controller action
                        return (Approved(id, username, order));
                    case "Disapproved":
                        // call another action to perform the cancellation
                        return (Disapproved(id, username, order));
                    default:
                        // If they've submitted the form without a submitButton, 
                        // just return the view again.
                        return (View(order));
                }
            }
            else
            {
                return View(order);
            }

        }
       

        private ActionResult Approved(int id, string user, Order order)
        {
            //if (order.totalPayment == "")
            //{
            //    ViewBag.ErrorMessage = "Wrong Username or Password";
            //}
            using (var context = new orderEntities())
            {
                var data = context.Orders.FirstOrDefault(y => y.Id == id); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    data.accounting_approval = "Approved";
                    data.acc_approve_date = DateTime.Now.ToString("dd-MMM-yyyy");
                    data.acc_approve_by = user;
                    data.terms_and_condition = order.terms_and_condition;
                    data.totalPayment = order.totalPayment;
                    data.delivery_status = "For Delivery";
                    context.SaveChanges();
                    return RedirectToAction("Display"); // It will redirect to the Read method
                }
                else
                    return View("Display");
            }
        }

        private ActionResult Disapproved(int id, string user, Order order)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.FirstOrDefault(y => y.Id == id); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    data.accounting_approval = "Disapproved";
                    data.acc_approve_date = DateTime.Now.ToString("dd-MMM-yyyy");
                    data.acc_approve_by = user;
                    data.terms_and_condition = "";
                    data.totalPayment = "0";
                    data.delivery_status = "Disapproved";
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




        // GET: Accounting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounting/Create
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

        // GET: Accounting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Accounting/Edit/5
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

        // GET: Accounting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Accounting/Delete/5
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
