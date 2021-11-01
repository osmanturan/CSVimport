using CsvImport.Data;
using CsvImport.EntityFramework;
using CsvImport.Product.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProductEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<ProductDbContext>, UnitOfWork<ProductDbContext>>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductFamilyRepository, ProductFamilyRepository>();
            services.AddScoped<IProductDetailRepository, ProductDetailRepository>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProductDbContext>(options =>
                DbContextConfigurer.Configure(options, connectionString));

            
        }
    }
}
