namespace Backend.Database.Seeders;

public static class ProgramsSeeder
{
    public static void Seed(AppDbContext context)
    {
        var sjCampusId = context.Campuses.First(c => c.Name == "San Jose").Id;
        var mainCampusId = context.Campuses.First(c => c.Name == "Main").Id;
        // var courses = new List<Entities.Program>
        // {
        //     new() { Name = "Bachelor in Human Services" },
        //     new() { Name = "Bachelor of Agriculture Technology" },
        //     new() { Name = "Bachelor of Arts in Communication" },
        //     new() { Name = "Bachelor of Arts in History" },
        //     new() { Name = "Bachelor of Elementary Education" },
        //     new() { Name = "Bachelor of Physical Education" },
        //     new() { Name = "Bachelor of Public Administration" },
        //     new() { Name = "Bachelor of Science in Accountancy" },
        //     new() { Name = "Bachelor of Science in Accounting Information System" },
        //     new() { Name = "Bachelor of Science in Agriculture" },
        //     new() { Name = "Bachelor of Science in Agroforestry" },
        //     new() { Name = "Bachelor of Science in Architecture" },
        //     new() { Name = "Bachelor of Science in Business Administration" },
        //     new() { Name = "Bachelor of Science in Civil Engineering" },
        //     new() { Name = "Bachelor of Science in Criminology" },
        //     new() { Name = "Bachelor of Science in Development Communication" },
        //     new() { Name = "Bachelor of Science in Electrical Engineering" },
        //     new() { Name = "Bachelor of Science in Hospitality Management" },
        //     new() { Name = "Bachelor of Science in Industrial Security Management" },
        //     new() { Name = "Bachelor of Science in Industrial Technology" },
        //     new() { Name = "Bachelor of Science in Information Technology" },
        //     new() { Name = "Bachelor of Science in Law Enforcement Administration" },
        //     new() { Name = "Bachelor of Science in Management Accounting" },
        //     new() { Name = "Bachelor of Science in Midwifery" },
        //     new() { Name = "Bachelor of Science in Office Administration" },
        //     new() { Name = "Bachelor of Science in Social Work" },
        //     new() { Name = "Bachelor of Science in Tourism Management" },
        //     new() { Name = "Bachelor of Secondary Education" },
        //     new() { Name = "Bachelor of Technical-Vocational Teacher Education" },
        //     new() { Name = "Bachelor of Technology and Livelihood Education" },
        //     new() { Name = "Diploma in Midwifery" },
        //     new() { Name = "Bachelor of Secondary Education Major in Filipino" },
        //     new() { Name = "Bachelor of Secondary Education Major in Mathematics" },
        //     new() { Name = "Bachelor of Secondary Education Major in English" },
        //     new() { Name = "Bachelor of Secondary Education Major in Science" },
        //     new() { Name = "Bachelor of Technology and Livelihood Education" },
        //     new() { Name = "Bachelor of Physical Education" },
        //     new() { Name = "Bachelor of Elementary Education" },
        // };
        //

        var courses = new List<Entities.Program>
        {
            new() { Name = "Bachelor of Arts in Communication", CampusId = mainCampusId },
            new() { Name = "Bachelor of Arts in History", CampusId = mainCampusId },
            new() { Name = "Bachelor in Human Services", CampusId = mainCampusId },
            new() { Name = "BS Development Communication", CampusId = mainCampusId },
            new() { Name = "BS Social Work", CampusId = mainCampusId },
            new() { Name = "BS Architecture", CampusId = mainCampusId },
            new() { Name = "BS Civil Engineering", CampusId = mainCampusId },
            new() { Name = "BS Electrical Engineering", CampusId = mainCampusId },
            new() { Name = "BS Industrial Engineering", CampusId = mainCampusId },
            new() { Name = "BS Accountancy", CampusId = mainCampusId },
            new() { Name = "BS Accounting Information System", CampusId = mainCampusId },
            new()
            {
                Name = "BS Business Administration - Financial Management",
                CampusId = mainCampusId,
            },
            new()
            {
                Name = "BS Business Administration - Operations Management",
                CampusId = mainCampusId,
            },
            new() { Name = "BS Management Accounting", CampusId = mainCampusId },
            new() { Name = "BS Office Administration", CampusId = mainCampusId },
            new() { Name = "Bachelor of Public Administration", CampusId = mainCampusId },
            new() { Name = "BS Hospitality Management", CampusId = mainCampusId },
            new() { Name = "BS Criminology", CampusId = mainCampusId },
            new() { Name = "BS Industrial Security Management", CampusId = mainCampusId },
            new() { Name = "BS Law Enforcement Administration", CampusId = mainCampusId },
            new() { Name = "Bachelor of Elementary Education", CampusId = sjCampusId },
            new() { Name = "Bachelor of Secondary Education - English", CampusId = sjCampusId },
            new() { Name = "Bachelor of Secondary Education - Filipino", CampusId = sjCampusId },
            new() { Name = "Bachelor of Secondary Education - Mathematics", CampusId = sjCampusId },
            new() { Name = "Bachelor of Secondary Education - Science", CampusId = sjCampusId },
            new() { Name = "Bachelor of Physical Education", CampusId = sjCampusId },
            new()
            {
                Name = "Bachelor of Technology And Livelihood Education - Home Economics",
                CampusId = sjCampusId,
            },
            new() { Name = "BS Information Technology", CampusId = sjCampusId },
            new() { Name = "BS Midwifery", CampusId = sjCampusId },
        };

        foreach (var course in courses)
        {
            if (!context.Programs.Any(c => c.Name == course.Name && c.CampusId == course.CampusId))
            {
                context.Programs.Add(course);
            }
        }
        context.SaveChanges();
    }
}
