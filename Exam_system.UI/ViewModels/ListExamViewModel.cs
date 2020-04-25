using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class ListExamViewModel
    {
        public List<Exam> Exams { get; set; }
        public List<Subject> Subjects { get; set; }

        public ListExamViewModel()
        {
            var db = new ApplicationDbContext();
            Exams = db.Exams.Include("Subject").ToList();
            Subjects = db.Subjects.ToList();
        }
    }
}