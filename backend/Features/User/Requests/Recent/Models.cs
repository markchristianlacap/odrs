using Humanizer;

namespace Backend.Features.User.Requests.Recent;

public class RequestRecentRes : RequestModel
{
    public Guid Id { get; set; }
    public string ProgramName { get; set; } = null!;
    public string CampusName { get; set; } = null!;
    public string DocumentTypeDesc =>
        string.Join(", ", DocumentTypes.Select(x => x.Humanize(LetterCasing.Title)));
    public string RequesterTypeDesc => RequesterType.Humanize(LetterCasing.Title);
    public string YearLevelDesc => YearLevel.Humanize(LetterCasing.Title);
    public string SemesterDesc => Semester.Humanize(LetterCasing.Title);
}
