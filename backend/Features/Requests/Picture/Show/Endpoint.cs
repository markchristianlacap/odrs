using Backend.Database;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Requests.Picture.Show;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public IStorageService StorageService { get; set; } = null!;

    public override void Configure()
    {
        Get("/requests/{id}/picture");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var path = await Db
            .Requests.AsQueryable()
            .Where(x => x.Id == id)
            .Select(x => x.PicturePath)
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
