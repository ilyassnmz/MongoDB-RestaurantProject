using AkademiQMongoDb.DTOs.CategoryDtos;
using AkademiQMongoDb.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
        {
            await _categoryService.CreateAsync(categoryDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCategory(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            await _categoryService.UpdateAsync(categoryDto);
            return RedirectToAction("Index");
        }
    }
}
