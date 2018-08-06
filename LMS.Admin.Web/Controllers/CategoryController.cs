using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Dto;
using LMS.Interfaces;
using LMS.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;




namespace LMS.Admin.Web.Controllers
{

    public class CategoryController : Controller
    {

        private readonly CategoryService categoryService;

        private readonly IMapper dtoMapper;

        public CategoryController(CategoryService сategories, IMapper mapper)
        {
            categoryService = сategories;
            dtoMapper = mapper;
        }

        [HttpGet]
        public IActionResult List()
        {
            var сategories = categoryService.GetAll();
            return View(сategories);
        }

        [HttpGet]
        public IActionResult Create()
        {


            ViewData["AvailableCategories"] = categoryService.GetAvailableCategories()
            .Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AmountChildrenCategories"] = 0;

            return View(new CategoryDTO());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]CategoryDTO сategories)
        {
            await categoryService.CreateAsync(сategories);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = categoryService.GetById(id);

            ViewData["AvailableCategories"] = categoryService.GetAvailableCategories(id)
            .Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AmountChildrenCategories"] = categoryService.GetAmountChildrenCategories(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]CategoryDTO сategory)
        {
            await categoryService.UpdateAsync(сategory);

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteAsync(id);
            var сategories = categoryService.GetAll();
            return View("List", сategories);
        }

    }
}
