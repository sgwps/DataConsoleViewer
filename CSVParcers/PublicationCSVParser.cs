using DataConsoleViewer.Entities;

namespace DataConsoleViewer.CSVParser;

class PublicationCSVParser : CSVParser<Publication>
{
    private Publication ParseLine(string line)
    {
        try{
        string[] splited = line.Split(',');
        DateOnly date = DateOnly.ParseExact(splited[0], "yyyyMMdd");

        return new Publication(date, splited[1]);
        } catch (Exception ex) when (ex is ArgumentException || ex is IndexOutOfRangeException) {
            throw exception;
        }
    }


    public override Publication[] Parse(){
        Publication[] result = new Publication[lines.Length];
        for (int i = 0; i < result.Length; i++) {
            result[i] = ParseLine(lines[i]);
        }
        return result;
    }
    public PublicationCSVParser() : base() { }
}
