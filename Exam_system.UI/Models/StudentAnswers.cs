using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Exam_system.UI.Models
{
    public class StudentAnswers : BaseModel
    {
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public ApplicationUser Student { get; set; }
        public Question Question { get; set; }
        public Exam Exam { get; set; }
    }
}