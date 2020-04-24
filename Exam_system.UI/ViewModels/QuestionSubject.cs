using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class QuestionSubject
    {
        public Question Question { get; set; }
        public List<Subject> Subjects { get; set; }
        [DisplayName("Question Mark")]
        public float QuestionMark { get; set; }
    }
}