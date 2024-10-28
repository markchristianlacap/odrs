namespace Backend.Entities.Common;

public class AuditEntity : BaseEntity
{
    public Guid? CreatedById { get; set; } = null;
    public User? CreatedBy { get; set; }
    public Guid? UpdatedById { get; set; } = null;
    public User? UpdatedBy { get; set; }
}
