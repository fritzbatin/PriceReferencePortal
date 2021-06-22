using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PriceReferencePortal.Models;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

[assembly: OwinStartupAttribute(typeof(PriceReferencePortal.Startup))]
namespace PriceReferencePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers(); 
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   
                
                var user = new ApplicationUser();
                user.UserName = "admin@email.com";
                user.Email = "admin@email.com";
                user.FirstName = "Admin";
                user.LastName = "admin";
                string userPWD = "Qwe!23";

                IdentityResult chkUser = UserManager.Create(user, userPWD);
                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Purchasing role     
            if (!roleManager.RoleExists("Purchasing"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Purchasing";
                roleManager.Create(role);

            }

            // creating Creating Logistic role     
            if (!roleManager.RoleExists("Logistic"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Logistic";
                roleManager.Create(role);

            }

            // creating Creating Accounting role     
            if (!roleManager.RoleExists("Accounting"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Accounting";
                roleManager.Create(role);

            }

            // creating Creating Requestor role     
            if (!roleManager.RoleExists("Requestor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Requestor";
                roleManager.Create(role);

            }
        }
    }

   

}
