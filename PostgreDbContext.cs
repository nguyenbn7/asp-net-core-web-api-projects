using Microsoft.EntityFrameworkCore;

namespace GenericRepository;

public sealed class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : ApplicationDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
