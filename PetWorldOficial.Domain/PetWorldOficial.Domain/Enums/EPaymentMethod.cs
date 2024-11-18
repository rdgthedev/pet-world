using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Domain.Enums;

public enum EPaymentMethod
{
    Boleto = 0,
    PIX = 1,
    [Display(Name = "Cartão")]
    Card = 2,
    Money = 3
}