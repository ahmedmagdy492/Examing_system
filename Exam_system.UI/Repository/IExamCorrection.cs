using Exam_system.UI.Models;
using Exam_system.UI.ViewModels;
using System.Collections.Generic;

namespace Exam_system.UI.Repository
{
    public interface IExamCorrection
    {
        void Commit();
        List<StudentAnswers> CorrectExam(StudentAnswerViewModel[] stAnswers, int examId, string stId);
        void SetGrade(int examId, string stId, List<StudentAnswers> studentAnswers);
    }
}