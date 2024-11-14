namespace Backend.Features.Options.Programs;

public class ProgramOptionsReq
{
    public Guid? CampusId { get; set; }
}

public class ProgramOptionsRes
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
