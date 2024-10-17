using System.Reflection.Metadata.Ecma335;
using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Race : Entity
{
    public Race()
    {
        
    }
    public Race(string name)
        => Name = name;

    public string Name { get; private set; }
    public List<Animal> Animals { get; private set; }
}