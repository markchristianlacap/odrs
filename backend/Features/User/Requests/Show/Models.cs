using Backend.Enums;
using Humanizer;

namespace Backend.Features.User.Requests.Show;

public class HistoryModel
{
    public Guid RequestId { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public string RequestStatusDesc => RequestStatus.Humanize(LetterCasing.Title);
    public string? Remarks { get; set; }
}

public class RequestShowRes : RequestModel
{
    public Guid Id { get; set; }
    public string ProgramName { get; set; } = null!;
    public string CampusName { get; set; } = null!;
    public string DocumentTypeDesc => DocumentType.Humanize(LetterCasing.Title);
    public string RequesterTypeDesc => RequesterType.Humanize(LetterCasing.Title);
    public string YearLevelDesc => YearLevel.Humanize(LetterCasing.Title);
    public string SemesterDesc => Semester.Humanize(LetterCasing.Title);
    public List<HistoryModel> Histories { get; set; } = null!;
}
