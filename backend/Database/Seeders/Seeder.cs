namespace Backend.Database.Seeders;

public static class Seeder
{
    public static void Seed(AppDbContext context)
    {
        UsersSeeder.Seed(context);
    }
}
