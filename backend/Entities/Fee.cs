using Backend.Enums;

namespace Backend.Entities;

public class Fee
{
    public DocumentType DocumentType { get; set; }
    public decimal Amount { get; set; }
}
