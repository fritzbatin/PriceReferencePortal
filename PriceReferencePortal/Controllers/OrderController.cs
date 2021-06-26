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
        public int GetLastOrderID()
        {
            int lastId=0;

            using (var context = new orderEntities())
            {
                int v = context.Orders.Count();
                if (v > 0)
                {
                    lastId = context.Orders.Max(item => item.Id); // get the last id in Order db
                }
            }
            
            return lastId += 1;

        }
        [ChildActionOnly]
        public ActionResult ViewOrderDetail()
        {
            //int id = (int)Session["OrderLastID"];

            int id = GetLastOrderID();
          
            using (var context = new orderDetailEntities()) //To open a connection to the database
            {
                //var data = context.orderDetails.ToList(); // Return the list of data from the database
                var data = context.orderDetails.Where(x => x.ordernumber == id).ToList(); 
                return PartialView("ViewOrderDetail", data);
            }

        }

        

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
        //int lastId = 0;
        
        // GET: Supply/Create
        public ActionResult Create()
        {
            ViewBag.orderCreated = DateTime.Now.ToString("dd-MMM-yyyy");
            //ViewBag.CreatedBy = User.Identity.GetUserName();

            var dt_userid = GetUserID(User.Identity.GetUserName());
            string val_FirstName = dt_userid.Rows[0]["FirstName"].ToString();
            string val_LastName = dt_userid.Rows[0]["LastName"].ToString();
            ViewBag.CreatedBy = $"{val_FirstName} {val_LastName}";
            int lastId = 0;

            lastId=GetLastOrderID();
            Session["OrderLastID"] = lastId;
            ViewBag.orderLastId = lastId;


            return View();
        }

        // GET: Order/Create
        [HttpPost]    //Specify the type of attribute i.e. it will add the record to the database
        public ActionResult create(Order model)
        {
            using (var context = new orderEntities()) //To open a connection to the database
            {
                model.procurement_approval = "Pending";
                model.accounting_approval = "Pending";
           

                context.Orders.Add(model); // Add data to the particular table
                context.SaveChanges(); // save the changes to the that are made
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;     // To display the message on the screen after the record is created successfully
            return RedirectToAction("Display");
        }
        [HttpGet]
        public ActionResult GetOrder(int orderId)
        {
            using (var context = new supplyEntity())
            {
                var data = context.supplies.Where(x => x.Id == orderId).SingleOrDefault();
                ViewBag.SupplyID = data.Id;
                return View(data);
            }

            

        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult GetOrder(orderDetail model)
        {
            using (var context = new orderDetailEntities()) //To open a connection to the database
            {
                context.orderDetails.Add(model); // Add data to the particular table
                context.SaveChanges(); // save the changes to the that are made
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;     // To display the message on the screen after the record is created successfully
            return View("Create"); // write @Viewbag.Message in the created view at the place where you want to display the message

        }
        //===============================Order Detail===================================
        // GET: Order/Edit/5
        [HttpGet]
        public ActionResult UpdateOrderDetail(int orderid) // To fill data in the form to enable easy editing
        {
            using (var context = new orderDetailEntities())
            {
                var data = context.orderDetails.Where(x => x.Id == orderid).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult UpdateOrderDetail(int orderId, orderDetail model)
        {
            using (var context = new orderDetailEntities())
            {
                var data = context.orderDetails.FirstOrDefault(y => y.Id == orderId); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    //data.supplyId = model.supplyId;
                    //data.date_created = model.date_created;
                    //data.vendor = model.vendor;
                    //data.sku = model.sku;
                    //data.description = model.description;
                    //data.delivery_lead_time = model.delivery_lead_time;
                    //data.validity = model.validity;
                    //data.date_created = model.date_created;
                    data.price = model.price;
                    data.qty = model.qty;
                    data.totalAmount = model.totalAmount;


                    context.SaveChanges();
                    return RedirectToAction("Create"); // It will redirect to the Read method
                }
                else
                    return View();
            }
        }

        [HttpGet] // Set the attribute to Read
        public ActionResult DeleteOrderDetail(int orderId)
        {
            using (var context = new orderDetailEntities())
            {
                var data = context.orderDetails.Where(x => x.Id == orderId).SingleOrDefault();
                return View(data);
            }

        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrderDetail(int orderId, FormCollection collection)
        {
            try
            {
                using (var context = new orderDetailEntities())
                {
                    var data = context.orderDetails.FirstOrDefault(x => x.Id == orderId);
                    if (data != null)
                    {
                        context.orderDetails.Remove(data);
                        context.SaveChanges();
                        return RedirectToAction("Create");
                    }
                    else
                        return Create();
                }
            }
            catch (Exception)
            {

                return Create();
            }

           
        }

        //===============================Order Detail===================================






        //===============================Order===========================================
        [HttpGet]
        public ActionResult order_UpdateOrderDetail(int orderid) // To fill data in the form to enable easy editing
        {
            using (var context = new orderDetailEntities())
            {
                var data = context.orderDetails.Where(x => x.Id == orderid).SingleOrDefault();
                return View(data);
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult order_UpdateOrderDetail(int orderId, orderDetail model)
        {
            using (var context = new orderDetailEntities())
            {
                var data = context.orderDetails.FirstOrDefault(y => y.Id == orderId); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    //data.supplyId = model.supplyId;
                    //data.date_created = model.date_created;
                    //data.vendor = model.vendor;
                    //data.sku = model.sku;
                    //data.description = model.description;
                    //data.delivery_lead_time = model.delivery_lead_time;
                    //data.validity = model.validity;
                    //data.date_created = model.date_created;
                    data.price = model.price;
                    data.qty = model.qty;
                    data.totalAmount = model.totalAmount;


                    context.SaveChanges();
                    return RedirectToAction("UpdateOrder/"+ model.ordernumber); // It will redirect to the Read method
                }
                else
                    return View();
            }
        }

        [HttpGet]
        public ActionResult UpdateOrder(int id) // To fill data in the form to enable easy editing    
        {
            using (var context = new orderEntities())
            {
                
                var data = context.Orders.Where(x => x.Id == id).SingleOrDefault();
                
                Session["OrderLastID"] = data.Id;
                return View(data);
            }


        }

        // POST: Order/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult UpdateOrder(int id, Order model)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.FirstOrDefault(y => y.Id == id); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {

                    //data.supplyId = model.supplyId;
                    //data.date_created = model.date_created;
                    //data.vendor = model.vendor;
                    //data.sku = model.sku;
                    //data.description = model.description;
                    //data.delivery_lead_time = model.delivery_lead_time;
                    //data.validity = model.validity;
                    //data.date_created = model.date_created;
                    //data.price = model.price;
                    data.created_by = model.created_by;
                    data.supply_ids = model.supply_ids;
                    data.date_created = DateTime.Now.ToString("dd-MMM-yyyy"); 
                    context.SaveChanges();
                    return RedirectToAction("Display"); // It will redirect to the Read method
                }
                else
                    return View();
            }
        }
        public ActionResult order_UpdateViewSupply()
        {
            using (var context = new supplyEntity())
            {

                var data = context.supplies.ToList(); // Return the list of data from the database
                
                return PartialView("order_UpdateViewSupply", data);
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

        [HttpGet]
        public ActionResult order_updateGetOrder(int id)
        {
            using (var context = new supplyEntity())
            {
                //int id = GetLastOrderID();
                var data = context.supplies.Where(x => x.Id == id).SingleOrDefault();
                ViewBag.SupplyID = data.Id;

                return View(data);
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult order_updateGetOrder(int id, orderDetail model)
        {
            using (var context = new orderDetailEntities()) //To open a connection to the database
            {
                context.orderDetails.Add(model); // Add data to the particular table
                context.SaveChanges(); // save the changes to the that are made
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;     // To display the message on the screen after the record is created successfully
            return RedirectToAction("UpdateOrder", new { id = model.ordernumber });

        }

        [HttpGet] // Set the attribute to Read
        public ActionResult order_DeleteOrderDetail(int orderId)
        {
            using (var context = new orderDetailEntities())
            {
                var data = context.orderDetails.Where(x => x.Id == orderId).SingleOrDefault();
                return View(data);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult order_DeleteOrderDetail(int orderId, int updateID)
        {
            try
            {
                var iddd = updateID;
                using (var context = new orderDetailEntities())
                {
                    var data = context.orderDetails.FirstOrDefault(x => x.Id == orderId);
                    if (data != null)
                    {
                        context.orderDetails.Remove(data);
                        context.SaveChanges();
                        return RedirectToAction("UpdateOrder/" + updateID);
                    }
                    else
                        return Create();
                }
            }
            catch (Exception)
            {

                return Create();
            }


        }


        [HttpGet] // Set the attribute to Read
        public ActionResult DeleteOrder(int id)
        {
            using (var context = new orderEntities())
            {
                var data = context.Orders.Where(x => x.Id == id).SingleOrDefault();
                return View(data);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(int id, FormCollection form)
        {
            try
            {
                using (var context = new orderEntities())
                {
                    var data = context.Orders.FirstOrDefault(x => x.Id == id);
                    if (data != null)
                    {
                        context.Orders.Remove(data);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                return View();
            }

            return RedirectToAction("Display");

        }
        //===============================Order===========================================






        //======================================Order===================================
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
