using Fiorello_PB101.Models;
using Fiorello_PB101.Services;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IBlogService _blogDetailService;

        public BlogDetailController(IBlogService blogDetailService)
        {
            _blogDetailService = blogDetailService;
        } 
        public async Task<IActionResult> Index(int? id)
        {
            if (id == 0) return BadRequest();
            Blog blog = await _blogDetailService.GetByIdAsync(id);
            if (blog == null) return NotFound();


            return View(blog);
        }
    }
}
