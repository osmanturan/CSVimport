using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product.Configuration
{
    internal class ProductFamilyTypeConfiguration : EntityTypeConfigurationBase<ProductFamily>
    {
        public override void Configure(EntityTypeBuilder<ProductFamily> builder)
        {
            base.Configure(builder);
        }
    }
}
