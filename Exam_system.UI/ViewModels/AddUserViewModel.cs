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
    public class AddUserViewModel
    {        
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
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}