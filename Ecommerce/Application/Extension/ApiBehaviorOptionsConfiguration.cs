using Ecommerce.Shared.Error;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Application.Extension;

public static class ApiBehaviorOptionsConfiguration
{
    public static IServiceCollection ApplyApiBehaviorOptionsSettings(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value?.Errors ?? [])
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                var errorResponse = new ValidationError<string>
                {
                    Code = ErrorCodes.VALIDATION_ERROR,
                    Message = "One or more field not meet requirements",
                    Details = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}
