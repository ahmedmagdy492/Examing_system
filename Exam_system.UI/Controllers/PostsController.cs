using Exam_system.UI.Models;
using Exam_system.UI.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exam_system.UI.Controllers
{    
    public class PostsController : Controller
    {
        private readonly IPostsRepository posts;

        public PostsController(IPostsRepository posts)
        {
            this.posts = posts;
        }

        [Authorize(Roles ="Teacher, Admin")]
        public ActionResult CreatePost()
        {
            return View(new Post());
        }

        [Authorize(Roles = "Teacher, Admin")]
        [HttpPost]
        public async Task<ActionResult> CreatePost(Post post)
        {
            post.PublishingDate = DateTime.Now;
            post.UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid) return View(post);
            await posts.Add(post);
            await posts.Commit();
            return Redirect("/Home/Index");
        }

        [Authorize(Roles = "Teacher, Admin")]
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var post = await posts.Find(id);
            if (post == null) return HttpNotFound();
            await posts.Delete(post);
            return Redirect("/Home/Index");
        }

        [Authorize(Roles = "Teacher, Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var post = await posts.Find(id);
            if (post == null) return HttpNotFound();
            return View(post);
        }

        [Authorize(Roles = "Teacher, Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Post post)
        {
            var oldPost = await posts.Find(id);
            if (oldPost == null) return HttpNotFound();
            if (!ModelState.IsValid) return View(post);

            oldPost.Content = post.Content;
            oldPost.UserId = User.Identity.GetUserId();
            oldPost.PublishingDate = DateTime.Now;
            await posts.Update(oldPost);
            return Redirect("/Home/Index");
        }
    }
}