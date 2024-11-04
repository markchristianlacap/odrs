namespace Backend.Database.Seeders;

public static class ProgramsSeeder
{
    public static void Seed(AppDbContext context)
    {
        var courses = new List<Entities.Program>
        {
            new() { Name = "Bachelor in Human Services" },
            new() { Name = "Bachelor of Agriculture Technology" },
            new() { Name = "Bachelor of Arts in Communication" },
            new() { Name = "Bachelor of Arts in History" },
            new() { Name = "Bachelor of Elementary Education" },
            new() { Name = "Bachelor of Physical Education" },
            new() { Name = "Bachelor of Public Administration" },
            new() { Name = "Bachelor of Science in Accountancy" },
            new() { Name = "Bachelor of Science in Accounting Information System" },
            new() { Name = "Bachelor of Science in Agriculture" },
            new() { Name = "Bachelor of Science in Agroforestry" },
            new() { Name = "Bachelor of Science in Architecture" },
            new() { Name = "Bachelor of Science in Business Administration" },
            new() { Name = "Bachelor of Science in Civil Engineering" },
            new() { Name = "Bachelor of Science in Criminology" },
            new() { Name = "Bachelor of Science in Development Communication" },
            new() { Name = "Bachelor of Science in Electrical Engineering" },
            new() { Name = "Bachelor of Science in Hospitality Management" },
            new() { Name = "Bachelor of Science in Industrial Security Management" },
            new() { Name = "Bachelor of Science in Industrial Technology" },
            new() { Name = "Bachelor of Science in Information Technology" },
            new() { Name = "Bachelor of Science in Law Enforcement Administration" },
            new() { Name = "Bachelor of Science in Management Accounting" },
            new() { Name = "Bachelor of Science in Midwifery" },
            new() { Name = "Bachelor of Science in Office Administration" },
            new() { Name = "Bachelor of Science in Social Work" },
            new() { Name = "Bachelor of Science in Tourism Management" },
            new() { Name = "Bachelor of Secondary Education" },
            new() { Name = "Bachelor of Technical-Vocational Teacher Education" },
            new() { Name = "Bachelor of Technology and Livelihood Education" },
            new() { Name = "Diploma in Midwifery" },
            new() { Name = "Bachelor of Secondary Education Major in Filipino" },
            new() { Name = "Bachelor of Secondary Education Major in Mathematics" },
            new() { Name = "Bachelor of Secondary Education Major in English" },
            new() { Name = "Bachelor of Secondary Education Major in Science" },
            new() { Name = "Bachelor of Technology and Livelihood Education" },
            new() { Name = "Bachelor of Physical Education" },
            new() { Name = "Bachelor of Elementary Education" },
        };

        foreach (var course in courses)
        {
            if (!context.Programs.Any(c => c.Name == course.Name))
            {
                context.Programs.Add(course);
            }
        }
        context.SaveChanges();
    }
}
