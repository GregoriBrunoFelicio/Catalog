using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface IProdructRepository : IRepository<Product>
    {
    }

    public class ProductRepository : Repository<Product>, IProdructRepository
    {
        public ProductRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
