using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Services.Results;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Services
{

    public interface IProductService
    {
        Task<IResult> Add(Product product);
        Task<IResult> Update(Product product);
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

        public async Task<IResult> Add(Product product)
        {
            if (!await IsNameAvailable(product.Name))
                return new Result("The product name is already in use", false);

            var category = await _categoryRepository.Get(product.CategoryId);

            if (category is null)
                return new Result("Category not found", false);

            product.Category = category;

            await _prodructRepository.Add(product);

            return new Result("Product created", true);
        }

        public async Task<IResult> Update(Product product)
        {
            if (!await IsNameAvailable(product.Name))
                return new Result("The product name is already in use", false);

            var category = await _categoryRepository.Get(product.CategoryId);

            if (category is null)
                return new Result("Category not found", false);

            await _prodructRepository.Update(product);

            return new Result("Product updated", true);
        }

        private async Task<bool> IsNameAvailable(string categoryName) =>
              !(await _prodructRepository.Get(x => x.Name == categoryName)).Any();
    }
}
