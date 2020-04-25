using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using Exam_system.UI.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Exam_system.UI.Controllers
{
    public class ExamController : Controller
    {
        private IRepository<Exam> examRepo;
        private ApplicationDbContext db;

        public ExamController(IRepository<Exam> repository)
        {
            examRepo = repository;            
        }
        public ActionResult Index()
        {
            ListExamViewModel model = new ListExamViewModel();            
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new AddExamViewModel
            {
                ExamName = string.Empty,
                Subject = string.Empty
            });
        }

        [HttpPost]
        public ActionResult Create(AddExamViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var newExam = examRepo.Add(new Exam
            {
                Name = model.ExamName,
                SubjectId = Convert.ToInt32(model.Subject)
            });
            examRepo.Commit();
            foreach (var quest in model.Questions)
            {
                db.ExamQuestions.Add(new ExamQuestions
                {
                    ExamId = newExam.Id,
                    QuestionId = Convert.ToInt32(quest)
                });
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(int id)
        {            
            var exam = examRepo.Find(id);
            if(exam == null)
            {
                return HttpNotFound();
            }
            return View();
        }
    }
}
