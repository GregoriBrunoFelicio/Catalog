using Catalog.API.Data;
using Catalog.API.Inputs;
using Catalog.API.Models;
using Catalog.API.Services.Results;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Services
{

    public interface IProductService
    {
        Task<IResult> Add(CreateProductInput product);
        Task<IResult> Update(UpdateProductInput product);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _prodructRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository prodructRepository,
            ICategoryRepository categoryRepository)
        {
            _prodructRepository = prodructRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IResult> Add(CreateProductInput input)
        {
            var product = new Product
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                CategoryId = input.CategoryId
            };

            if (!await IsNameAvailable(product.Name))
                return new Result("Nome do produto já está em uso", false);

            var category = await _categoryRepository.Get(product.CategoryId);

            if (category is null)
                return new Result("Categoria informada não foi encontrada", false);

            product.Category = category;

            await _prodructRepository.Add(product);

            return new Result("Produto criado com sucesso", true);
        }

        public async Task<IResult> Update(UpdateProductInput input)
        {
            var product = new Product
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                CategoryId = input.CategoryId
            };

            var productFromDb = await _prodructRepository.Get(product.Id);

            if (!await IsNameAvailable(product.Name) && (product.Name != productFromDb.Name))
                return new Result("Nome do produto já está em uso", false);

            var category = await _categoryRepository.Get(product.CategoryId);

            if (category is null)
                return new Result("Categoria informada não foi encontrada", false);

            await _prodructRepository.Update(product);

            return new Result("Produto atualizado com sucesso", true);
        }

        private async Task<bool> IsNameAvailable(string categoryName) =>
              !(await _prodructRepository.Get(x => x.Name == categoryName)).Any();
    }
}
