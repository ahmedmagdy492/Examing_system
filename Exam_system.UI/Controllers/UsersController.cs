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
            AddUserViewModel addUserViewModel = new AddUserViewModel();            
            return View(addUserViewModel);
        }

        [HttpPost]
        public ActionResult Create(AddUserViewModel addUserViewModel, HttpPostedFileBase img)
        {
            if(!ModelState.IsValid)
            {                
                return View(addUserViewModel);
            }
            string fileName = Guid.NewGuid().ToString("N") + System.IO.Path.GetFileName(img.FileName);
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

        public ActionResult Edit(string id)
        {
            var user = userDb.Users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            EditUserViewModel userViewModel = new EditUserViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Name = user.Name,
                Role = user.Role,
                Id = id                
            };
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel userViewModel, HttpPostedFileBase img)
        {                        
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            var user = userDb.Users.Find(userViewModel.Id);
            if (img != null)
            {
                string fileName = Guid.NewGuid().ToString("N") + System.IO.Path.GetFileName(img.FileName);
                string filePath = Server.MapPath("~/Content/imgs");
                string fullPath = System.IO.Path.Combine(filePath, fileName);
                user.Imgurl = fileName;
                img.SaveAs(fullPath);
            }
            user.Imgurl = user.Imgurl;
            user.Name = userViewModel.Name;
            user.PhoneNumber = userViewModel.PhoneNumber;
            user.Email = userViewModel.Email;
            user.UserName = userViewModel.UserName;
            user.Role = userViewModel.Role;
            userManager.AddToRole(user.Id, userViewModel.Role);
            userDb.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}