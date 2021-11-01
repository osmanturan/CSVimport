using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    [Obsolete("Only used for migrations")]
    public class DoorDbConProductDbContextFactorytextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProductDbContext>();

            var connectionString = MigrationHelper.GetConfiguration().GetConnectionString("DefaultConnection");
            DbContextConfigurer.Configure(builder, connectionString);

            return new ProductDbContext(builder.Options);
        }
    }
}
