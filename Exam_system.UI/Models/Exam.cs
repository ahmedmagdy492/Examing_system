using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class Exam : BaseModel
    {        
        [Required]
        public string Name { get; set; }
        [ForeignKey("Subject")]
        [DisplayName("Subject")]
        public int SubjectId { get; set; }
        public bool? IsAvailable { get; set; }
        public double? ExamTime { get; set; } = 1;
        public Subject Subject { get; set; }
        public List<StudentExams> StudentExams { get; set; }
        public List<StudentAnswers> StudentAnswers { get; set; }
        public List<ExamQuestions> ExamQuestions { get; set; }
    }
}