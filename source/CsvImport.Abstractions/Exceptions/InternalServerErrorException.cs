using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CsvImport.Exceptions
{
    public class InternalServerErrorException : HttpException
    {
        public InternalServerErrorException()
            : base(HttpStatusCode.InternalServerError, "Internal server error.")
        {
        }

        public InternalServerErrorException(Exception innerException)
            : base(HttpStatusCode.InternalServerError, "Internal server error.", innerException)
        {
        }

        public InternalServerErrorException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public InternalServerErrorException(string message, Exception innerException)
            : base(HttpStatusCode.InternalServerError, message, innerException)
        {
        }
    }
}
