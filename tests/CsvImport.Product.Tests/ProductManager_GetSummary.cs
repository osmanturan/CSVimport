using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CsvImport.Product
{
    public class ProductManager_GetSummary
    {
        [Fact]
        public async Task Should_GetSummary()
        {
            var resource = EmbededResourceHelper.Get("valid.csv");

            var productManager = MockHelper.GetProductManager(false);
            var result = await productManager.ImportCsvAsync(resource, false);

            Assert.True(result.Success);
            Assert.NotNull(result.Result);

            var summaryResult = productManager.GetSummary(result.Result);
            Assert.True(summaryResult.Success);
            Assert.NotNull(summaryResult.Result);
            Assert.Equal(10, summaryResult.Result.CreateRecordCount);
            Assert.Equal(0, summaryResult.Result.UpdateRecordCount);
            Assert.Equal(0, summaryResult.Result.DeleteRecordCount);
            Assert.Equal(62, summaryResult.Result.InvalidRecordCount);
        }

        [Fact]
        public void Should_Return_Fail_WrongOperationId()
        {
            var productManager = MockHelper.GetProductManager(false);
            var summaryResult = productManager.GetSummary(Guid.NewGuid().ToString());

            Assert.False(summaryResult.Success);
            Assert.Null(summaryResult.Result);
        }
    }
}


