using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            var result = await _categoryService.Add(category);
            return result.Success
                ? Ok(result.Message)
                : BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Category category)
        {
            var result = await _categoryService.Update(category);
            return result.Success
                ? Ok(result.Message)
                : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryRepository.Get(id);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.Delete(id);
            return Ok();
        }
    }
}
