using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvImport.Product.Models
{
    public class CsvOperationModel
    {
        public Guid ProductId { get; set; }
        public int Line { get; set; }
        public CsvModel CsvItem { get; set; }
        public Operation Operation { get; set; }

        public CsvOperationModel(CsvModel csvItem, int line)
        {
            CsvItem = csvItem;
            Line = line;
        }

        public CsvOperationModel(Product product, Operation operation)
        {
            ProductId = product.Id;
            CsvItem = new CsvModel
            {
                Description = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Description))?.Value,
                Q1 = product.ProductFamily.Q1,
                Color = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Color))?.Value,
                DeliveredIn = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.DeliveredIn))?.Value,
                DiscountPrice = product.DiscountPrice,
                ArtikelCode = product.ProductFamily.Code,
                Price = product.Price,
                Size = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.Size))?.Value,
                Key = product.Key,
                ColorCode = product.ProductDetails.FirstOrDefault(x => x.Key == nameof(CsvItem.ColorCode))?.Value
            };
            Operation = operation;
        }
    }
}
