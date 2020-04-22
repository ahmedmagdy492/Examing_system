using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_system.UI.Contracts
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> Collection();
        T Find(int id);
        T Add(T entity);
        void Delete(int id);
        void Edit(int id, T entity);
        void Commit();
    }
}
