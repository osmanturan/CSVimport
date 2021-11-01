using CsvImport.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace CsvImport.Host.Results
{
    public class ExceptionResult : ObjectResult
    {
        public new ExceptionResponse Value { get; set; }

        protected ExceptionResult(ExceptionResponse response) : base(response)
        {
            StatusCode = response.StatusCode;
            Value = response;
        }

        public static ExceptionResult Create(Exception exception, object result = null)
        {
            var response = new ExceptionResponse(GetErrorName(exception), GetErrorMessage(exception), GetStatusCode(exception), exception, result);
            var exceptionResult = new ExceptionResult(response);
            return exceptionResult;
        }

        static string GetErrorName(Exception exception)
        {
            if (exception is HttpException)
                return ((HttpException)exception).Name;

            return "Error";
        }

        static string GetErrorMessage(Exception exception)
        {
            return exception.Message;
        }

        static int GetStatusCode(Exception exception)
        {
            if (exception is HttpException)
                return ((HttpException)exception).StatusCode;

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}