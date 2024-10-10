namespace PetWorldOficial.Application.DTO;

public record TimeDTO(TimeSpan Time, bool Status)
{
    public bool Status { get; set; }
}