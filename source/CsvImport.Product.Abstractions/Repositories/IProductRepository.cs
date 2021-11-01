using CsvImport.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product.Repositories
{
    public interface IProductRepository : IRepository
    {
        Task<Product> FindByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<Product> FindBySkuAsync(string sku, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<Product>> GetAllBySkusAsync(IEnumerable<string> skus, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<Product>> GetAllExceptSkusAsync(IEnumerable<string> skus, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Create(Product product);
        void Update(Product product);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
