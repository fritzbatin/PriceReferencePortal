using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PriceReferencePortal.Context;
using PriceReferencePortal.Models;

namespace PriceReferencePortal.Controllers
{
    public class OrderController : Controller
    {
        //// GET: Order
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[ChildActionOnly]
        public ActionResult ViewSupply()
        {
            using (var context = new supplyEntity())
            {

                var data = context.supplies.ToList(); // Return the list of data from the database
                return PartialView("ViewSupply", data);
            }

        }

        // GET: Order/Details/5
        [HttpGet] // Set the attribute to Read
        public ActionResult display()
        {
            using (var context = new orderEntities())
            {

                var data = context.Orders.ToList(); // Return the list of data from the database
                return View(data);
            }

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
        // GET: Supply/Create
        public ActionResult Create()
        {
            ViewBag.orderCreated = DateTime.Now.ToString("dd-MMM-yyyy");
            //ViewBag.CreatedBy = User.Identity.GetUserName();

            var dt_userid = GetUserID(User.Identity.GetUserName());
            string val_FirstName = dt_userid.Rows[0]["FirstName"].ToString();
            string val_LastName = dt_userid.Rows[0]["LastName"].ToString();
            ViewBag.CreatedBy = $"{val_FirstName} {val_LastName}";
            return View();
        }

        // GET: Order/Create
        [HttpPost]    //Specify the type of attribute i.e. it will add the record to the database
        public ActionResult create(Order model)
        {
            using (var context = new orderEntities()) //To open a connection to the database
            {
                context.Orders.Add(model); // Add data to the particular table
                context.SaveChanges(); // save the changes to the that are made
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;     // To display the message on the screen after the record is created successfully
            return View(); // write @Viewbag.Message in the created view at the place where you want to display the message
        }


        // GET: Order/Edit/5
        public ActionResult Update(int orderid) // To fill data in the form to enable easy editing
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.Where(x => x.Id == orderid).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult Update(int orderId, Order model)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.FirstOrDefault(y =>y.Id  == orderId); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    data.supply_ids = model.supply_ids;
                    
                    data.date_created = model.date_created;
                    data.created_by = model.created_by;
                    
                    data.procurement_approval = model.procurement_approval;
                    data.proc_approval_date = model.proc_approval_date;

                    data.proc_approve_by = model.proc_approve_by;
                    data.accounting_approval = model.accounting_approval;

                    data.acc_approve_date = model.acc_approve_date;
                    data.acc_approve_by = model.acc_approve_by;

                    context.SaveChanges();
                    return RedirectToAction("Display"); // It will redirect to the Read method
                }
                else
                    return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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
