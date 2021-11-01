using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public static class ServiceProviderExtensions
    {
        public static void InitializeDatabase(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ProductDbContext>();
            context.Database.Migrate();
        }
    }
}
