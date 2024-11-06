using Backend.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.User.Reports;

public class Endpoint : Endpoint<ReportReq, List<ReportRes>>
{
    public AppDbContext Db { get; set; } = null!;

    public override void Configure()
    {
        Get("/user/reports");
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
        Response = await query
            .Select(x => new ReportRes
            {
                CreatedAt = x.Request.CreatedAt,
                Status = x.Request.Status,
                Email = x.Request.Email,
                Amount = x.Request.Amount,
                Address = x.Request.Address,
                Purpose = x.Purpose,
                FirstName = x.Request.FirstName,
                LastName = x.Request.LastName,
                ExtensionName = x.Request.ExtensionName,
                MiddleName = x.Request.MiddleName,
                Section = x.Request.Section,
                ORNumber = x.Request.ORNumber,
                Semester = x.Request.Semester,
                Birthdate = x.Request.Birthdate,
                Document = x.Type.ToString(),
                YearLevel = x.Request.YearLevel,
                CampusName = x.Request.Campus.Name,
                PaymentPath = x.Request.PaymentPath,
                PicturePath = x.Request.PicturePath,
                ProgramName = x.Request.Program.Name,
                DateReleased = x.Request.DateReleased,
                CollectorType = x.Request.CollectorType,
                ContactNumber = x.Request.ContactNumber,
                RequesterType = x.Request.RequesterType,
                StudentNumber = x.Request.StudentNumber,
                Representative = x.Request.Representative,
                ReferenceNumber = x.Request.ReferenceNumber,
                LastAttendanceEndYear = x.Request.LastAttendanceEndYear,
                LastAttendanceStartYear = x.Request.LastAttendanceStartYear,
            })
            .ToListAsync(ct);
    }
}
