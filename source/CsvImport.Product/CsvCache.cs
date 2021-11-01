using System;
using System.Collections.Generic;
using System.Text;
using CsvImport.Product.Models;

namespace CsvImport.Product
{
    public class CsvCache : ICsvCache
    {
        private readonly ICacheProvider _cacheProvider;

        public CsvCache(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public void Add(string key, CsvOperationModel csvOperation)
        {
            var cachedItems = Find(key);
            if (cachedItems == null)
                cachedItems = new List<CsvOperationModel>();

            cachedItems.Add(csvOperation);
            _cacheProvider.Add(key, cachedItems, TimeSpan.FromMinutes(20));
        }

        public void Delete(string key)
        {
            _cacheProvider.Delete<IList<CsvOperationModel>>(key);
        }

        public IList<CsvOperationModel> Find(string key)
        {
            IList<CsvOperationModel> csv;
            _cacheProvider.TryGet(key, out csv);

            return csv;
        }
    }
}
