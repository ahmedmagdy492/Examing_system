using Exam_system.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exam_system.UI.Repository
{
    public interface IPostsRepository
    {
        Task Add(Post post);
        Task Commit();
        Task Delete(Post post);
        Task<IEnumerable<Post>> GetAll();
        Task Update(Post post);
        Task<Post> Find(int id);
    }
}