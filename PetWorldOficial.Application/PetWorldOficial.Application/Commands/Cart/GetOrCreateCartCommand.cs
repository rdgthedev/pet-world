﻿using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Commands.Cart
{
    public class GetOrCreateCartCommand : IRequest<CartDetailsViewModel>
    {
    }
}