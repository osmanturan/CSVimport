using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public class ProductFamily : Entity
    {
        public string Code { get; set; }
        public string Q1 { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
