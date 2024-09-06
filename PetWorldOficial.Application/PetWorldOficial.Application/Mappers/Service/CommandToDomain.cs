using AutoMapper;
using PetWorldOficial.Application.Commands.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Mappers.Service
{
    public class CommandToDomain : Profile
    {
        public CommandToDomain()
        {
            CreateMap<CreateServiceCommand, Domain.Entities.Service>();
            CreateMap<UpdateServiceCommand, Domain.Entities.Service>();
            CreateMap<DeleteServiceCommand, Domain.Entities.Service>();
        }
    }
}
