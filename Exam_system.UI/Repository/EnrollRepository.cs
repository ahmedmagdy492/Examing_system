using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Repository
{
    public class EnrollRepository : IEnrollable
    {
        private ApplicationDbContext db;

        public EnrollRepository()
        {
            db = new ApplicationDbContext();
        }
        public bool EnrollStudentsToSubject(List<string> students, Subject subject)
        {
            bool success = false;
            foreach(var student in students)
            {
                var st = db.StudentsSubjects.FirstOrDefault(s => s.StudentId == student);
                if(st == null)
                {
                    StudentSubjects sb = new StudentSubjects
                    {                        
                        StudentId = student,
                        SubjectId = subject.Id,
                        Grade = 0.0f
                    };
                    db.StudentsSubjects.Add(sb);
                    success = true;
                }
                else
                {
                    success = false;
                    break;
                }                
            }
            if (success == true)
            {
                db.SaveChanges();
            }
            return success;
        }
    }
}