using Fiorello_PB101.Services;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fiorello_PB101.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISliderService _sliderService;
        private readonly IBlogService _blogService;
        private readonly IExpertsService _expertsService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public HomeController(ISliderService sliderService,
                               IBlogService blogService,
                               IExpertsService expertsService,
                               ICategoryService categoryService,
                               IProductService productService)
        {
            _sliderService = sliderService;
            _blogService = blogService;
            _expertsService = expertsService;
            _categoryService = categoryService;
            _productService = productService;   
        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllAsync(),
                SliderInfo = await _sliderService.GetSliderInfoAsync(),
                Blogs = await _blogService.GetAllAsync(3),
                Experts = await _expertsService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
            };
            return View(model);  
        }
    }
}
