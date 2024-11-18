using MediatR;
using PetWorldOficial.Application.ViewModels.Order;

namespace PetWorldOficial.Application.Commands.Order;

public class GetOrderByIdCommand : IRequest<OrderDetailsViewModel>
{
    public int Id { get; set; }
}