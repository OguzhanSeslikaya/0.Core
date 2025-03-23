using System.Net;

namespace _0.Core.Exceptions
{
    public partial class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
