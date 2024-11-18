using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Queries.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Order;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using Exception = System.Exception;

namespace PetWorldOficial.Application.Handlers.Order;

public class GetAllOrdersQueryHandler(
    IOrderService orderService,
    IUserService userService,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDetailsViewModel>>
{
    public async Task<IEnumerable<OrderDetailsViewModel>> Handle(GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var claimsPrincipal = httpContextAccessor.HttpContext.User;

            var email = claimsPrincipal.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            var client = await userService.GetByEmailAsync(email, cancellationToken);

            if (client is null)
                throw new UserNotFoundException("Faça o login ou cadastre-se no site!");

            var orders = Enumerable.Empty<OrderDetailsViewModel>();

            if (claimsPrincipal.IsInRole(ERole.Admin.ToString()))
                orders = await orderService.GetAllAsync(cancellationToken);
            else
                orders = await orderService.GetAllByClientId(client.Id, cancellationToken);

            if (!orders.Any())
                throw new OrderNotFoundException("Nenhum pedido encontrado!");

            return orders.OrderByDescending(o => o.CreatedAt).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}