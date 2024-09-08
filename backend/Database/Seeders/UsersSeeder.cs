using Backend.Entities;

namespace Backend.Database.Seeders;

public static class UsersSeeder
{
  public static void Seed(AppDbContext context)
  {
    var admin = new User
    {
      Name = "Admin",
      Email = "admin@admin.com",
      Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password"),
      ContactNumber = "n/a",
    };
    if (!context.Users.Any(u => u.Email == admin.Email))
    {
      context.Users.Add(admin);
    }
    context.SaveChanges();
  }
}
