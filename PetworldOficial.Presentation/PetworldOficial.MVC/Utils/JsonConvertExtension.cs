using Newtonsoft.Json;

namespace PetworldOficial.MVC.Utils;

public static class JsonConvertExtension
{
    public static string SerializeObject(this object obj)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
    }
}