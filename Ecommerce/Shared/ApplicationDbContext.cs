using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Products.Models;
using Ecommerce.Orders.Models;
using Ecommerce.Users.Models;


namespace Ecommerce.Shared;

public abstract class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderDeliveryMethod> OrderDeliveryMethods { get; set; }
    public DbSet<BillingDetail> BillingDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
               .HasMany(u => u.UserRoles)
               .WithOne(ur => ur.User)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();

        builder.Entity<ApplicationRole>()
               .HasMany(r => r.UserRoles)
               .WithOne(ur => ur.Role)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired();

        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<ApplicationRole>().ToTable("Roles");
        builder.Entity<ApplicationUserRole>().ToTable("UserRoles");

        builder.Entity<Order>(b =>
        {
            b.Property(o => o.Status).HasConversion(
                s => s.ToString(),
                x => (OrderStatus)Enum.Parse(typeof(OrderStatus), x));

            b.HasMany(o => o.OrderItems)
             .WithOne(oi => oi.Order)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<OrderItem>(b =>
        {
            b.OwnsOne(oi => oi.OrderedProduct, onb => { onb.WithOwner(); });
        });
    }
}