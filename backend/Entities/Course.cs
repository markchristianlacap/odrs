using Backend.Entities.Common;

namespace Backend.Entities;

public class Course : AuditEntity
{
    public string Name { get; set; } = null!;
}
