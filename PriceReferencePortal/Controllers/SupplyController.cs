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
    public class SupplyController : Controller
    {


    

        //// GET: CRUD
        [HttpGet] // Set the attribute to Read
        public ActionResult display()
        {
            using (var context = new supplyEntity())
            {
                
                var data = context.supplies.ToList(); // Return the list of data from the database
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
            ViewBag.DateCreated = DateTime.Now.ToString("dd-MMM-yyyy");
            //ViewBag.CreatedBy = User.Identity.GetUserName();

            var dt_userid = GetUserID(User.Identity.GetUserName());
            string val_FirstName = dt_userid.Rows[0]["FirstName"].ToString();
            string val_LastName = dt_userid.Rows[0]["LastName"].ToString();
            ViewBag.CreatedBy = $"{val_FirstName} {val_LastName}";

            return View();
        }

        // POST: Supply/Create
        [HttpPost]
        public ActionResult Create(supply  model)
        {
         
                // TODO: Add insert logic here
                using (var context = new supplyEntity()) //To open a connection to the database
                {
                    context.supplies.Add(model); // Add data to the particular table
                    context.SaveChanges(); // save the changes to the that are made
                }
                string message = "Created the record successfully";
                ViewBag.Message = message;

                return RedirectToAction("Display"); // It will redirect to the Display method

        }

        // GET: Supply/Update/5
        public ActionResult Update(int id)
        {
            using (var context = new supplyEntity())
            {
                var data = context.supplies.Where(x => x.Id== id).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Supply/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, supply model)
        {
            using (var context = new supplyEntity())
            {
                var data = context.supplies.FirstOrDefault(x => x.Id == id); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {
                    data.vendor = model.vendor;
                    data.sku = model.sku;
                    data.description = model.description;
                    data.price = model.price;
                    data.delivery_lead_time = model.delivery_lead_time;
                    data.validity = model.validity;
                    data.date_created = model.date_created;
                    context.SaveChanges();
                    return RedirectToAction("Display"); // It will redirect to the Display method
                }
                else
                    return View();
            }
        }

        // GET: Supply/Delete/5
        public ActionResult Delete(int id)
        {
            using (var context = new supplyEntity())
            {
                var data = context.supplies.Where(x => x.Id == id).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Supply/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (var context = new supplyEntity())
            {
                var data = context.supplies.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    context.supplies.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Display");
                }
                else
                    return View();
            }
        }
    }
}
