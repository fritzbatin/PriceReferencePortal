using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PriceReferencePortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "GetOrder", 
                "Order/GetOrder/{orderId}",
                new { 
                    Controller = "Order", 
                    action = "GetOrder", 
                    id = UrlParameter.Optional});
            routes.MapRoute(
                "order_updateGetOrder",
                "Order/order_updateGetOrder/{id}", 
                new { 
                    Controller = "Order", 
                    action = "order_updateGetOrder", 
                    id = UrlParameter.Optional });

            routes.MapRoute(
                "UpdateOrder",
                "Order/UpdateOrder/{id}",
                new
                {
                    Controller = "Order",
                    action = "UpdateOrder",
                    id = UrlParameter.Optional
                });


            routes.MapRoute(
                "DeleteOrder",
                "Order/DeleteOrder/{id}",
                new
                {
                    Controller = "Order",
                    action = "DeleteOrder",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                 defaults: new
                 {
                     controller = "Account",
                     action = "Login",
                     id = UrlParameter.Optional
                 }
            );
        }
    }
}
