using Backend.Enums;

namespace Backend.Entities;

public class RequestRequirement
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public RequirementType Type { get; set; }
    public string Path { get; set; } = null!;
}
