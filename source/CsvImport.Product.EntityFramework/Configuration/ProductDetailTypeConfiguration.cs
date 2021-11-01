using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product.Configuration
{
    internal class ProductDetailTypeConfiguration : EntityTypeConfigurationBase<ProductDetail>
    {
        public override void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            base.Configure(builder);
        }
    }
}
