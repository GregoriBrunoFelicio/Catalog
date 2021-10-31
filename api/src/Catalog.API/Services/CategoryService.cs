using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Services.Results;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Services
{
    public interface ICategoryService
    {
        Task<IResult> Add(Category category);
        Task<IResult> Update(Category category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) =>
            _categoryRepository = categoryRepository;

        public async Task<IResult> Add(Category category)
        {
            if (!await IsNameAvailable(category.Name))
                return new Result("The category name is already in use", false);

            await _categoryRepository.Add(category);

            return new Result("Category created", true);
        }

        public async Task<IResult> Update(Category category)
        {
            if (!await IsNameAvailable(category.Name))
                return new Result("The category name is already in use", false);

            await _categoryRepository.Update(category);

            return new Result("Category updated", true);
        }

        private async Task<bool> IsNameAvailable(string categoryName) =>
            !(await _categoryRepository.Get(x => x.Name == categoryName)).Any();
    }
}
