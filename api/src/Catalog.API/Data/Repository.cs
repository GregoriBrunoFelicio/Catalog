using Catalog.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public interface IRepository<T>
    {
        Task Add(T obj);
        Task Update(T obj);
        Task Delete(Guid id);
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
    }

    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected IMongoCollection<T> Collection;

        public Repository(IMongoDatabase mongoDatabase) =>
            Collection = mongoDatabase.GetCollection<T>(typeof(T).Name);

        public async Task Add(T obj) =>
            await Collection.InsertOneAsync(obj);

        public async Task Update(T obj) =>
            await Collection.ReplaceOneAsync(o => o.Id == obj.Id, obj);

        public async Task<T> Get(Guid id) =>
            await Collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();

        public async Task Delete(Guid id) => await Collection.DeleteOneAsync(x => x.Id == id);

        public async Task<IEnumerable<T>> GetAll() =>
            await Collection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate) =>
            await Collection.Find(predicate).ToListAsync();
    }
}
