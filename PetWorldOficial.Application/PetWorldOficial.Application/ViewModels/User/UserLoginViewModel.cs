using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.User;

public class UserLoginViewModel
{
    [Required(ErrorMessage = "O nome de usuário é obrigatório!")]
    [DisplayName("Nome de usuário")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage="A senha é obrigatória!")]
    [DisplayName("Senha")]
    public string Password { get; set; }
    
    [DisplayName("Lembrar-me")]
    public bool? RememberMe { get; set; }
}