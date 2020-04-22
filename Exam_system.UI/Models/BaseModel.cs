using Exam_system.UI.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public abstract class BaseModel : IUserRepository
    {
        [Key]
        public int Id { get; set; }
    }
}