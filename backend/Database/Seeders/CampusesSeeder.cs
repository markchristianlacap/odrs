﻿using Backend.Entities;

namespace Backend.Database.Seeders;

public static class CampusesSeeder
{
    public static void Seed(AppDbContext context)
    {
        var campuses = new List<Campus>
        {
            new() { Name = "Main", },
            new() { Name = "San Jose", },
            new() { Name = "Murtha", },
            new() { Name = "Sablayan", },
            new() { Name = "Mamburao", },
            new() { Name = "Lubang", },
        };
        foreach (var campus in campuses)
        {
            if (!context.Campuses.Any(c => c.Name == campus.Name))
            {
                context.Campuses.Add(campus);
            }
        }
        context.SaveChanges();
    }
}