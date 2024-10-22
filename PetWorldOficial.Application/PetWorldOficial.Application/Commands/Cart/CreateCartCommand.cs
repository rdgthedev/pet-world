using MediatR;
using PetWorldOficial.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Commands.Cart
{
    public class CreateCartCommand : IRequest<CartDetailsViewModel?>
    {
        public int? ProductId { get; set; }
    }
}
