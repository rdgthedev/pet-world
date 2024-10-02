using System.Reflection.Metadata.Ecma335;

namespace PetWorldOficial.Application.Settings;

public class Settings
{
    public DefaultRange Range { get; set; }

    public class DefaultRange
    {
        public int DefaultRangeGrooming { get; set; }
        public int DefaultRangeVeterinary { get; set; }
    }
}