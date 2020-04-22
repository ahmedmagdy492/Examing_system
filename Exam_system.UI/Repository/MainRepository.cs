using Exam_system.UI.Contracts;
using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Exam_system.UI.Repository
{
    public class MainRepository<T> : IRepository<T> where T : BaseModel
    {
        private ApplicationDbContext db;
        private DbSet<T> Entity;
        public MainRepository(ApplicationDbContext _db)
        {
            this.db = _db;
            Entity = db.Set<T>();
        }
        public T Add(T entity)
        {
            Entity.Add(entity);
            return entity;
        }

        public IQueryable<T> Collection()
        {
            return Entity;
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Entity.Find(id);
            if (db.Entry(entity).State == EntityState.Detached)
            {
                Entity.Attach(entity);
            }
            Entity.Remove(entity);
        }

        public void Edit(int id, T entity)
        {
            var oldEntity = Entity.FirstOrDefault(e => e.Id == id);
            Entity.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }

        public T Find(int id)
        {
            return Entity.FirstOrDefault(e => e.Id == id);
        }
    }
}