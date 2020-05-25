using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PublishingDate { get; set; }

        [ForeignKey(nameof(Admin))]
        public string UserId { get; set; }
        public ApplicationUser Admin { get; set; }
    }
}