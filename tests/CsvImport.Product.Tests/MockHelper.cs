using CsvImport.Core;
using CsvImport.Product.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product
{
    public static class MockHelper
    {
        public static ProductManager GetProductManager(bool mockCache = true)
        {
            ProductManager productManager;
            if (mockCache)
            {
                productManager = new ProductManager(
                    GetProductFamilyRepositoryMock().Object,
                    GetProductRepositoryMock().Object,
                    GetCsvCacheMock().Object,
                    GetCsvErrorCacheMock().Object);

            }
            else
            {
                productManager = new ProductManager(
                    GetProductFamilyRepositoryMock().Object,
                    GetProductRepositoryMock().Object,
                    GetCsvCache(),
                    GetCsvErrorCache());
            }

            return productManager;
        }

        public static Mock<IProductFamilyRepository> GetProductFamilyRepositoryMock()
        {
            var mock = new Mock<IProductFamilyRepository>();

            mock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns((Guid id, CancellationToken cancellationToken) => { return Task.FromResult(new ProductFamily()); });
            mock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns((CancellationToken cancellationToken) => { return Task.FromResult((IEnumerable<ProductFamily>)new List<ProductFamily>()); });
            mock.Setup(r => r.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns((CancellationToken cancellationToken) => { return Task.FromResult(Attempt<int>.Succeed()); });

            return mock;
        }

        public static Mock<IProductRepository> GetProductRepositoryMock()
        {
            var mock = new Mock<IProductRepository>();

            mock.Setup(r => r.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns((Guid id, CancellationToken cancellationToken) => { return Task.FromResult(new Product()); });
            mock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns((CancellationToken cancellationToken) => { return Task.FromResult((IEnumerable<Product>)new List<Product>()); });
            mock.Setup(r => r.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns((CancellationToken cancellationToken) => { return Task.FromResult(Attempt<int>.Succeed()); });
            mock.Setup(r => r.GetAllBySkusAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
                .Returns((IEnumerable<string> skus, CancellationToken cancellationToken) =>
                {
                    return Task.FromResult((IEnumerable<Product>)new List<Product>());
                });
            mock.Setup(r => r.GetAllExceptSkusAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
                .Returns((IEnumerable<string> skus, CancellationToken cancellationToken) =>
                {
                    return Task.FromResult((IEnumerable<Product>)new List<Product>());
                });

            return mock;
        }

        public static Mock<ICsvCache> GetCsvCacheMock()
        {
            var mock = new Mock<ICsvCache>();

            return mock;
        }

        public static Mock<ICsvErrorCache> GetCsvErrorCacheMock()
        {
            var mock = new Mock<ICsvErrorCache>();

            return mock;
        }

        public static ICsvCache GetCsvCache()
        {
            var memoryCacheOptions = new MemoryCacheOptions();
            var memoryCache = new MemoryCache(memoryCacheOptions);
            var memoryCacheProvider = new MemoryCacheProvider(memoryCache);

            return new CsvCache(memoryCacheProvider);
        }

        public static ICsvErrorCache GetCsvErrorCache()
        {
            var memoryCacheOptions = new MemoryCacheOptions();
            var memoryCache = new MemoryCache(memoryCacheOptions);
            var memoryCacheProvider = new MemoryCacheProvider(memoryCache);

            return new CsvErrorCache(memoryCacheProvider);
        }
    }
}


