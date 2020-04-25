using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class ExamQuestions
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}