using Fiorello_PB101.Data;
using Fiorello_PB101.Extensions;
using Fiorello_PB101.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public SliderController(AppDbContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _context.Sliders.Select(m=> new SliderVM { Id =m.Id ,Image = m.Image}).ToListAsync();     
            return View(sliders);
        }



        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Image can accept only image format");    
                return View();
            }

            if (!(request.Image.CheckFileSize(200)))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return View();
            }


            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = _env.GeneratedFilePath("img", fileName);


            //using(FileStream stream = new FileStream(path, FileMode.Create))
            //{
            //    await request.Image.CopyToAsync(stream);
            //}

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Sliders.AddAsync(new Models.Slider { Image = fileName });
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var deletedSlider = await _context.Sliders.FindAsync(id);

            if (deletedSlider == null) return NotFound();

            string path = _env.GeneratedFilePath("img", deletedSlider.Image);


            //if(System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}

            path.DeleteFileFromLocal();


            _context.Sliders.Remove(deletedSlider);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var slider = await _context.Sliders.FindAsync(id);

            if (slider == null) return NotFound();

            return View(new SliderEditVM { Image = slider.Image }); 

        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id == null) return BadRequest();

            var slider = await _context.Sliders.FindAsync(id);

            if (slider == null) return NotFound();

            if (request.NewImage == null) return RedirectToAction("Index");

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Image can accept only image format");
                request.Image = slider.Image;
                return View(request);
            }

            if (!(request.NewImage.CheckFileSize(200)))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200 KB");
                request.Image = slider.Image;
                return View(request);
            }


            string oldPath = _env.GeneratedFilePath("img", slider.Image);   

            oldPath.DeleteFileFromLocal();

            string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

            string newPath = _env.GeneratedFilePath("img", fileName);

            await request.NewImage.SaveFileToLocalAsync(newPath);

            slider.Image = fileName;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


    }
}
