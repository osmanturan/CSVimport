using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product.Configuration
{
    internal class ProductTypeConfiguration : EntityTypeConfigurationBase<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
        }
    }
}
