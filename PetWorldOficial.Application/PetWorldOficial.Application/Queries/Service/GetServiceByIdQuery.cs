using MediatR;
using PetWorldOficial.Application.DTOs.Service.Output;
using PetWorldOficial.Application.ViewModels.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Queries.Service
{
    public record GetServiceByIdQuery(int Id) : IRequest<ServiceDetailsViewModel>
    {
    }
}
