using CsvImport.Data;
using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product.Repositories
{
    public class ProductFamilyRepository : RepositoryBase<ProductDbContext>, IProductFamilyRepository
    {
        public ProductFamilyRepository(IUnitOfWork<ProductDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(ProductFamily productFamily)
        {
            UnitOfWork.Context.Add(productFamily);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productFamilyToDelete = await UnitOfWork.Context.ProductFamilies.SingleOrDefaultAsync(x => x.Id == id);
            if (productFamilyToDelete == null)
                return;

            UnitOfWork.Context.Remove(productFamilyToDelete);
        }

        public async Task<ProductFamily> FindByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productFamily = await UnitOfWork.Context.ProductFamilies
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            return productFamily;
        }

        public async Task<IEnumerable<ProductFamily>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var productFamilies = await UnitOfWork.Context.ProductFamilies.ToListAsync(cancellationToken);
            return productFamilies;
        }

        public void Update(ProductFamily productFamily)
        {
            UnitOfWork.Context.Attach(productFamily);
        }
    }
}
