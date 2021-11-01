using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Exceptions
{
    public class InvalidCsvException : BadRequestException
    {
        public InvalidCsvException()
        {
        }

        public InvalidCsvException(string message) : base(message)
        {
        }

        public InvalidCsvException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
