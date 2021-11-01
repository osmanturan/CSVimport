using CsvImport.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product.Repositories
{
    public interface IProductFamilyRepository : IRepository
    {
        Task<ProductFamily> FindByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<ProductFamily>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Create(ProductFamily productFamily);
        void Update(ProductFamily productFamily);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
