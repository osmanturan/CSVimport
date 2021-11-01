using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvImport.Product.Models
{
    public static class CsvOperationModelExtensions
    {
        public static Product ToProduct(this CsvOperationModel csvOperationModel, Product product = null)
        {
            return csvOperationModel.ToProduct(product.ProductFamily, product);
        }

        public static Product ToProduct(this CsvOperationModel csvOperationModel, ProductFamily productFamily = null, Product product = null)
        {
            if (product == null)
            {
                product = new Product();
                product.ProductDetails = new List<ProductDetail>();
            }

            product.Price = csvOperationModel.CsvItem.Price;
            product.DiscountPrice = csvOperationModel.CsvItem.DiscountPrice;
            product.Key = csvOperationModel.CsvItem.Key;

            if (productFamily != null)
                product.ProductFamily = productFamily;
            else
            {
                product.ProductFamily = new ProductFamily
                {
                    Q1 = csvOperationModel.CsvItem.Q1,
                    Code = csvOperationModel.CsvItem.ArtikelCode
                };
            }

            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Description), csvOperationModel.CsvItem.Description);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Color), csvOperationModel.CsvItem.Color);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.DeliveredIn), csvOperationModel.CsvItem.DeliveredIn);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.Size), csvOperationModel.CsvItem.Size);
            UpdateDetails(product, nameof(csvOperationModel.CsvItem.ColorCode), csvOperationModel.CsvItem.ColorCode);

            return product;
        }

        private static void UpdateDetails(Product product, string key, string value)
        {
            var existingDetail = product.ProductDetails.FirstOrDefault(pd => pd.Key == key);
            if (!string.IsNullOrEmpty(value))
            {
                if (existingDetail != null)
                    existingDetail.Value = value;
                else
                    product.ProductDetails.Add(new ProductDetail { Key = key, Value = value });
            }
            else if (existingDetail != null)
            {
                product.ProductDetails.Remove(existingDetail);
            }
        }

    }
}
