using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.User;

public class RegisterUserViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    [DisplayName("Nome")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O nome de usuário é obrigatório!")]
    [DisplayName("Nome de usuário")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório!")]
    [DisplayName("Gênero")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "A data de aniversário é obrigatória!")]
    [DisplayName("Data de aniversário")]
    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessage = "O documento é obrigatório!")]
    [DisplayName("Documento")]
    public string Document { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória!")]
    [DisplayName("Senha")]
    public string Password { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória!")]
    [EmailAddress]
    [DisplayName("Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória!")]
    [Phone]
    [DisplayName("Celular")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "A rua é obrigatória!")]
    [DisplayName("Rua")]
    public string Street { get; set; }

    [Required(ErrorMessage = "O número é obrigatório!")]
    [DisplayName("Número")]
    public int? Number { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório!")]
    [DisplayName("CEP")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório!")]
    [DisplayName("Bairro")]
    public string Neighborhood { get; set; }

    [Required(ErrorMessage = "O complemento é obrigatório!")]
    [DisplayName("Complement")]
    public string Complement { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória!")]
    [DisplayName("Cidade")]
    public string City { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório!")]
    [DisplayName("Estado")]
    public string State { get; set; }
}