namespace Backend.Entities.Common;

public class AuditEntity : BaseEntity
{
    public Guid? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public Guid? UpdatedById { get; set; }
    public User? UpdatedBy { get; set; }
}
