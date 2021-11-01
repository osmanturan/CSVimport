using CsvImport.Product.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product
{
    public interface IProductManager
    {
        Task<Attempt<string>> ImportCsvAsync(Stream file, bool replaceAll, CancellationToken cancellationToken = default(CancellationToken));
        Task<Attempt<CsvImportSummaryModel>> SaveImportAsync(string operationId, CancellationToken cancellationToken = default(CancellationToken));
        Attempt<CsvImportSummaryModel> GetSummary(string operationId);
        Attempt<IEnumerable<CsvOperationModel>> GetValidRecords(string operationId, int page = 0, int batchSize = 50);
        Attempt<IEnumerable<CsvErrorModel>> GetInvalidRecords(string operationId, int page = 0, int batchSize = 50);

    }
}
