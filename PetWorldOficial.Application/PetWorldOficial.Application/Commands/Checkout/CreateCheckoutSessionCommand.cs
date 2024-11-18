using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetWorldOficial.Application.Commands.Checkout;

public class CreateCheckoutSessionCommand : IRequest<StatusCodeResult>
{
}