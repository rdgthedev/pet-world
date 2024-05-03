namespace PetWorldOficial.Application.DTOs;

public class ResultDTO<TData> where TData : class
{
    public TData Data { get; private set; }
    
    public ResultDTO(TData data)
       => Data = data;
}