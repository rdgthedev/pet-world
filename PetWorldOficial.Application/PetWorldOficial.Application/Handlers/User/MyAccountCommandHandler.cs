using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class MyAccountCommandHandler(
    IUserService userService,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : IRequestHandler<MyAccountCommand, MyAccountCommand>
{
    public async Task<MyAccountCommand> Handle(MyAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                var email = httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            
                var user = await userService.GetByEmailAsync(email, cancellationToken);

                if (user is null)
                    throw new UserNotFoundException("Usuário não encontrado!");

                request.Id = user.Id;
                request.Name = user.Name;
                request.Email = user.Email;
                request.Document = user.Document;
                request.PhoneNumber = user.PhoneNumber;
                request.Gender = user.Gender;
                request.BirthDate = user.BirthDate;
                request.Street = user.Street;
                request.Number = user.Number;
                request.Complement = user.Complement;
                request.Neighborhood = user.Neighborhood;
                request.City = user.City;
                request.State = user.State;
                request.PostalCode = user.PostalCode;
                request.PasswordHash = user.PasswordHash;
                request.ConcurrencyStamp = user.ConcurrencyStamp;

                return request;
            }

            if (request.Password != null)
                await userService.UpdatePasswordAsync(request);
            
            await userService.UpdateAsync(mapper.Map<UpdateUserCommand>(request), cancellationToken);
            request.Message = "Dados alterados com sucesso!";
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}