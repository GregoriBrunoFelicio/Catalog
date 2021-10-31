﻿using Catalog.API.Data;
using Catalog.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Shared
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IProdructRepository, ProductRepository>();

            service.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
