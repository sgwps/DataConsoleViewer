using System.Text.Json.Serialization;
using DataConsoleViewer.JsonConverter;

namespace DataConsoleViewer.Entities;



public class Publication
{
    [JsonConverter(typeof(DateOnlyJsonConverter))]

    /// <summary>
    /// Дата публикации (1-ый столбец CSV файла)
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    /// Заголовок публикации (2-ой столбец CSV файла)
    /// </summary>
    public String Name { get; private set; }

    public Publication(DateOnly date, string name)
    {
        Date = date;
        Name = name;
    }


    /// <summary>
    /// Описание публикации, выводимое на экран
    /// </summary>
    public override string ToString()
    {
        return $"{Date}: {Name}";
    }
}
