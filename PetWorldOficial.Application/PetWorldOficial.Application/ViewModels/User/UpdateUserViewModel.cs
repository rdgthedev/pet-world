using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.ViewModels.User;

public class UpdateUserViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Gender { get; set; }
    
    [Required(ErrorMessage = "O documento é obrigatório!")]
    public string Document { get; set; }
    
    [Required(ErrorMessage = "A data é obrigatória!")]
    public DateTime? BirthDate { get; set; }
    
    [Required(ErrorMessage = "O email é obrigatório!")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O telefone é obrigatório!")]
    public string PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "A rua é obrigatória!")]
    public string Street { get; set; }
    
    [Required(ErrorMessage = "O número é obrigatório!")]
    public int Number { get; set; }
    
    [Required(ErrorMessage = "O complemento é obrigatório!")]
    public string Complement { get; set; }
    
    [Required(ErrorMessage = "O CEP é obrigatório!")]
    public string PostalCode { get; set; }
    
    [Required(ErrorMessage = "O bairro é obrigatório!")]
    public string Neighborhood { get; set; }
    
    [Required(ErrorMessage = "A cidade é obrigatória!")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "O estado é obrigatório!")]
    public string State { get; set; }
}