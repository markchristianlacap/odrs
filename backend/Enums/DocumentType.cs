using System.ComponentModel;

namespace Backend.Enums;

public enum DocumentType
{
    [Description("Transcript of Records")]
    TOR,
    SecondCopyOfDiploma,
    CertificateOfEnrollment,
    CertificateOfMediumOfInstruction,

    [Description("Certificate of GPA/GWA")]
    CertificateOfGPAGWA,
    HonorableDismissal,

    [Description("Certificate, Authentication and Verification")]
    CAV,
}
