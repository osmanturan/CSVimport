using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.EntityFramework
{
    public abstract class DbContextBase : DbContext, IDbContext
    {
        IDbContextTransaction _transaction;

        protected DbContextBase(DbContextOptions options) : base(options)
        {
        }
        
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateCreateDate();
            UpdateModifiedDate();
            UpdateDeletedDate();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            UpdateCreateDate();
            UpdateModifiedDate();
            UpdateDeletedDate();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
                throw new ApplicationException("No transaction to commit.");
            _transaction.Commit();
        }

        void UpdateCreateDate()
        {
            var entries = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Added)
                .Where(i => i.Entity is IEntity);

            foreach (var entry in entries)
            {
                if (((IEntity)entry.Entity).CreatedDate == DateTime.MinValue)
                    ((IEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }

        void UpdateModifiedDate()
        {
            var entries = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Modified || i.State == EntityState.Added)
                .Where(i => i.Entity is IEntity);

            foreach (var entry in entries)
            {
                ((IEntity)entry.Entity).ModifiedDate = DateTime.UtcNow;
            }
        }

        void UpdateDeletedDate()
        {
            var entries = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Deleted)
                .Where(i => i.Entity is IEntity);

            foreach (var entry in entries)
            {
                // change state to modified, since we don't actually want to delete the entity
                entry.State = EntityState.Modified;
                ((IEntity)entry.Entity).DeletedDate = DateTime.UtcNow;
            }
        }
    }
}
