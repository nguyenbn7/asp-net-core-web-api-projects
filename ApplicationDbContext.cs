using Microsoft.EntityFrameworkCore;

namespace GenericRepository;

public abstract class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
}
