using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace PetWorldOficial.Application.Commands.Order
{
    public class UpdateStatusToInTransitCommand : IRequest<(int statusCode, string message)>
    {
        public int Id { get; set; }
    }
}