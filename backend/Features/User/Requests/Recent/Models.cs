using Backend.Enums;
using Humanizer;

namespace Backend.Features.User.Requests.Recent;

public class RequestRecentRes
{
    public Guid Id { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? ExtensionName { get; set; }
    public string DocumentsDesc { get; set; } = null!;
    public RequesterType RequesterType { get; set; }
    public YearLevel? YearLevel { get; set; }
    public string RequesterTypeDesc => RequesterType.Humanize(LetterCasing.Title);
    public string YearLevelDesc => YearLevel?.Humanize(LetterCasing.Title) ?? "N/A";
    public string SemesterDesc => Semester.Humanize(LetterCasing.Title);
    public Semester Semester { get; set; }
    public string FirstName { get; set; } = null!;
    public DocumentType Type { get; set; }
    public string TypeDesc => Type.Humanize(LetterCasing.Title);
}
