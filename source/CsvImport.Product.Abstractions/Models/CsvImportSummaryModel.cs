using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product.Models
{
    public class CsvImportSummaryModel
    {
        public string OperationId { get; set; }
        public int InvalidRecordCount { get; set; }
        public int CreateRecordCount { get; set; }
        public int UpdateRecordCount { get; set; }
        public int DeleteRecordCount { get; set; }

    }
}
