using System.Reflection;
using Backend.Database.Interceptors;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    AuditInterceptor? auditInterceptor = null
) : DbContext(options)
{
    private readonly AuditInterceptor? _auditInterceptor = auditInterceptor;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_auditInterceptor != null)
            optionsBuilder.AddInterceptors(_auditInterceptor);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ResetPassword> ResetPasswords { get; set; } = null!;
    public DbSet<DocumentRequest> DocumentRequests { get; set; } = null!;
    public DbSet<Campus> Campuses { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
}
