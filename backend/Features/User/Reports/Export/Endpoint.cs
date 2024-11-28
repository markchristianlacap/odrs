using Backend.Database;
using ClosedXML.Excel;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Reports.Export;

public class Endpoint : Endpoint<ReportReq>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/reports/export");
    }

    public override async Task HandleAsync(ReportReq req, CancellationToken ct)
    {
        var query = Db.RequestDocuments.AsQueryable();
        if (req.DateTo is not null)
        {
            query = query.Where(x => x.Request.CreatedAt.Date <= req.DateTo.Value.Date);
        }

        if (req.DateFrom is not null)
        {
            query = query.Where(x => x.Request.CreatedAt.Date >= req.DateFrom.Value.Date);
        }

        if (req.Status is not null)
        {
            query = query.Where(x => x.Request.Status == req.Status);
        }
        var requests = await query
            .Select(static x => new
            {
                x.Request.CreatedAt,
                x.Request.FirstName,
                x.Request.LastName,
                x.Request.ExtensionName,
                x.Request.MiddleName,
                x.Type,
                Campus = x.Request.Campus.Name,
                Program = x.Request.Program.Name,
                x.Purpose,
                x.Request.Amount,
                x.Request.ORNumber,
                x.Request.Status,
                x.Request.DateReleased,
                ActionTakenBy = x.Request.ReleasedBy!.FirstName
                    + " "
                    + x.Request.ReleasedBy.LastName,
            })
            .ToListAsync(ct);
        using var http = new HttpClient();
        var template = await http.GetByteArrayAsync(BaseURL + "templates/report.xlsx", ct);
        var wb = new XLWorkbook(new MemoryStream(template));
        var ws = wb.Worksheet(1);
        var row = 13;
        var col = 1;
        ws.Cell(row, col++).Value = "Date";
        ws.Cell(row, col++).Value = "Name";
        ws.Cell(row, col++).Value = "Type of Documents";
        ws.Cell(row, col++).Value = "Campus";
        ws.Cell(row, col++).Value = "Course";
        ws.Cell(row, col++).Value = "Purpose";
        ws.Cell(row, col++).Value = "Price";
        ws.Cell(row, col++).Value = "OR Number";
        ws.Cell(row, col++).Value = "Status";
        ws.Cell(row, col++).Value = "Date of Released ";
        ws.Cell(row, col++).Value = "Action Taken By";

        foreach (var request in requests)
        {
            row++;
            col = 1;

            ws.Cell(row, col++).Value = request.CreatedAt.ToString("yyyy-MM-dd");
            ws.Cell(row, col++).Value =
                $"{request.FirstName} {request.MiddleName} {request.LastName}";
            ws.Cell(row, col++).Value = request.Type.Humanize(LetterCasing.Sentence);
            ws.Cell(row, col++).Value = request.Campus;
            ws.Cell(row, col++).Value = request.Program;
            ws.Cell(row, col++).Value = request.Purpose;
            ws.Cell(row, col++).Value = request.Amount;
            ws.Cell(row, col++).Value = request.ORNumber;
            ws.Cell(row, col++).Value = request.Status.Humanize(LetterCasing.Sentence);
            ws.Cell(row, col++).Value = request.DateReleased;
            ws.Cell(row, col++).Value = request.ActionTakenBy;
        }
        ws.Columns().AdjustToContents();
        var stream = new MemoryStream();
        wb.SaveAs(stream);
        stream.Position = 0;
        await SendStreamAsync(
            stream,
            fileName: "requests report.xlsx",
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            cancellation: ct
        );
    }
}
