using Backend.Entities.Common;

namespace Backend.Entities;

public class Program : AuditEntity
{
    public string Name { get; set; } = null!;
}
