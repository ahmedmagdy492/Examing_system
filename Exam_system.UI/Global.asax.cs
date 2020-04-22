using Exam_system.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Exam_system.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Init();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Init()
        {
            var db = new ApplicationDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserName == "admin123");

            if(user == null)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var newUser = new ApplicationUser
                {
                    UserName = "admin123",
                    Email = "admin@gmail.com",
                    Name = "Ahmed Magdy"
                };
                var result = userManager.Create(newUser, "0123429320");
                if(result.Succeeded)
                {
                    roleManager.Create(new IdentityRole
                    {
                        Name = "Admin"                        
                    });
                    roleManager.Create(new IdentityRole
                    {
                        Name = "Teacher"
                    });
                    roleManager.Create(new IdentityRole
                    {
                        Name = "Student"
                    });
                    userManager.AddToRole(newUser.Id, "Admin");
                }
            }
        }
    }
}
