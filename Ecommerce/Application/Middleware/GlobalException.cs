using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Ecommerce.Shared.Error;

namespace Ecommerce.Application.Middleware;

public class GlobalException(RequestDelegate next, IWebHostEnvironment environment)
{
    private readonly RequestDelegate next = next;
    private readonly IWebHostEnvironment environment = environment;
    private readonly char[] delimiters = ['\r', '\n'];

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // TODO: log to server
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                Code = ErrorCodes.SERVER_ERROR,
                Message = environment.IsDevelopment() ? ex.Message : "Something smoky in server operations. We will fix this",
                StackTrace = environment.IsDevelopment() ? ex.StackTrace?.Split(
                    delimiters, StringSplitOptions.RemoveEmptyEntries) : null
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
