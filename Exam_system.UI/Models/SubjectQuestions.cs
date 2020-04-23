using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class SubjectQuestions
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }
        [Column(Order = 1)]
        [ForeignKey(nameof(Question))]
        [Key]
        public int QuestionId { get; set; }
        public float QuestionMark { get; set; }
        public Subject Subject { get; set; }
        public Question Question { get; set; }
    }
}