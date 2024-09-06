using AutoMapper;
using PetWorldOficial.Application.Commands.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
