﻿using Fiorello_PB101.Data;
using Fiorello_PB101.Models;
using Fiorello_PB101.Services;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;

        public CategoryController(ICategoryService categoryService,
                                  AppDbContext context)
        {
            _context = context;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllWithProductCountAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if(!ModelState .IsValid)
            {
                return View();
            }


            bool existCategory = await _categoryService.ExistAsync(category.Name);

            if(existCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }


            await _categoryService.CreateAsync(new Category { Name = category.Name });
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetByIdAsync((int)id);
            if (category == null) return NotFound();

            await _categoryService.DeleteAsync(category);

            return RedirectToAction("Index");

        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            Category categories = await _categoryService.GetByIdAsync(id);

            return View(categories);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category == null) return NotFound();
      
            return View(new CategoryEditVM { Name = category.Name });
        }





        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, CategoryEditVM  request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();

            if(await _categoryService.ExistExceptByIdAsync((int)id, request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }

            //bool isExist = await _categoryService.ExistAsync(request.Name);

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category == null) return NotFound();


            if (category.Name == request.Name)
            {
                return RedirectToAction("Index");       
            }

            category.Name = request.Name;

            await _context.SaveChangesAsync();

            return View(new CategoryEditVM { Name = category.Name });
        }


    }
}
