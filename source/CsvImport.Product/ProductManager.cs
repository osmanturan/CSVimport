using CsvHelper;
using CsvHelper.Configuration;
using CsvImport.Exceptions;
using CsvImport.Product.Models;
using CsvImport.Product.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImport.Product
{
    public class ProductManager : IProductManager
    {
        private readonly IProductFamilyRepository _productFamilyRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICsvCache _csvCache;
        private readonly ICsvErrorCache _csvErrorCache;
        private readonly Configuration _csvConfiguration;

        public ProductManager(
            IProductFamilyRepository productFamilyRepository, 
            IProductRepository productRepository, 
            ICsvCache csvCache, 
            ICsvErrorCache csvErrorCache)
        {
            _productFamilyRepository = productFamilyRepository;
            _productRepository = productRepository;
            _csvCache = csvCache;
            _csvErrorCache = csvErrorCache;
            _csvConfiguration = new Configuration()
            {
                TrimOptions = TrimOptions.Trim,
                Delimiter = ",",
                DetectColumnCountChanges = true,
                HasHeaderRecord = true
            };
        }

        public async Task<Attempt<string>> ImportCsvAsync(Stream fileStream, bool replaceAll, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var operationId = Guid.NewGuid().ToString();
            var csvOperations = new List<CsvOperationModel>();

            fileStream.Position = 0;
            using (var textReader = new StreamReader(fileStream))
            {
                var csvReader = new CsvReader(textReader, _csvConfiguration);

                var headerValidated = false;
                while (csvReader.Read())
                {
                    Exception headerException;
                    if (!headerValidated)
                    {
                        if (!TryValidateCsvHeader(csvReader, out headerException))
                            return Attempt<string>.Fail(new Exception(headerException.Message));

                        headerValidated = true;
                        continue;
                    }

                    try
                    {
                        csvOperations.Add(new CsvOperationModel(csvReader.GetRecord<CsvModel>(), csvReader.Context.Row));
                    }
                    catch (Exception ex)
                    {
                        var csvError = new CsvErrorModel(csvReader.Context.CurrentIndex, new List<string>() { ex.Message });
                        _csvErrorCache.Add(operationId, csvError);
                    }
                }
            }

            if (!replaceAll)
                await ValidateAgainstExistingAsync(operationId, csvOperations);

            await SetOperationTypesAsync(replaceAll, csvOperations, cancellationToken);

            foreach (var csvOperation in csvOperations)
                _csvCache.Add(operationId, csvOperation);

            return Attempt<string>.Succeed(operationId);
        }

        public async Task<Attempt<CsvImportSummaryModel>> SaveImportAsync(string operationId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var validRecords = _csvCache.Find(operationId);
            if (validRecords == null)
                return Attempt<CsvImportSummaryModel>.Fail(new NotFoundException($"No import data has found. Please upload again."));

            var toCreate = validRecords.Where(r => r.Operation == Operation.Create);
            var toDelete = validRecords.Where(r => r.Operation == Operation.Delete);
            var toUpdate = validRecords.Where(r => r.Operation == Operation.Update);

            foreach (var record in toDelete)
                await _productRepository.DeleteAsync(record.ProductId, cancellationToken);

            var productFamilies = (await _productFamilyRepository.GetAllAsync(cancellationToken)).ToList();
            foreach (var record in toCreate)
            {
                var existingProductFamily = productFamilies.SingleOrDefault(pf => pf.Code == record.CsvItem.ArtikelCode);
                var product = record.ToProduct(existingProductFamily);
                if (existingProductFamily == null)
                    productFamilies.Add(product.ProductFamily);
                _productRepository.Create(product);
            }

            var existingProducts = await _productRepository.GetAllBySkusAsync(toUpdate.Select(co => co.CsvItem.Key), cancellationToken);
            foreach (var record in toUpdate)
            {
                var existingProduct = existingProducts.Single(pf => pf.Key == record.CsvItem.Key);
                _productRepository.Update(record.ToProduct(existingProduct));
            }

            var saveAttempt = await _productRepository.SaveAsync(cancellationToken);
            if (!saveAttempt.Success)
                return Attempt<CsvImportSummaryModel>.Fail(saveAttempt.Exception);

            var result = new CsvImportSummaryModel {
                OperationId = operationId,
                CreateRecordCount = toCreate.Count(),
                DeleteRecordCount = toDelete.Count(),
                UpdateRecordCount = toUpdate.Count()
            };
            return Attempt<CsvImportSummaryModel>.Succeed(result);
        }

        public Attempt<CsvImportSummaryModel> GetSummary(string operationId)
        {
            var validRecords = _csvCache.Find(operationId);
            var invalidRecords = _csvErrorCache.Find(operationId);
            if (validRecords == null && invalidRecords == null)
                return Attempt<CsvImportSummaryModel>.Fail(new NotFoundException($"No import data has found. Please upload again."));

            var summary = new CsvImportSummaryModel
            {
                OperationId = operationId,
                InvalidRecordCount = invalidRecords?.Count ?? 0,
                CreateRecordCount = validRecords?.Count(r => r.Operation == Operation.Create) ?? 0,
                DeleteRecordCount = validRecords?.Count(r => r.Operation == Operation.Delete) ?? 0,
                UpdateRecordCount = validRecords?.Count(r => r.Operation == Operation.Update) ?? 0,
            };

            return Attempt<CsvImportSummaryModel>.Succeed(summary);
        }

        public Attempt<IEnumerable<CsvOperationModel>> GetValidRecords(string operationKey, int page = 0, int batchSize = 50)
        {
            var records = _csvCache.Find(operationKey);
            if (records == null)
                return Attempt<IEnumerable<CsvOperationModel>>.Fail(new NotFoundException($"No import data has found. Please upload again."));

            return Attempt<IEnumerable<CsvOperationModel>>.Succeed(records.Skip(page * batchSize).Take(batchSize));
        }

        public Attempt<IEnumerable<CsvErrorModel>> GetInvalidRecords(string operationKey, int page = 0, int batchSize = 50)
        {
            var records = _csvErrorCache.Find(operationKey);
            if (records == null)
                return Attempt<IEnumerable<CsvErrorModel>>.Succeed(new List<CsvErrorModel>());

            return Attempt<IEnumerable<CsvErrorModel>>.Succeed(records.Skip(page * batchSize).Take(batchSize));
        }

        private bool TryValidateCsvHeader(CsvReader csv, out Exception exception)
        {
            exception = null;

            try
            {
                csv.ReadHeader();
                csv.ValidateHeader<CsvModel>();
            }
            catch (CsvHelper.ValidationException ex)
            {
                exception = new Exception(ex.Message.Split('.')[0]);
                return false;
            }

            if (csv.Context.HeaderRecord.Count() != typeof(CsvModel).GetProperties().Length)
                return false;

            return true;
        }


        private async Task ValidateAgainstExistingAsync(string operationId, IList<CsvOperationModel> csvOperations, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            var productFamilies = await _productFamilyRepository.GetAllAsync(cancellationToken);
            var operationsToRemove = new List<CsvOperationModel>();
            foreach (var csvOperation in csvOperations)
            {
                var productFamily = productFamilies.SingleOrDefault(pf => pf.Code == csvOperation.CsvItem.ArtikelCode);
                if (productFamily != null && productFamily.Q1 != csvOperation.CsvItem.Q1)
                {
                    var csvError = new CsvErrorModel(
                        csvOperation.Line, 
                        $"Inconsistent category with against products. Category should be {productFamily.Q1}.");
                    _csvErrorCache.Add(operationId, csvError);
                    operationsToRemove.Add(csvOperation);
                }
            }

            foreach (var csvOperation in operationsToRemove)
                csvOperations.Remove(csvOperation);
        }


        private async Task SetOperationTypesAsync(bool replaceAll, List<CsvOperationModel> csvOperations, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existingProducts = await _productRepository.GetAllBySkusAsync(csvOperations.Select(co => co.CsvItem.Key), cancellationToken);
            foreach (var csvOperation in csvOperations)
            {
                if (existingProducts.Any(x => x.Key == csvOperation.CsvItem.Key))
                {
                    csvOperation.ProductId = existingProducts.Single(x => x.Key == csvOperation.CsvItem.Key).Id;
                    csvOperation.Operation = Operation.Update;
                }
                else
                    csvOperation.Operation = Operation.Create;
            }

            if (replaceAll)
            {
                var productsToRemove = await _productRepository.GetAllExceptSkusAsync(csvOperations.Select(co => co.CsvItem.Key), cancellationToken);
                foreach (var product in productsToRemove)
                {
                    csvOperations.Add(new CsvOperationModel(product, Operation.Delete));
                }
            }
        }
    }
}
