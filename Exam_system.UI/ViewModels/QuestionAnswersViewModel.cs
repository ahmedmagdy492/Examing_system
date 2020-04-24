using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class QuestionAnswersViewModel
    {
        public Answer Answer { get; set; }
        [DisplayName("Select Question")]
        public List<Question> Questions { get; set; }        
    }
}