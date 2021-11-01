using CsvImport.Data;
using CsvImport.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.EntityFramework
{
    public abstract class RepositoryBase<TDbContext> : IDisposable, IRepository where TDbContext : IDbContext
    {
        public IUnitOfWork<TDbContext> UnitOfWork { get; protected set; }

        public RepositoryBase(IUnitOfWork<TDbContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public void BeginTransaction()
        {
            UnitOfWork.Context.BeginTransaction();
        }

        public void CommitTransaction()
        {
            UnitOfWork.Context.CommitTransaction();
        }

        public async Task<Attempt<int>> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await UnitOfWork.SaveAsync(cancellationToken);
                if (result > -1) return Attempt<int>.Succeed(result);
                return Attempt<int>.Fail(new DbErrorException("Changes not saved"));
            }
            catch (Exception ex)
            {
                return Attempt<int>.Fail(ex);
            }
        }
    }
}
