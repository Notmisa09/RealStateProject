using System.Net;
using System.Text.Json;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Presentation.API.Middlewares;

public class ErrorHandlerMiddlewares
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddlewares(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception error)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message};
           
            response.StatusCode = error switch
            {
                ExceptionsForApi e =>
                    
                    e.ErrorCode switch
                    {
                        (int)HttpStatusCode.BadRequest => (int)HttpStatusCode.BadRequest,
                        (int)HttpStatusCode.InternalServerError => (int)HttpStatusCode.InternalServerError,
                        (int)HttpStatusCode.NotFound => (int)HttpStatusCode.NotFound,
                        (int)HttpStatusCode.NoContent => (int)HttpStatusCode.NoContent,
                        _ => (int)HttpStatusCode.InternalServerError
                    },
                KeyNotFoundException e =>
                    
                    (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}