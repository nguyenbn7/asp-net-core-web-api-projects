using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Application.Extension;

public static class JwtSchemeConfiguration
{
    public static IServiceCollection AuthenticateUsingJwt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var tokenSettingsConfig = configuration.GetSection("JWT");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var appKey = tokenSettingsConfig["Key"];
                var appIssuer = tokenSettingsConfig["Issuer"];
                if (appKey == null)
                {
                    throw new Exception("JWT Key not found");
                }

                if (appIssuer == null)
                {
                    throw new Exception("JWT Issuer not found");
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appKey)),
                    ValidIssuer = appIssuer,
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        return services;
    }
}
