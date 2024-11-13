namespace PetWorldOficial.Domain.Enums;

public enum EOrderStatus
{
    WaitingPayment = 0,
    ProcessingPayment = 1,
    PaymentConfirmed = 2,
    AwaitingPickup = 3,
    Completed = 4,
    Canceled = 5
}