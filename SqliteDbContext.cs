using Microsoft.EntityFrameworkCore;

namespace GenericRepository;

public sealed class SqliteDbContext(DbContextOptions<SqliteDbContext> options) : ApplicationDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
