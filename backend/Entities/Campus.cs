using Backend.Entities.Common;

namespace Backend.Entities;

public class Campus : AuditEntity
{
    public string Name { get; set; } = null!;
}
