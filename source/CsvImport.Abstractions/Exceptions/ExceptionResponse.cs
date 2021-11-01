using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.Exceptions
{
    public class ExceptionResponse
    {
        public string Error { get; set; }
        public string ErrorMessage { get; set; }
        public object Result { get; set; }
        public int StatusCode { get; set; }

        public ExceptionResponse()
        { }

        public ExceptionResponse(string error, string errorMessage, int statusCode, Exception exception = null, object result = null)
        {
            Error = error;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
            Result = result;
        }
    }
}
