using Exam_system.UI.Models;
using Exam_system.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Repository
{
    public class ExamCorrection : IExamCorrection
    {
        private readonly ApplicationDbContext dbContext;

        public ExamCorrection(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<StudentAnswers> CorrectExam(StudentAnswerViewModel[] stAnswers, int examId, string stId)
        {
            List<StudentAnswers> studentAnswers = new List<StudentAnswers>();
            foreach (var stAns in stAnswers)
            {
                var answer = dbContext.Answers.FirstOrDefault(a => a.Id == stAns.answerId);
                StudentAnswers stAnswer = new StudentAnswers
                {
                    ExamId = examId,
                    QuestionId = stAns.questionId,
                    StudentId = stId
                };
                if (answer.IsCorrect) stAnswer.IsCorrect = true;
                else
                    stAnswer.IsCorrect = false;
                studentAnswers.Add(stAnswer);
            }
            return studentAnswers;
        }

        public void SetGrade(int examId, string stId, List<StudentAnswers> studentAnswers)
        {
            var exam = dbContext.Exams.FirstOrDefault(e => e.Id == examId);
            var subjectStudent = dbContext.StudentsSubjects.FirstOrDefault(s => s.SubjectId == exam.SubjectId && s.StudentId == stId);
            var stExam = dbContext.StudentExams.FirstOrDefault(se => se.ExamId == examId && se.UserId == stId);
            
            float grade = 0;
            foreach (var ans in studentAnswers)
            {
                var question = dbContext.SubjectQuestions.FirstOrDefault(sq => sq.QuestionId == ans.QuestionId && sq.SubjectId == exam.SubjectId);
                if(question != null && question.QuestionMark != 0)
                {
                    if (ans.IsCorrect)
                        grade += question.QuestionMark;
                }
                else
                {
                    if (ans.IsCorrect)
                        grade += 1;
                }
            }
            subjectStudent.Grade = grade;
            stExam.Mark = grade;
            dbContext.Entry(stExam).State = System.Data.Entity.EntityState.Modified;
            dbContext.Entry(subjectStudent).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}