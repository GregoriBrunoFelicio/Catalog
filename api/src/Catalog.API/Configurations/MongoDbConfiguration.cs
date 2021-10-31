using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Catalog.API.Configurations
{
    public static class MongoDbConfiguration
    {
        public static void AddMongoDbConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IMongoClient, MongoClient>(_ =>
            {
                var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("NoSqlConnection"));
                return new MongoClient(settings);
            });

            service.AddScoped(provider =>
            {
                var database = configuration.GetSection("NoSqlDatabaseName").Value;
                var client = provider.GetService<IMongoClient>();
                return client?.GetDatabase(database);
            });
        }
    }
}
