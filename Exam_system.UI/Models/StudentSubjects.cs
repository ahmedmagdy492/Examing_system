using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class StudentSubjects
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        [ForeignKey("Subject")]
        [Key]
        [Column(Order = 1)]
        public int SubjectId { get; set; }  
        public float Grade { get; set; }
        public ApplicationUser Student { get; set; }
        public Subject Subject { get; set; }
    }
}