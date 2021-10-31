using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
