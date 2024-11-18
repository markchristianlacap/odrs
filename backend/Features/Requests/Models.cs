using Backend.Enums;

namespace Backend.Features.Requests;

public class RequestDocumentModel
{
    public DocumentType Type { get; set; }
    public string Purpose { get; set; } = null!;
    public int Copies { get; set; }
}

public class RequestModel
{
    [BindFrom("documents[]")]
    public List<RequestDocumentModel> Documents { get; set; } = [];
    public string Email { get; set; } = null!;
    public RequesterType? RequesterType { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? ExtensionName { get; set; }
    public string? StudentNumber { get; set; }
    public string ContactNumber { get; set; } = null!;
    public DateOnly? Birthdate { get; set; } = null;
    public string Address { get; set; } = null!;
    public int? LastAttendanceStartYear { get; set; } = null;
    public int? LastAttendanceEndYear { get; set; } = null;
    public Semester? Semester { get; set; }
    public YearLevel? YearLevel { get; set; }
    public string? Section { get; set; }
    public Guid? ProgramId { get; set; } = null;
    public Guid? CampusId { get; set; } = null;
    public string? Representative { get; set; }
}
