using CsvImport.Product.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public interface ICsvCache
    {
        void Add(string key, CsvOperationModel csvOperation);
        void Delete(string key);
        IList<CsvOperationModel> Find(string key);
    }
}
