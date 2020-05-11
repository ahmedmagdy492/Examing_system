using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class ExamQuestionAnswersViewModel
    {
        public Exam Exam { get; set; }
        public Dictionary<Question, List<Answer>> QuestionWithAnswers { get; set; }
        public ApplicationUser User { get; set; }
    }
}