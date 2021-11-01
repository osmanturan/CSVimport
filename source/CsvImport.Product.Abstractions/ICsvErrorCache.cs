using CsvImport.Product.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public interface ICsvErrorCache
    {
        void Add(string key, CsvErrorModel csvError);
        void Delete(string key);
        IList<CsvErrorModel> Find(string key);
    }
}
