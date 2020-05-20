using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using Exam_system.UI.ViewModels;
using Microsoft.AspNet.Identity;
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
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            ListExamViewModel model = new ListExamViewModel();            
            return View(model);
        }

        public ActionResult Create()
        {
            AddExamViewModel viewModel = new AddExamViewModel
            {
                ExamName = string.Empty,
                Subject = string.Empty
            };
            return View(viewModel);
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
                SubjectId = Convert.ToInt32(model.Subject),
                IsAvailable = false
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
            List<ExamQuestions> questions = db.ExamQuestions.Include("Question").Where(e => e.ExamId == id).ToList();
            return View(questions);
        }

        public ActionResult SubjectsExams()
        {
            string studentId = User.Identity.GetUserId();
            // get the current students subjects
            var stSubjects = db.StudentsSubjects.Where(s => s.StudentId == studentId).ToList();
            // get the exams of each subject
            List<Exam> exams = new List<Exam>();

            foreach(var subject in stSubjects)
            {
                foreach(var exam in db.Exams.Include("Subject").Where(e => e.SubjectId == subject.SubjectId && e.IsAvailable == true))
                {
                    exams.Add(exam);
                }
            }
            return View(exams);
        }

        public ActionResult Avail(int id)
        {
            var exam = examRepo.Find(id);
            if(exam == null)
            {
                return HttpNotFound();
            }
            exam.IsAvailable = !exam.IsAvailable.Value;
            examRepo.Commit();
            return Content("Done");
        }

        public ActionResult Filter(int id)
        {
            var subject = db.Subjects.Find(id);

            if(subject == null)
            {
                return HttpNotFound();
            }
            var exams = db.Exams.Where(e => e.SubjectId == subject.Id).ToList();
            return PartialView("_Filter", exams);
        }

        public ActionResult MyExam()
        {
            var userId = User.Identity.GetUserId();
            var examsTaken = db.StudentExams.Include("Student").Include("Exam").Where(e => e.Student.Id == userId);
            return View(examsTaken.ToList());
        }

    }
}
