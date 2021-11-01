using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CsvImport.Product
{
    public class ProductManager_SaveImport
    {
        [Fact]
        public async Task Should_Save()
        {
            var resource = EmbededResourceHelper.Get("valid.csv");

            var productManager = MockHelper.GetProductManager(false);
            var result = await productManager.ImportCsvAsync(resource, false);

            Assert.True(result.Success);
            Assert.NotNull(result.Result);

            var saveResult = await productManager.SaveImportAsync(result.Result);
            Assert.True(saveResult.Success);
            Assert.NotNull(saveResult.Result);
        }

        [Fact]
        public async Task Should_Return_Fail_WrongOperationId()
        {
            var productManager = MockHelper.GetProductManager(false);

            var saveResult = await productManager.SaveImportAsync(Guid.NewGuid().ToString());
            Assert.False(saveResult.Success);
            Assert.Null(saveResult.Result);
        }
    }
}


