using Catalog.API.Data;
using Catalog.API.Inputs;
using Catalog.API.Models;
using Catalog.API.Services;
using Catalog.API.Services.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryRepository categoryRepository, ICategoryService categoryService)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryInput input)
        {
            var category = new Category
            {
                Id = input.Id,
                Name = input.Name
            };

            var result = await _categoryService.Add(category);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryInput input)
        {
            var category = new Category
            {
                Id = input.Id,
                Name = input.Name
            };

            var result = await _categoryService.Update(category);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await _categoryRepository.Get(id);
            return Ok(category);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryRepository.Delete(id);
            return Ok(new Result("Categoria removida com sucesso", true));
        }
    }
}
