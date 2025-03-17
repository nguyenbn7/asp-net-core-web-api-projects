using Ecommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application;

public sealed class SqliteDbContext(DbContextOptions<SqliteDbContext> options) : ApplicationDbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<decimal>().HaveConversion(typeof(DecimalToDouble));
    }
}