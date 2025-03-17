using System.Text.Json;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

var dbUsed = string.Empty;

if (!string.IsNullOrEmpty(builder.Configuration.GetConnectionString("Sqlite")))
{
    dbUsed = "sqlite";
    builder.Services.AddDbContext<DbContext, SqliteDbContext>(optionsBuilder =>
    {
        optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    });
}

if (!string.IsNullOrEmpty(builder.Configuration.GetConnectionString("Postgre")))
{
    dbUsed = "postgre";
    builder.Services.AddDbContext<DbContext, PostgreDbContext>(optionsBuilder =>
    {
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Postgre"));
    });
}

// ..... more provider after this. You can use extensions to wrap logic

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var logger = services.GetRequiredService<ILogger<Program>>();
    ApplicationDbContext dbContext = dbUsed switch
    {
        "postgre" => services.GetRequiredService<PostgreDbContext>(),
        "sqlite" => services.GetRequiredService<SqliteDbContext>(),
        _ => services.GetRequiredService<ApplicationDbContext>(),
    };
    try
    {
        await dbContext.Database.MigrateAsync();
        using var reader = File.OpenRead("./student.json");
        List<Student>? students = JsonSerializer.Deserialize<List<Student>>(reader);

        if (!dbContext.Students.Any())
        {
            if (students is not null)
            {
                dbContext.AddRange(students);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError("An error occured during migration and seeding: {}", ex.Message);
        logger.LogError("Details: {}", ex.StackTrace);
    }
}

app.Run();
