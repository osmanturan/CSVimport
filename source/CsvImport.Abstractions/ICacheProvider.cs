using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport
{
    public interface ICacheProvider
    {
        bool TryGet<T>(string key, out T obj);
        void Add<T>(string key, T obj, TimeSpan cacheDuration);
        void Delete<T>(string key);
    }
}
