namespace PetWorldOficial.Application.Extensions;

public static class CPFFormatterExtension
{
    public static string MaskCPF(this string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return cpf;
        
        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        
        if (cpf.Length == 11)
        {
            return string.Format("{0}.{1}.{2}-{3}",
                cpf.Substring(0, 3),
                cpf.Substring(3, 3),
                cpf.Substring(6, 3),
                cpf.Substring(9, 2));
        }

        return cpf; 
    }
}