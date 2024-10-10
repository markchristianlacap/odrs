using Backend.Database;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Requests.Requirements.Show;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public IStorageService StorageService { get; set; } = null!;

    public override void Configure()
    {
        Get("/requests/{requestId}/requirements/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var requestId = Route<Guid>("requestId");
        var path = await Db
            .RequestRequirements.AsQueryable()
            .Where(x => x.Id == id && x.RequestId == requestId)
            .Select(x => x.Path)
            .FirstOrDefaultAsync(ct);
        if (path == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var bytes = await StorageService.DownloadFileAsync(path, ct);
        await SendBytesAsync(bytes, contentType: "image/png", fileName: path, cancellation: ct);
    }
}
