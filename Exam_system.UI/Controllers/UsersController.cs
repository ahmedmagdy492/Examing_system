using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using Exam_system.UI.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_system.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private ApplicationDbContext userDb;
        private UserManager<ApplicationUser> userManager;

        public UsersController()
        {
            userDb = new ApplicationDbContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userDb));
        }
        // GET: Student
        public ActionResult Index()
        {
            return View(userDb.Users.ToList());
        }        

        public ActionResult Create()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel
            {                
                Roles = new List<RoleViewModel> {
                    new RoleViewModel { Name = "Admin" },
                    new RoleViewModel { Name = "Student" },
                    new RoleViewModel { Name = "Teacher" }
                }
            };
            return View(addUserViewModel);
        }

        [HttpPost]
        public ActionResult Create(AddUserViewModel addUserViewModel, HttpPostedFileBase img)
        {
            if(!ModelState.IsValid)
            {
                addUserViewModel.Roles = new List<RoleViewModel> {
                    new RoleViewModel { Name = "Admin" },
                    new RoleViewModel { Name = "Student" },
                    new RoleViewModel { Name = "Teacher" }
                };
                return View(addUserViewModel);
            }
            string fileName = Guid.NewGuid().ToString("N") + img.FileName;
            string filePath = Server.MapPath("~/Content/imgs");
            string fullPath = System.IO.Path.Combine(filePath, fileName);            
            var newUser = new ApplicationUser 
            {
                Name = addUserViewModel.Name,
                Email = addUserViewModel.Email,
                UserName = addUserViewModel.UserName,
                Role = addUserViewModel.Role,
                Imgurl = fileName,
                PhoneNumber = addUserViewModel.PhoneNumber
            };
            var result = userManager.Create(newUser, addUserViewModel.Password);

            if (result.Succeeded)
            {
                img.SaveAs(fullPath);
                userManager.AddToRole(newUser.Id, newUser.Role);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(string id)
        {
            var user = userDb.Users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var user = userDb.Users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            userDb.Users.Remove(user);
            userDb.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}