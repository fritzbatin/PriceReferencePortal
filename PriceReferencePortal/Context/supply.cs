//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PriceReferencePortal.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class supply
    {
        public int Id { get; set; }
        [Display(Name = "Vendor")]
        public string vendor { get; set; }
        [Display(Name = "SKU")]
        public string sku { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Price")]
        public string price { get; set; }
        [Display(Name = "Delivery lead time")]
        public string delivery_lead_time { get; set; }
        [Display(Name = "Validity")]
        public string validity { get; set; }
        [Display(Name = "Date update")]
        public string date_created { get; set; }
        [Display(Name = "Created by")]
        public string created_by { get; set; }

        [Display(Name = "Order number")]
        public int ordernumber { get; set; }
        [Display(Name = "Supply ID")]
        public int supplyId { get; set; }

        [Display(Name = "Qty")]
        public string qty { get; set; }

        [Display(Name = "Total Amount")]
        public string totalAmount { get; set; }

    }
  
}
