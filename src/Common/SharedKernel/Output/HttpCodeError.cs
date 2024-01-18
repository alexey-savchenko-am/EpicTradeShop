using System.Net;

namespace SharedKernel.Output;

public class HttpCodeError
    : Error
{
    public HttpStatusCode? StatusCode { get; }

    public HttpCodeError(string code, string message, HttpStatusCode statusCode)
        : base(code, message)   
    {
        StatusCode = statusCode;
    }

    public HttpCodeError(string code, string message, HttpStatusCode statusCode, Error innerError)
        : base(code, message, innerError)
    {
        StatusCode = statusCode;
    }
}
