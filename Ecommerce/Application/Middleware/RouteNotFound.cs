using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Ecommerce.Shared.Error;

namespace Ecommerce.Application.Middleware;

public class RouteNotFound(RequestDelegate next)
{
    private readonly RequestDelegate next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBody = context.Response.Body;

        try
        {
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await next(context);

            memStream.Position = 0;
            string responseBody = new StreamReader(memStream).ReadToEnd();

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound && string.IsNullOrEmpty(responseBody))
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var response = new ErrorResponse
                {
                    Code = ErrorCodes.ROUTE_NOT_FOUND,
                    Message = "Route not found"
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }

            memStream.Position = 0;
            await memStream.CopyToAsync(originalBody);
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }
}
