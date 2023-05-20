using System.Text.Json.Serialization;
using DataConsoleViewer.JsonConverter;

namespace DataConsoleViewer.Entities;



public class Publication
{
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Date { get; private set; }
    public String Name { get; private set; }

    public Publication(DateOnly date, string name)
    {
        Date = date;
        Name = name;
    }

    public override string ToString()
    {
        return $"{Date}: {Name}";
    }
}
