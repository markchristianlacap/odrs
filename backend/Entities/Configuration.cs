namespace Backend.Entities;

public class Configuration
{
    public Guid Id { get; set; }
    public string Property { get; set; } = null!;
    public string Value { get; set; } = null!;
}
