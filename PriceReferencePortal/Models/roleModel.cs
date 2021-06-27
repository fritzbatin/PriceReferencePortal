using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PriceReferencePortal.Models
{
    public enum myRole
    {
        roleName
    }
    public class roleModel
    {
        public myRole myRoles { get; set; }

        public static IEnumerable<SelectListItem> GetMyRole()
        {
            yield return new SelectListItem { Text = "roleName", Value = "Admin" };
            yield return new SelectListItem { Text = "roleName", Value = "Purchasing" };
        }
    }

    
}