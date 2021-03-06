﻿using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using Exam_system.UI.Repository;
using Exam_system.UI.ViewModels;
using Microsoft.AspNet.Identity;
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
        private readonly IExamCorrection examCorrection;

        public SubjectController(
            IRepository<Subject> repo, 
            IEnrollable enrollable,
            IExamCorrection examCorrection)
        {
            this.subRepo = repo;
            usersDb = new ApplicationDbContext();
            this.enrollable = enrollable;
            this.examCorrection = examCorrection;
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
                StudentSubjects = studentsSubject.ToList()
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


        [Authorize(Roles = "Student")]
        public ActionResult SubjectExam(string studentId, int examId)
        {
            var oldexam = usersDb.StudentExams.Include("Student").FirstOrDefault(e => e.Student.Id == studentId && e.ExamId == examId);
            if(oldexam == null)
            {
                // get the exam with that examId
                var exam = usersDb.Exams.FirstOrDefault(e => e.Id == examId);
                if (exam == null) return HttpNotFound();

                // get the exam questions
                List<ExamQuestions> questions = usersDb.ExamQuestions.Include("Question").Include("Question.Answers").Where(q => q.ExamId == examId).ToList();
                var model = new ExamQuestionAnswersViewModel
                {
                    Exam = exam,
                    QuestionWithAnswers = new Dictionary<Question, List<Answer>>(),
                    User = usersDb.Users.FirstOrDefault(u => u.Id == studentId)
                };

                foreach (var question in questions)
                {
                    model.QuestionWithAnswers.Add(question.Question, question.Question.Answers);
                }

                // save that "this student took that exam"
                usersDb.StudentExams.Add(new StudentExams
                {
                    ExamId = examId,
                    UserId = studentId,
                    TookExamCount = oldexam == null ? 1 : oldexam.TookExamCount + 1,
                    Mark = 0,
                    TakenExamDate = DateTime.Now
                });
                usersDb.SaveChanges();

                return View(model);
            }
            else
            {
                return PartialView("_AlreadyTakenExam");
            }
        }


        [Authorize(Roles = "Student")]
        [HttpPost]
        public string FinishExam(string body, string examId)
        {
            StudentAnswerViewModel[] examAnswers = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentAnswerViewModel[]>(body);
            var currentUserId = User.Identity.GetUserId();
            List<StudentAnswers> stAnswers = examCorrection.CorrectExam(examAnswers, Convert.ToInt32(examId), currentUserId);
            examCorrection.SetGrade(Convert.ToInt32(examId), currentUserId, stAnswers);
            return $"/Subject/MySubjects?id={currentUserId}";
        }

    }
}