using Backend.Database;
using Backend.Entities;
using Backend.Services;
using Mapster;

namespace Backend.Features.User.Documents.Store;

public class Endpoint : Endpoint<DocumentRequestReq>
{
    public AppDbContext Db { get; set; } = null!;
    public IUserService UserService { get; set; } = null!;

    public override void Configure()
    {
        Post("/user/documents");
    }

    public override async Task HandleAsync(DocumentRequestReq req, CancellationToken ct)
    {
        var request = req.Adapt<DocumentRequest>();
        if (!UserService.UserId.HasValue)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        request.RequestById = UserService.UserId.Value;

        await Db.DocumentRequests.AddAsync(request, ct);
        await Db.SaveChangesAsync(ct);
    }
}
