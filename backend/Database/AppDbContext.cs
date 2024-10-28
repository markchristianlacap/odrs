using System.Reflection;
using Backend.Database.Interceptors;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    AppDbInterceptor? interceptor = null
) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (interceptor != null)
            optionsBuilder.AddInterceptors(interceptor);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ResetPassword> ResetPasswords { get; set; } = null!;
    public DbSet<Request> Requests { get; set; } = null!;
    public DbSet<RequestDocument> RequestDocuments { get; set; } = null!;
    public DbSet<Campus> Campuses { get; set; } = null!;
    public DbSet<Entities.Program> Programs { get; set; } = null!;
    public DbSet<RequestHistory> RequestHistories { get; set; } = null!;
    public DbSet<RequestRequirement> RequestRequirements { get; set; }
}
