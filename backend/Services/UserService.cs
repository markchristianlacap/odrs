using System.Security.Claims;

namespace Backend.Services;

public interface IUserService
{
    Guid? UserId { get; }
}

public class UserService(IHttpContextAccessor ctx) : IUserService
{
    public Guid? UserId =>
        Guid.TryParse(ctx.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
            ? id
            : null;
}
