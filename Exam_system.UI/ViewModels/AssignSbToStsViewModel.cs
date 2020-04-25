using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class AssignSbToStsViewModel
    {        
        public List<ApplicationUser> Students { get; private set; }
        [Required]
        [DisplayName("Enrolled Student")]
        public List<string> EnrolledStudents { get; set; }
        [Required]
        public Subject Subject { get; set; }

        public AssignSbToStsViewModel()
        {
            var db = new ApplicationDbContext();
            Students = db.Users.Where(u => u.Role == "Student").ToList();           
        }
    }
}