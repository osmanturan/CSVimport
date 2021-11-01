using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product.Models
{
    public class CsvModel
    {
        public string Key { get; set; }
        public string ArtikelCode { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public string DeliveredIn { get; set; }
        public string Q1 { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
    }
}
