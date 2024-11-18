using MediatR;

namespace PetWorldOficial.Application.Commands.Order;

public class UpdateStatusToDeliveredCommand : IRequest<(int statusCode, string message)>
{
    public int Id { get; set; }
}