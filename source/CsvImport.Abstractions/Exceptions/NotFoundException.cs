using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CsvImport.Exceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(string message)
            : base(HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(HttpStatusCode.NotFound, message, innerException)
        {
        }
    }
}
