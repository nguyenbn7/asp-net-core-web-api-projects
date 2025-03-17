using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Shared;
using Ecommerce.Users.Models;

namespace Ecommerce.Application.Extension;

public static class IdentitySettings
{
    public static IServiceCollection ApplyIdentitySettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Configure more if needed
            var passwordSettingsConfig = configuration.GetSection("IdentityOptions:Password");

            // Password settings.
            options.Password.RequireDigit = passwordSettingsConfig.GetValue(
                "RequireDigit", options.Password.RequireDigit);
            options.Password.RequireLowercase = passwordSettingsConfig.GetValue(
                "RequireLowercase", options.Password.RequireLowercase);
            options.Password.RequireNonAlphanumeric = passwordSettingsConfig.GetValue(
                "RequireNonAlphanumeric", options.Password.RequireNonAlphanumeric);
            options.Password.RequireUppercase = passwordSettingsConfig.GetValue(
                "RequireUppercase", options.Password.RequireUppercase);
            options.Password.RequiredLength = passwordSettingsConfig.GetValue(
                "RequiredLength", options.Password.RequiredLength);
            options.Password.RequiredUniqueChars = passwordSettingsConfig.GetValue(
                "RequiredUniqueChars", options.Password.RequiredUniqueChars);

            var lockoutSettingsConfig = configuration.GetSection("IdentityOptions:Lockout");

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettingsConfig.GetValue("DefaultLockoutTimeSpanMinutes", options.Lockout.DefaultLockoutTimeSpan.TotalMinutes));
            options.Lockout.MaxFailedAccessAttempts = lockoutSettingsConfig.GetValue("MaxFailedAccessAttempts", options.Lockout.MaxFailedAccessAttempts);
            options.Lockout.AllowedForNewUsers = lockoutSettingsConfig.GetValue("AllowedForNewUsers", options.Lockout.AllowedForNewUsers);

            var userSettingsConfig = configuration.GetSection("IdentityOptions:User");

            // User settings.
            options.User.AllowedUserNameCharacters = userSettingsConfig.GetValue<string>("AllowedUserNameCharacters") ?? options.User.AllowedUserNameCharacters;

            options.ClaimsIdentity.UserNameClaimType = JwtRegisteredClaimNames.Name;
        });

        return services;
    }
}
