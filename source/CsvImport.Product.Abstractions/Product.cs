using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product
{
    public class Product : Entity
    {
        public Guid ProductFamilyId { get; set; }
        public virtual ProductFamily ProductFamily { get; set; }
        public string Key { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public virtual IList<ProductDetail> ProductDetails { get; set; }
    }
}
