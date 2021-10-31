using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface IProductRepository : IRepository<Product>
    {
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
