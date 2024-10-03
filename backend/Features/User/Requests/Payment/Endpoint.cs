using Backend.Database;
using Backend.Enums;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Requests.Payment;

public class Endpoint : EndpointWithoutRequest
{
    public AppDbContext Db { get; set; } = null!;
    public IStorageService StorageService { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/requests/{id}/payment");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var request = await Db.Requests.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (request == null || request.PaymentPath == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var bytes = await StorageService.DownloadFileAsync(request.PaymentPath, ct);
        await SendBytesAsync(
            bytes,
            fileName: request.PaymentPath,
            contentType: "application/image",
            cancellation: ct
        );
    }
}
