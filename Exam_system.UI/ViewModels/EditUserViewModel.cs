using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_system.UI.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<RoleViewModel> {
                    new RoleViewModel { Name = "Admin" },
                    new RoleViewModel { Name = "Student" },
                    new RoleViewModel { Name = "Teacher" }
            };
        }

        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(3), MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Role { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}