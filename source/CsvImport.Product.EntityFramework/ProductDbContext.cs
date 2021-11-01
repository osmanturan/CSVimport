using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public class ProductDbContext : DbContextBase
    {
        public ProductDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFamily> ProductFamilies { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
    }
}
