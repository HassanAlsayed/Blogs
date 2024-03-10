using Blogs.Data;
using Blogs.Models;
using Blogs.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blogs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepo _db;

        public HomeController(ILogger<HomeController> logger,IBlogPostRepo db)
        {
            _logger = logger;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
           var Blogs = await _db.GetAllAsync();
            return View(Blogs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
