using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Data
{
    public interface IRepository
    {
        void BeginTransaction();
        void CommitTransaction();
        Task<Attempt<int>> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
