using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserDetailsViewModel> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<UserDetailsViewModel?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);

    Task<OutputUserExistsDTO> UserExistsAsync(
        string? userName,
        string? cpf,
        string? phoneNumber,
        string? email,
        CancellationToken cancellationToken);

    Task UpdateAsync(UpdateUserCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteUserCommand command, CancellationToken cancellationToken);
}