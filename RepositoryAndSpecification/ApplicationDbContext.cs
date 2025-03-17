using Microsoft.EntityFrameworkCore;

namespace Demo;

public abstract class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
}
