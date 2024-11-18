using MediatR;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Commands.Cart;

public class GetCartFromCookieCommand : IRequest<CartDetailsViewModel>
{
    // public int Id { get; set; }
}