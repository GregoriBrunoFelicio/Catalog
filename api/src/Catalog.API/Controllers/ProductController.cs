﻿using Catalog.API.Data;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _prodructRepository;

        public ProductController(IProductService productService, IProductRepository prodructRepository)
        {
            _productService = productService;
            _prodructRepository = prodructRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductInput input)
        {
            var product = new Product
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                CategoryId = input.CategoryId
            };

            var result = await _productService.Add(product);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductInput input)
        {
            var product = new Product
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                CategoryId = input.CategoryId
            };

            var result = await _productService.Update(product);
            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _prodructRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _prodructRepository.Get(id);
            return Ok(product);
        }

        [HttpGet("ByCategory/{id:Guid}")]
        public async Task<IActionResult> GetByCategory(Guid id)
        {
            var products = await _prodructRepository.GetByCategory(id);
            return Ok(products);
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var products = await _prodructRepository.GetByName(name);
            return Ok(products);
        }


        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _prodructRepository.Delete(id);
            return Ok(new Result("Produto removido com sucesso", true));
        }
    }
}
