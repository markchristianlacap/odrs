using Backend.Database;

namespace Backend.Features.User.Requests.Index;

public class RequestPagedReq : PagedReq
{
    public string? Search { get; set; }
}

public class RequestRowRes : RequestModel
{
    public Guid Id { get; set; }
    public string ProgramName { get; set; } = null!;
    public string CampusName { get; set; } = null!;
}
