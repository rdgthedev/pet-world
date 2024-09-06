using AutoMapper;
using PetWorldOficial.Application.ViewModels.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Mappers.Service
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Domain.Entities.Service, ServiceDetailsViewModel>();
            CreateMap<Domain.Entities.Service, ServiceDetailsViewModel>();
            CreateMap<Domain.Entities.Service, ServiceDetailsViewModel>();
        }
    }
}
