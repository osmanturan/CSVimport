using CsvImport.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product.Repositories
{
    public interface IProductDetailRepository : IRepository
    {
        Task<ProductDetail> FindByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<ProductDetail>> GetAllByProductIdAsync(Guid productId, CancellationToken cancellationToken = default(CancellationToken));
        void Create(ProductDetail productDetail);
        void Update(ProductDetail productDetail);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
