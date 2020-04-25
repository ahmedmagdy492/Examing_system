using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class Question : BaseModel
    {
        [Required]
        public string Header { get; set; }
        public string SubjectName { get; set; }
        public List<Answer> Answers { get; set; }
        public List<SubjectQuestions> SubjectQuestions { get; set; }
        public List<StudentAnswers> StudentAnswers { get; set; }
        public List<ExamQuestions> ExamQuestions { get; set; }
    }
}