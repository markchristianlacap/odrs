using Backend.Enums;
using Humanizer;

namespace Backend.Features.User.Requests.Show;

public class HistoryModel
{
    public Guid RequestId { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public string RequestStatusDesc => RequestStatus.Humanize(LetterCasing.Title);
    public string? Remarks { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class RequestRequirementModel
{
    public Guid Id { get; set; }
    public RequirementType Type { get; set; }
    public string TypeDesc => Type.Humanize(LetterCasing.Title);
}

public class RequestShowRes : RequestModel
{
    public Guid Id { get; set; }
    public string CollectorTypeDesc => CollectorType.Humanize(LetterCasing.Title);
    public string ProgramName { get; set; } = null!;
    public string CampusName { get; set; } = null!;
    public string DocumentsDesc { get; set; } = null!;
    public string RequesterTypeDesc => RequesterType.Humanize(LetterCasing.Title);
    public string? YearLevelDesc => YearLevel?.Humanize(LetterCasing.Title);
    public string SemesterDesc => Semester.Humanize(LetterCasing.Title);
    public DateTime? ClaimDeadline { get; set; }
    public List<HistoryModel> Histories { get; set; } = null!;
    public List<RequestRequirementModel> Requirements { get; set; } = null!;
}
