using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "YYYY-MM-DD";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}

public class FileRecord
{
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Date { get; private set; }
    public String Name { get; private set; }


    /// <exception cref="FormatException">
    /// Выбрасывается при получении строки, невозможной для преобразования в дату
    /// </exception>
    public FileRecord(string date, string name)
    {
        Date = DateOnly.ParseExact(date, "yyyyMMdd");
        Name = name;
    }


    /// <summary>
    /// Чтение строки CSV файла
    /// <summary/>
    private static FileRecord ParseLine(string line){
        string[] splited = line.Split(',');
        return new FileRecord(splited[0], splited[1]);
    }


    /// <summary>
    /// Чтение CSV файла
    /// <summary/>
    /// <returns>
    /// Флаг успеешности чтения файла
    /// </returns>
    static public bool ParseFileCSV(string filePath, out FileRecord[] array)
    {
        try
        {
            string[] lines;
            using (StreamReader sr = new StreamReader(filePath)) {
                sr.ReadLine();
                lines = sr.ReadToEnd().Split('\n');
            }
            array = new FileRecord[lines.Length];
            for (int i = 0; i < lines.Length; i++){
                array[i] = ParseLine(lines[i]);
            }
            return true;

        }
        // жизнь меня многому научила
        catch (IOException)
        {
            array = new FileRecord[0];
            return false;
        }
        catch (IndexOutOfRangeException)
        {
            array = new FileRecord[0];
            return false;
        }
        catch (ArgumentException)
        {
            array = new FileRecord[0];
            return false;
        }
    }

    public override string ToString()
    {
        return $"{Date}: {Name}";
    }
}
