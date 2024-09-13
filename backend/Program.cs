global using FastEndpoints;
using System.Text.Json;
using Backend.Database;
using Backend.Database.Interceptors;
using Backend.Database.Seeders;
using Backend.Services;
using FastEndpoints.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var bld = WebApplication.CreateBuilder();
var config = bld.Configuration;
var connectionString = config.GetConnectionString("DefaultConnection");
var directory = config.GetValue<string>("Directory") ?? "directory";

// Configure services
bld.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

bld.Services.AddSingleton<IDateTimeService, DateTimeService>();
bld.Services.AddSingleton<IUserService, UserService>();
bld.Services.AddSingleton<AppDbInterceptor>();
bld.Services.AddSingleton<IEmailService, EmailService>();
bld.Services.AddSpaStaticFiles(o => o.RootPath = "dist");
bld.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(directory))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(30));

bld.Services.AddAuthenticationCookie(validFor: TimeSpan.FromHours(8))
    .AddAuthorization()
    .AddFastEndpoints();

var app = bld.Build();

// Run args seed the database
if (args.Length > 0 && args[0] == "seed")
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Seeder.Seed(db);
    Environment.Exit(0);
}

app.UseRouting();
app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(o =>
    {
        o.Endpoints.RoutePrefix = "api";
        o.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
app.UseEndpoints(_ => { });

// Spa fallback
app.UseSpaStaticFiles();
app.UseSpa(x =>
{
    if (!app.Environment.IsDevelopment())
        return;
    x.UseProxyToSpaDevelopmentServer("http://localhost:3201");
});

app.Run();
