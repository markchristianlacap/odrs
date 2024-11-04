using Backend.Entities.Common;
using Backend.Enums;

namespace Backend.Entities;

public class Request
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ReferenceNumber { get; set; } = null!;
    public List<RequestDocument> Documents { get; set; } = null!;
    public string Email { get; set; } = null!;
    public RequesterType RequesterType { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string? ExtensionName { get; set; }
    public string StudentNumber { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public DateOnly Birthdate { get; set; }
    public string Address { get; set; } = null!;
    public int LastAttendanceStartYear { get; set; }
    public int LastAttendanceEndYear { get; set; }
    public Semester Semester { get; set; }
    public YearLevel? YearLevel { get; set; }
    public string Section { get; set; } = null!;
    public Guid ProgramId { get; set; }
    public Program Program { get; set; } = null!;
    public Guid CampusId { get; set; }
    public Campus Campus { get; set; } = null!;
    public List<RequestHistory> Histories { get; set; } = null!;
    public decimal Amount { get; set; }
    public RequestStatus Status { get; set; }
    public string PicturePath { get; set; } = null!;
    public string? PaymentPath { get; set; }
    public string? ORNumber { get; set; }
    public CollectorType CollectorType { get; set; }
    public List<RequestRequirement> Requirements { get; set; } = null!;
    public string? Representative { get; set; }
    public DateTime? DateReleased { get; set; }
    public Guid? ReleasedById { get; set; }
    public User? ReleasedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ClaimDeadline { get; set; }
}
