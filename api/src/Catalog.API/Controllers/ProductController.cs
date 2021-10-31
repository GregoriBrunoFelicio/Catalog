using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProdructRepository _prodructRepository;

        public ProductController(IProductService productService, IProdructRepository prodructRepository)
        {
            _productService = productService;
            _prodructRepository = prodructRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var result = await _productService.Add(product);
            return result.Success
                ? Ok(result.Message)
                : BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Product product)
        {
            var result = await _productService.Update(product);
            return result.Success
                ? Ok(result.Message)
                : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _prodructRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _prodructRepository.Get(id);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _prodructRepository.Delete(id);
            return Ok("Product deleted");
        }
    }
}
