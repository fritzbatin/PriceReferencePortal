//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PriceReferencePortal.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Order
    {
        [Display(Name="Order number")]
        public int Id { get; set; }
        [Display(Name = "Supply ID")]
        public string supply_ids { get; set; }

        [Display(Name = "Date Update")]
        public string date_created { get; set; }
        [Display(Name = "Created by")]
        public string created_by { get; set; }
        [Display(Name = "Status (Procurement)")]
        public string procurement_approval { get; set; }
        [Display(Name = "Date approved (Procurement)")]
        public string proc_approval_date { get; set; }
        [Display(Name = "Approved by (Procurement)")]
        public string proc_approve_by { get; set; }
        [Display(Name = "Status (Accounting)")]
        public string accounting_approval { get; set; }
        [Display(Name = "Date approved (Accounting)")]
        public string acc_approve_date { get; set; }
        [Display(Name = "Approved by (Accounting)")]
        public string acc_approve_by { get; set; }


        [Display(Name = "Terms and Condition")]
        public string terms_and_condition { get; set; }
        [Display(Name = "Total Payment")]
        public string totalPayment { get; set; }
        [Display(Name = "Delivery Status")]
        public string delivery_status { get; set; }

    }
}