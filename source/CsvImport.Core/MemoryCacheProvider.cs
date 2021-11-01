using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Core
{
    public class MemoryCacheProvider : ICacheProvider
    {
        readonly IMemoryCache _cache;
        readonly object _cacheLock = new object();

        public MemoryCacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        string GetFullKey<T>(string key)
        {
            var cacheKey = typeof(T).FullName + "-" + key;
            return cacheKey;
        }

        public void Add<T>(string key, T obj, TimeSpan cacheDuration)
        {
            if (obj == null) return;

            lock (_cacheLock)
            {
                var cacheKey = GetFullKey<T>(key);
                _cache.Remove(cacheKey);

                var options = new MemoryCacheEntryOptions();
                options.AbsoluteExpirationRelativeToNow = cacheDuration;

                _cache.Set(cacheKey, obj, options);
            }
        }

        public void Delete<T>(string key)
        {
            lock (_cacheLock)
            {
                var cacheKey = GetFullKey<T>(key);
                _cache.Remove(cacheKey);
            }
        }

        public bool TryGet<T>(string key, out T obj)
        {
            var cacheKey = GetFullKey<T>(key);
            return (_cache.TryGetValue<T>(cacheKey, out obj));
        }
    }
}
