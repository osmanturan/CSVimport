using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Product.Models
{
    public class CsvErrorModel
    {
        public int Line { get; set; }
        public IList<string> Messages { get; set; }

        public CsvErrorModel(int line, IList<string> messages)
        {
            Line = line;
            Messages = messages;
        }

        public CsvErrorModel(int line, string message)
        {
            Line = line;

            if (Messages == null)
                Messages = new List<string>();
            Messages.Add(message);
        }
    }
}
