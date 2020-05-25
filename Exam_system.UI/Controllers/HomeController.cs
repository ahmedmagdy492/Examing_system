using Exam_system.UI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Exam_system.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsRepository postsRepository;

        public HomeController(IPostsRepository postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<ActionResult> Index()
        {
            return View(await postsRepository.GetAll());
        }        
    }
}