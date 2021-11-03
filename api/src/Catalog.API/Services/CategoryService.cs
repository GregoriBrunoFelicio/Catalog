using Catalog.API.Data;
using Catalog.API.Inputs;
using Catalog.API.Models;
using Catalog.API.Services.Results;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Services
{
    public interface ICategoryService
    {
        Task<IResult> Add(CreateCategoryInput input);
        Task<IResult> Update(UpdateCategoryInput input);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) =>
            _categoryRepository = categoryRepository;

        public async Task<IResult> Add(CreateCategoryInput input)
        {
            var category = new Category
            {
                Id = input.Id,
                Name = input.Name
            };

            if (!await IsNameAvailable(category.Name))
                return new Result("Nome da categoria já está em uso", false);

            await _categoryRepository.Add(category);

            return new Result("Categoria criada com sucesso", true);
        }

        public async Task<IResult> Update(UpdateCategoryInput input)
        {
            var category = new Category
            {
                Id = input.Id,
                Name = input.Name
            };

            if (!await IsNameAvailable(category.Name))
                return new Result("Nome da categoria já está em uso", false);

            await _categoryRepository.Update(category);

            return new Result("Categoria atualizada com sucesso", true);
        }

        private async Task<bool> IsNameAvailable(string categoryName) =>
            !(await _categoryRepository.Get(x => x.Name == categoryName)).Any();
    }
}
