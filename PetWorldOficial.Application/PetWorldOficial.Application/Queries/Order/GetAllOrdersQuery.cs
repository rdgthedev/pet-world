using MediatR;
using PetWorldOficial.Application.ViewModels.Order;

namespace PetWorldOficial.Application.Queries.Order;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDetailsViewModel>>
{
}