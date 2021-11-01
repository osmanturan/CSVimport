using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProduct(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICsvCache, CsvCache>();
            services.AddScoped<ICsvErrorCache, CsvErrorCache>();
        }
    }
}
