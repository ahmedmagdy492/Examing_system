using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class SubjectStudentsViewModel
    {
        public Subject Subject { get; set; }      
        public List<ApplicationUser> Students { get; set; }
    }
}