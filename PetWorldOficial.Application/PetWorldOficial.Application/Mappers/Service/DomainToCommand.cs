using AutoMapper;
using PetWorldOficial.Application.Commands.Service;

namespace PetWorldOficial.Application.Mappers.Service
{
    public class DomainToCommand : Profile
    {
        public DomainToCommand()
        {
            CreateMap<Domain.Entities.Service, CreateServiceCommand>();
            CreateMap<Domain.Entities.Service, UpdateServiceCommand>();
            CreateMap<Domain.Entities.Service, DeleteServiceCommand>();
        }
    }
}
