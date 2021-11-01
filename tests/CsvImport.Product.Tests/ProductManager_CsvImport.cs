using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CsvImport.Product
{
    public class ProductManager_CsvImport
    {
        
        [Fact]
        public async Task Should_Import()
        {
            var resource = EmbededResourceHelper.Get("valid.csv");

            var productManager = MockHelper.GetProductManager();
            var result = await productManager.ImportCsvAsync(resource, false);

            Assert.True(result.Success);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Should_NotImport_NotCsvFile()
        {
            var resource = EmbededResourceHelper.Get("notacsv.jpg");

            var productManager = MockHelper.GetProductManager();
            var result = await productManager.ImportCsvAsync(resource, false);

            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task Should_NotImport_InvalidHeader1()
        {
            var resource = EmbededResourceHelper.Get("invalid1.csv");

            var productManager = MockHelper.GetProductManager();
            var result = await productManager.ImportCsvAsync(resource, false);

            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task Should_NotImport_InvalidHeader2()
        {
            var resource = EmbededResourceHelper.Get("invalid2.csv");

            var productManager = MockHelper.GetProductManager();
            var result = await productManager.ImportCsvAsync(resource, false);

            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.NotNull(result.Exception);
        }
    }
}
