using Microsoft.AspNetCore.Identity;

namespace PetworldOficial.MVC.Configurations;

public class PortugueseIdentityErrorDescriberConfiguration : IdentityErrorDescriber
{
    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"A senha deve ter pelo menos {length} caracteres."
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "A senha deve conter pelo menos um caractere não alfanumérico."
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "A senha deve conter pelo menos um número."
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "A senha deve conter pelo menos uma letra maiúscula."
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "A senha deve conter pelo menos uma letra minúscula."
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = $"O nome de usuário '{userName}' já está em uso."
        };
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = $"O e-mail '{email}' já está em uso."
        };
    }

    public override IdentityError InvalidToken()
    {
        return new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = "Token inválido."
        };
    }

    public override IdentityError DefaultError()
    {
        return new IdentityError
        {
            Code = nameof(DefaultError),
            Description = "Ocorreu um erro desconhecido."
        };
    }
}
