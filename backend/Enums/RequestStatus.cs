namespace Backend.Enums;

public enum RequestStatus
{
    Rejected,
    Submitted,
    WaitingForPayment,
    PaymentSubmitted,
    OnProcess,
    PendingForPickup,
    Released,
    Canceled,
}
