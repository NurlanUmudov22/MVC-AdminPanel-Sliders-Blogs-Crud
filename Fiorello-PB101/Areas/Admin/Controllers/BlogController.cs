using Fiorello_PB101.Data;
using Fiorello_PB101.Extensions;
using Fiorello_PB101.Models;
using Fiorello_PB101.Services;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Blog;
using Fiorello_PB101.ViewModels.Categories;
using Fiorello_PB101.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public BlogController(IBlogService blogService, AppDbContext context, IWebHostEnvironment env)
        {
            _blogService = blogService;
            _context = context;
            _env = env;
        }


        [HttpGet]

        public async Task<IActionResult> Index()
        {
            return View( await _blogService.GetAllAsync());
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            bool existBlog = await _blogService.ExistAsync(blog.Title, blog.Description);

            if (existBlog)
            {
                ModelState.AddModelError("Title", "This blog already exist");
                return View();
            }
            if (existBlog)
            {
                ModelState.AddModelError("Description", "This blog already exist");
                return View();
            }


            if (!blog.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Image can accept only image format");
                return View();
            }

            if (!(blog.Image.CheckFileSize(200)))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + blog.Image.FileName;

            string path = _env.GeneratedFilePath("img", fileName);


          

            await blog.Image.SaveFileToLocalAsync(path);

            await _blogService.CreateAsync(new Blog
            {
                Title = blog.Title,
                Description = blog.Description,
                Image = fileName
            }) ;
            return RedirectToAction("Index");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var blog = await _blogService.GetByIdAsync(id);

            if (blog == null) return NotFound();

            string path = _env.GeneratedFilePath("img", blog.Image);


            path.DeleteFileFromLocal();


            await _blogService.DeleteAsync(blog);

            return RedirectToAction("Index");

        }


    

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            Blog blogs = await _blogService.GetByIdAsync(id);

            return View(blogs);
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var blog = await _blogService.GetByIdAsync(id);

            if (blog == null) return NotFound();

            return View(new BlogEditVM { 
                Title = blog.Title,
                Description = blog.Description,
                Image = blog.Image }
            );

        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }


            if (id == null) return BadRequest();

            var blog = await _blogService.GetByIdAsync(id);

            if (blog == null) return NotFound();

            if (request.NewImage == null) return RedirectToAction("Index");

            if (await _blogService.ExistExceptByIdAsync((int)id, request.Title))
            {
                ModelState.AddModelError("Title", "This category already exist");
                return View();
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Image can accept only image format");
                request.Image = blog.Image;
                return View(request);
            }

            if (!(request.NewImage.CheckFileSize(200)))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                request.Image = blog.Image;
                return View(request);
            }

            if (blog.Title == request.Title)
            {
                return RedirectToAction("Index");
            }


            string oldPath = _env.GeneratedFilePath("img", blog.Image);

            oldPath.DeleteFileFromLocal();

            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string newPath = _env.GeneratedFilePath("img", fileName);

            await request.NewImage.SaveFileToLocalAsync(newPath);

            blog.Image = fileName;

            blog.Title = request.Title;

            blog.Description = request.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
           

        }



        //return View(new BlogEditVM { 
        //    Title = blog.Title ,
        //    Description = blog.Description ,
        //    Image = blog.Image 
        //});








    }
}
