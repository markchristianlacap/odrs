using System.ComponentModel;

namespace Backend.Enums;

public enum DocumentType
{
    [Description("Transcript of Records")]
    TOR,
    Diploma,
    CertificateOfGraduation,
}
