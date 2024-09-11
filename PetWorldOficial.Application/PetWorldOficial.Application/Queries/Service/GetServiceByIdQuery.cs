using MediatR;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Queries.Service
{
    public record GetServiceByIdQuery(int Id) : IRequest<ServiceDetailsViewModel>
    {
    }
}
