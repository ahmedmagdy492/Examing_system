using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class AddExamViewModel
    {
        [Required]
        [DisplayName("Exam Name")]
        public string ExamName { get; set; }
        public string Subject { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<string> Questions { get; set; }
        public AddExamViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Subjects = db.Subjects.ToList();
        }
    }
}