using Backend.Database;
using Backend.Enums;

namespace Backend.Features.User.Requests.Index;

public class RequestPagedReq : PagedReq
{
    public RequestStatus? Status { get; set; }
    public string? Search { get; set; }
    public DocumentType? DocumentType { get; set; }
}

public class RequestRowRes : RequestModel
{
    public Guid Id { get; set; }
    public string ProgramName { get; set; } = null!;
    public string CampusName { get; set; } = null!;
    public string DocumentsDesc { get; set; } = null!;
}
