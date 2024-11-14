using Backend.Entities.Common;

namespace Backend.Entities;

public class Program : AuditEntity
{
    public string Name { get; set; } = null!;
    public Guid? CampusId { get; set; }
    public Campus Campus { get; set; } = null!;
}
