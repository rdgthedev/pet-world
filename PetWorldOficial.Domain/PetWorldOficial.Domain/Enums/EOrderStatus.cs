using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Domain.Enums;

public enum EOrderStatus
{
    WaitingPayment = 0,
    ProcessingPayment = 1,
    [Display(Name = "Pagamento Confimado")]
    PaymentConfirmed = 2,
    [Display(Name = "Aguardando Coleta")]
    AwaitingPickup = 3,
    [Display(Name = "Em Trânsito")]
    InTransit = 4,
    [Display(Name = "Entregue")]
    Delivered = 5,
    [Display(Name = "Cancelado")]
    Canceled = 6
}