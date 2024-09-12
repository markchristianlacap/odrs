using Backend.Enums;

namespace Backend.Features.User.Documents;

public class DocumentRequestModel
{
    public CopyType? CopyType { get; set; }
    public DocumentType? DocumentType { get; set; }
    public Guid RequestById { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? ExtensionName { get; set; }
    public string Email { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public DateOnly Birthdate { get; set; }
    public string Address { get; set; } = null!;
    public string Purpose { get; set; } = null!;
    public int? LastAttendanceStartYear { get; set; }
    public int? LastAttendanceEndYear { get; set; }
    public Semester? LastAttendanceSemester { get; set; }
    public YearLevel? LastAttendanceYearLevel { get; set; }
    public string LastAttendanceSection { get; set; } = null!;
    public bool? IsGraduate { get; set; }
}
