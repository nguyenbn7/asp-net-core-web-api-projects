using Microsoft.EntityFrameworkCore;
using Ecommerce.Shared;
using Ecommerce.Products.Models;
using Ecommerce.Orders.Models;

namespace Ecommerce.Application;

public sealed class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : ApplicationDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>(b =>
        {
            b.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });

        builder.Entity<OrderItem>(b =>
        {
            b.OwnsOne(oi => oi.OrderedProduct, cb =>
            {
                cb.Property(op => op.Price).HasColumnType("decimal(18,2)");
            });
        });

        builder.Entity<OrderDeliveryMethod>(b =>
        {
            b.Property(odm => odm.Price).HasColumnType("decimal(18,2)");
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateTime>().HaveConversion(typeof(DateTimeToDateTimeUtc));
    }
}