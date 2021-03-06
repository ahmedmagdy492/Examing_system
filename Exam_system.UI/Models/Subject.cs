﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Models
{
    public class Subject : BaseModel
    {        
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<StudentSubjects> Students { get; set; }
        public List<Exam> Exams { get; set; }
        public List<SubjectQuestions> SubjectQuestions { get; set; }
        public List<StudentAnswers> StudentAnswers { get; set; }
    }
}