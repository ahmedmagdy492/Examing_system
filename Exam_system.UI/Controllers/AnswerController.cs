using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using Exam_system.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace Exam_system.UI.Controllers
{
    public class AnswerController : Controller
    {
        private IRepository<Question> questDb;
        private IincludeNavigation<Answer> ansDb;

        public AnswerController(IRepository<Question> repository, IincludeNavigation<Answer> ansRepo)
        {
            questDb = repository;
            ansDb = ansRepo;
        }
        public ActionResult Create()
        {
            QuestionAnswersViewModel model = new QuestionAnswersViewModel
            {
                Answer = new Models.Answer(),
                Questions = questDb.Collection().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(QuestionAnswersViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(new QuestionAnswersViewModel
                {
                    Answer = model.Answer,
                    Questions = questDb.Collection().ToList()
                });
            }
            ansDb.Add(model.Answer);
            ansDb.Commit();
            return PartialView("_Answers", ansDb.CollectionInclude().ToList());
        }

        public ActionResult AnswersOf(string id)
        {
            int Id = Convert.ToInt32(id);
            var questions = ansDb.Collection().Where(a => a.QuestionId == Id);
            return PartialView("_AnswersOf", questions.ToList());
        }
    }
}
