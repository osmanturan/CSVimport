using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Data
{
    public interface IUnitOfWork<TDbContext> : IDisposable
        where TDbContext : IDbContext
    {
        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        TDbContext Context { get; }
    }
}
