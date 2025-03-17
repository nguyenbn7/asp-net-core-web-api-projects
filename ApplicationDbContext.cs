using Microsoft.EntityFrameworkCore;

namespace GenericRepository;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
}
