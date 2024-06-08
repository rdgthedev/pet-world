namespace PetWorldOficial.Application.DTOs.User.Input;

public record UserLoginDTO(string UserName, string Password, bool? RememberMe);