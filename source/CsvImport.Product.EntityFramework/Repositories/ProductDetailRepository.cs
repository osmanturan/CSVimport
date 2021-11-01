using CsvImport.Data;
using CsvImport.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product.Repositories
{
    public class ProductDetailRepository : RepositoryBase<ProductDbContext>, IProductDetailRepository
    {
        public ProductDetailRepository(IUnitOfWork<ProductDbContext> unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(ProductDetail productDetail)
        {
            UnitOfWork.Context.Add(productDetail);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productDetailToDelete = await UnitOfWork.Context.ProductDetails.SingleOrDefaultAsync(x => x.Id == id);
            if (productDetailToDelete == null)
                return;

            UnitOfWork.Context.Remove(productDetailToDelete);
        }

        public async Task<ProductDetail> FindByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productDetail = await UnitOfWork.Context.ProductDetails
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            return productDetail;
        }

        public async Task<IEnumerable<ProductDetail>> GetAllByProductIdAsync(Guid productId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var productDetails = await UnitOfWork.Context.ProductDetails.Where(pd => pd.ProductId == productId).ToListAsync(cancellationToken);
            return productDetails;
        }

        public void Update(ProductDetail productDetail)
        {
            UnitOfWork.Context.Attach(productDetail);
        }
    }
}
