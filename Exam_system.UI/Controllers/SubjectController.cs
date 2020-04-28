using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using Exam_system.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_system.UI.Controllers
{    
    public class SubjectController : Controller
    {
        private IRepository<Subject> subRepo;
        private ApplicationDbContext usersDb;
        private IEnrollable enrollable;

        public SubjectController(IRepository<Subject> repo, IEnrollable enrollable)
        {
            this.subRepo = repo;
            usersDb = new ApplicationDbContext();
            this.enrollable = enrollable;
        }

        [Authorize(Roles = "Admin, Teacher")]
        public ActionResult Index()
        {
            return View(this.subRepo.Collection().ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ModelState.Clear();
            return View(new Subject());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Subject subject)
        {
            subject.CreationDate = DateTime.Now;
            if(!ModelState.IsValid)
            {
                return View(subject);
            }            
            subRepo.Add(subject);
            subRepo.Commit();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var subject = subRepo.Find(id);
            if(subject == null)
            {
                return HttpNotFound();
            }
            subRepo.Delete(id);
            subRepo.Commit();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        private List<ApplicationUser> GetUsers(IQueryable<StudentSubjects> list)
        {
            List<ApplicationUser> l = new List<ApplicationUser>();
            foreach(var item in list)
            {
                l.Add(item.Student);
            }
            return l;
        }

        [Authorize(Roles = "Admin, Teacher")]
        public ActionResult Details(int id)
        {
            var subject = subRepo.Find(id);            
            if (subject == null)
            {
                return HttpNotFound();
            }
            var studentsSubject = usersDb.StudentsSubjects.Include("Student").Where(u => u.SubjectId == id);
            SubjectStudentsViewModel viewModel = new SubjectStudentsViewModel
            {
                Subject = subject,
                Students = GetUsers(studentsSubject)
            };
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Enroll(int id)
        {
            var subject = subRepo.Find(id);

            if(subject == null)
            {
                return HttpNotFound();
            }
            AssignSbToStsViewModel viewModel = new AssignSbToStsViewModel
            {
                Subject = subject
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Enroll(AssignSbToStsViewModel model)
        {
            #region validation code
            //int id = Convert.ToInt32(Id);
            //if (!ModelState.IsValid)
            //{                
            //    viewModel.Subject = subRepo.Find(id);
            //    return View(viewModel);
            //}
            //model.Subject = subRepo.Find(model.Subject.Id);
            #endregion                        
            bool result = enrollable.EnrollStudentsToSubject(model.EnrolledStudents, model.Subject);
            if(result)
            {
                return RedirectToAction(nameof(Index));
            }
            return Content($"<script>alert(\"Student is already enrolled in that subject\");location.replace(\"/Subject/Details?id={model.Subject.Id}\")</script>");
        }

        [Authorize(Roles = "Student")]
        public ActionResult MySubjects(string id)
        {
            var user = usersDb.Users.Find(id);

            if(user == null)
            {
                return HttpNotFound();
            }
            List<StudentSubjects> studentSubjects = usersDb.StudentsSubjects.Include("Subject").Where(s => s.StudentId == id).ToList();
            return View(studentSubjects);
        }
    }
}