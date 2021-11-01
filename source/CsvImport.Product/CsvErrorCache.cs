using System;
using System.Collections.Generic;
using System.Text;
using CsvImport.Product.Models;

namespace CsvImport.Product
{
    public class CsvErrorCache : ICsvErrorCache
    {
        private readonly ICacheProvider _cacheProvider;

        public CsvErrorCache(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public void Add(string key, CsvErrorModel csvError)
        {
            var cachedItems = Find(key);
            if (cachedItems == null)
                cachedItems = new List<CsvErrorModel>();

            cachedItems.Add(csvError);
            _cacheProvider.Add(key, cachedItems, TimeSpan.FromMinutes(20));
        }

        public void Delete(string key)
        {
            _cacheProvider.Delete<IList<CsvErrorModel>>(key);
        }

        public IList<CsvErrorModel> Find(string key)
        {
            IList<CsvErrorModel> csv;
            _cacheProvider.TryGet(key, out csv);

            return csv;
        }
    }
}
