﻿using Backend.Entities;

namespace Backend.Database.Seeders;

public static class UsersSeeder
{
    public static void Seed(AppDbContext context)
    {
        var admin = new User
        {
            LastName = "Admin",
            FirstName = "OMSC",
            Email = "admin@admin.com",
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password"),
            ContactNumber = "n/a",
            Address = "n/a",
        };
        var mark = new User
        {
            LastName = "Mark Admin",
            FirstName = "OMSC",
            Email = "markchristianlacap@gmail.com",
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password"),
            ContactNumber = "n/a",
            Address = "n/a",
        };
        if (!context.Users.Any(u => u.Email == admin.Email))
        {
            context.Users.Add(admin);
        }
        if (!context.Users.Any(u => u.Email == mark.Email))
        {
            context.Users.Add(mark);
        }
        context.SaveChanges();
    }
}
