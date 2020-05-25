using Exam_system.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Exam_system.UI.Repository
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext db;

        public PostsRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await db.Posts.Include("Admin").ToListAsync();
        }

        public async Task Commit()
        {
            await db.SaveChangesAsync();
        }

        public Task Add(Post post)
        {
            db.Posts.Add(post);
            return Task.FromResult(0);
        }

        public async Task Delete(Post post)
        {
            db.Posts.Remove(post);
            db.Entry(post).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task Update(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Post> Find(int id)
        {
            return await db.Posts.FindAsync(id);
        }

    }
}