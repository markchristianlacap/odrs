using Backend.Database;
using Humanizer;

namespace Backend.Features.User.Users.Index;

public class UserPagedReq : PagedReq
{
    public string? Search { get; set; }
}

public class UserRowRes : UserModel
{
    public Guid Id { get; set; }
    public string? RoleDesc => Role.Humanize();
}
