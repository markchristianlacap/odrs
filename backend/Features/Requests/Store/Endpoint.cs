using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Backend.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Requests.Store;

public class Endpoint : Endpoint<RequestReq, RequestRes>
{
    public AppDbContext Db { get; set; } = null!;
    public IStorageService StorageService { get; set; } = null!;

    public override void Configure()
    {
        Post("/requests");
        AllowAnonymous();
        AllowFormData();
        AllowFileUploads();
    }

    public override async Task HandleAsync(RequestReq req, CancellationToken ct)
    {
        var request = req.Adapt<Request>();
        request.Histories = [];
        request.ReferenceNumber = await GenerateReferenceNumber(ct);
        request.Status = RequestStatus.Submitted;
        var history = new RequestHistory
        {
            Remarks =
                "Request submitted. Waiting for admin validation before proceeding with payment.",
            RequestStatus = RequestStatus.Submitted,
        };
        request.PicturePath = await StorageService.UploadFileAsync(req.Picture, "pictures", ct);
        request.Histories.Add(history);
        await Db.Requests.AddAsync(request, ct);
        await Db.SaveChangesAsync(ct);
        Response = request.Adapt<RequestRes>();
    }

    private async Task<string> GenerateReferenceNumber(CancellationToken ct)
    {
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;
        var lastNum = await Db
            .Requests.Where(x => x.CreatedAt.Year == year && x.CreatedAt.Month == month)
            .OrderByDescending(x => x.ReferenceNumber)
            .Select(x => x.ReferenceNumber)
            .FirstOrDefaultAsync(ct);
        lastNum = lastNum?[6..];
        var newNum = lastNum == null ? 1 : int.Parse(lastNum) + 1;
        return $"{year}{month:00}{newNum:0000}";
    }
}
