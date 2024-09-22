using Backend.Database;
using Backend.Entities;
using Backend.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Requests.Store;

public class Endpoint : Endpoint<RequestReq, RequestRes>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Post("/requests");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RequestReq req, CancellationToken ct)
    {
        var request = req.Adapt<Request>();
        request.Histories = [];
        request.ReferenceNumber = await GenerateReferenceNumber(ct);
        var fee = await Db.Fees.FirstOrDefaultAsync(
            x => x.DocumentType == request.DocumentType,
            ct
        );
        request.Amount = fee?.Amount ?? 0;
        var history = new RequestHistory
        {
            Remarks = "Request created",
            RequestStatus = RequestStatus.Submitted,
        };
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
