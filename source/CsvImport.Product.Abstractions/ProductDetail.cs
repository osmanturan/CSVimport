using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public class ProductDetail : Entity
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
