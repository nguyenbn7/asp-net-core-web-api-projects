using Ecommerce.Orders;
using Ecommerce.Products;
using Ecommerce.Users;
using Ecommerce.Application.Extension;
using Ecommerce.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.CustomizeKestrelSettings();

builder.Services.ApplyRouteOptionsSettings();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ApplyCorsSettings(builder.Configuration, builder.Environment);

builder.Services.ApplySwaggerSettings();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ApplyDbContextSettings(builder.Environment, builder.Configuration);

builder.Services.ApplyRedisSettings(builder.Configuration);

builder.Services.ApplyIdentitySettings(builder.Configuration)
                .AuthenticateUsingJwt(builder.Configuration);

builder.Services.ApplyApiBehaviorOptionsSettings();

builder.Services.AddApplicationServices();

builder.Services.AddAuthorizationBuilder().AddPolicy("CustomerOnly", config =>
{
    var ap = config.RequireRole(["Customer"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Only for demo
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalException>();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<RouteNotFound>();

app.MapControllers();

await app.ApplyMigrationsAsync();

// TODO: add demo environment

await app.SeedProductBrandsAsync();
await app.SeedProductsAsync();
await app.SeedOrderDeliveryMethodsAsync();
await app.CreateRolesAsync();
await app.CreateDemoCustomerAsync();
await app.CreateDemoCustomer1Async();

app.Run();
