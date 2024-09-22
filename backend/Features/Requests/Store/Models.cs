namespace Backend.Features.Requests.Store;

public class RequestReq : RequestModel { }

public class RequestRes : RequestModel
{
    public string ReferenceNumber { get; set; } = null!;
    public decimal Amount { get; set; }
}
