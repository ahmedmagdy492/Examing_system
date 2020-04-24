using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Repository
{
    public class AnswerRepository : IincludeNavigation<Answer>
    {
        private ApplicationDbContext db;
        private DbSet<Answer> answers;

        public AnswerRepository(ApplicationDbContext _db)
        {
            db = _db;
            answers = db.Set<Answer>();
        }
        public Answer Add(Answer entity)
        {
            return answers.Add(entity);
        }

        public IQueryable<Answer> Collection()
        {
            return db.Answers;
        }

        public IQueryable<Answer> CollectionInclude()
        {
            return db.Answers.Include("Question");
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, Answer entity)
        {
            throw new NotImplementedException();
        }

        public Answer Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}