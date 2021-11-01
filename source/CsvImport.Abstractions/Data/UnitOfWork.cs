using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Data
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : IDbContext
    {
        readonly TDbContext _context;

        public UnitOfWork(TDbContext context)
        {
            _context = context;
        }

        public TDbContext Context
        {
            get
            {
                return _context;
            }
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaveAsync(true, cancellationToken);
        }

        public async Task<int> SaveAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
