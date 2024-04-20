using System.Globalization;

namespace RealStateApp.Core.Application.Exceptions;

public class ExceptionsForApi : Exception
{
    public int ErrorCode { get; set; }
    public ExceptionsForApi() : base() { }

    public ExceptionsForApi(string message) : base(message) {        
    }

    public ExceptionsForApi(string message,int errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    public ExceptionsForApi(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}