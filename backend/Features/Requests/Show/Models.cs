using Backend.Enums;
using Humanizer;

namespace Backend.Features.Requests.Show;

public class HistoryModel
{
    public Guid RequestId { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public string RequestStatusDesc => RequestStatus.Humanize(LetterCasing.Title);
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class RequestDocumentShow : RequestDocumentModel
{
    public string TypeDesc => Type.Humanize(LetterCasing.Title);
}

public class RequestShowRes : RequestModel
{
    public Guid Id { get; set; }
    public string ReferenceNumber { get; set; } = null!;
    public string ProgramName { get; set; } = null!;
    public string CampusName { get; set; } = null!;
    public string RequesterTypeDesc => RequesterType.Humanize(LetterCasing.Title);
    public string YearLevelDesc => YearLevel.Humanize(LetterCasing.Title);
    public string SemesterDesc => Semester.Humanize(LetterCasing.Title);
    public RequestStatus Status { get; set; }
    public string StatusDesc => Status.Humanize(LetterCasing.Title);
    public List<HistoryModel> Histories { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public new List<RequestDocumentShow> Documents { get; set; } = null!;
}
