using Backend.Enums;

namespace Backend.Features.User.Documents;

public class DocumentModel
{
  public DocumentType DocumentType { get; set; }
  public string? Reason { get; set; }
}
