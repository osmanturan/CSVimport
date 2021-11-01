using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CsvImport.Exceptions
{
    public abstract class HttpException : Exception
    {
        public string Name
        {
            get { return GetName(); }
        }

        public int StatusCode { get; protected set; }

        public HttpException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = (int)statusCode;
        }

        public HttpException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = (int)statusCode;
        }

        public HttpException(int statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        protected virtual string GetName()
        {
            var name = GetType().Name;
            if (name.Contains("`")) name = name.Substring(0, name.IndexOf('`'));
            if (name.EndsWith("Exception", StringComparison.InvariantCultureIgnoreCase))
                name = name.Substring(0, name.Length - "Exception".Length);
            return name;
        }
    }
}
