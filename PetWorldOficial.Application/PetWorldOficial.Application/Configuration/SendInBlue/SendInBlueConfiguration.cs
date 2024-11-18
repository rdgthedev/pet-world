namespace PetWorldOficial.Application.Configuration.SendInBlue;

public class SendInBlueConfiguration
{
    public string SMTP { get; set; } = string.Empty;
    public int Port { get; set; } 
    public string From { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}