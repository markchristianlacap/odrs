using Backend.Enums;

namespace Backend.Features.Requests.Store;

public class RequestReq : RequestModel
{
    public IFormFile? Picture { get; set; } = null!;
    public IFormFile? ValidId { get; set; } = null;
    public IFormFile? RepresentativeValidId { get; set; } = null;
    public IFormFile? AuthorizationLetter { get; set; } = null;
    public IFormFile? SpecialPowerOfAttorney { get; set; } = null;
    public IFormFile? AffidavitOfLoss { get; set; } = null;
    public IFormFile? BirthCertificate { get; set; } = null;
    public IFormFile? RequestLetter { get; set; } = null;
    public IFormFile? DocumentToBeCertified { get; set; } = null;
    public CollectorType CollectorType { get; set; }
}

public class RequestRes : RequestModel
{
    public string ReferenceNumber { get; set; } = null!;
    public decimal Amount { get; set; }
}
