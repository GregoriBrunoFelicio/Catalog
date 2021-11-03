using Catalog.API.Models;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable> GetByCategory(Guid categoryId);
        Task<IEnumerable> GetByName(string name);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        public async Task<IEnumerable> GetByCategory(Guid categoryId)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.CategoryId, categoryId);
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable> GetByName(string name) =>
            await Collection.Find(filter => filter.Name.ToUpperInvariant().Contains(name)).ToListAsync();
    }
}
