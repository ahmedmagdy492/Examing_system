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
    [Authorize(Roles = "Admin, Teacher")]
    public class QuestionController : Controller
    {
        private IRepository<Subject> subRepo;
        private ApplicationDbContext db;
        private IRepository<Question> questRepo;

        public QuestionController(IRepository<Subject> repository, IRepository<Question> _questRepo)
        {
            subRepo = repository;
            questRepo = _questRepo;
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View(questRepo.Collection().ToList());
        }

        public ActionResult SubjectQuestions(int id)
        {
            var subject = subRepo.Find(id);
            if(subject == null)
            {
                return HttpNotFound();
            }
            List<SubjectQuestions> SubjectQuestions = db.SubjectQuestions.Include("Subject").Include("Question").Where(q => q.SubjectId == id).ToList();
            SubjectQuestionsViewModel sbViewModel = new SubjectQuestionsViewModel
            {
                SubjectQuestions = SubjectQuestions,
                Subject = subject
            };
            return View(sbViewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new QuestionSubject
            {
                Subjects = subRepo.Collection().ToList(),
                Question = new Question()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(QuestionSubject model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            questRepo.Add(model.Question);
            questRepo.Commit();
            var subject = db.Subjects.FirstOrDefault(s => s.Name == model.Question.SubjectName);
            db.SubjectQuestions.Add(new SubjectQuestions
            {
                SubjectId = subject.Id,
                QuestionId = model.Question.Id,
                QuestionMark = model.QuestionMark
            });
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Questions(string name)
        {
            return PartialView("_QuestionSubject", questRepo.Collection().Where(q => q.SubjectName == name).ToList());
        }

    }
}