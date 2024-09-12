using Backend.Enums;

namespace Backend.Features.Requests;

public class RequestModel
{
    public DocumentType? DocumentType { get; set; }
    public string Email { get; set; } = null!;
    public RequesterType? RequesterType { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? ExtensionName { get; set; }
    public string StudentNumber { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public DateOnly Birthdate { get; set; }
    public string Address { get; set; } = null!;
    public string Purpose { get; set; } = null!;
    public int LastAttendanceStartYear { get; set; }
    public int LastAttendanceEndYear { get; set; }
    public Semester? Semester { get; set; }
    public YearLevel? YearLevel { get; set; }
    public string Section { get; set; } = null!;
    public Guid ProgramId { get; set; }
    public Guid CampusId { get; set; }
}
