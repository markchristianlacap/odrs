using Backend.Entities.Common;
using Backend.Enums;

namespace Backend.Entities;

public class RequestHistory : AuditEntity
{
    public Guid RequestId { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public string? Remarks { get; set; }
}
