using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class StudentExams
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey(nameof(Student))]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
        public float Mark { get; set; }
        public ApplicationUser Student { get; set; }
        public Exam Exam { get; set; }
    }
}