using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBaseItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class CategoryController : CustomControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return this.CreateActionResultInstance(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return this.CreateActionResultInstance(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDto category)
        {
            var response=await _categoryService.CreateAsync(category);
            return this.CreateActionResultInstance(response);
        }
    }
}
