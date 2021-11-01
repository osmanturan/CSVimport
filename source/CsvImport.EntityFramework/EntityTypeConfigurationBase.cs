using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.EntityFramework
{
    public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
            {
                builder.HasKey(e => ((IEntity)e).Id);
            }

            if (typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
            {
                builder.HasQueryFilter(e => ((IEntity)e).DeletedDate == null);
                builder.HasIndex(e => ((IEntity)e).DeletedDate);
            }
        }
    }
}
