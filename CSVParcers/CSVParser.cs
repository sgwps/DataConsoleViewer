namespace DataConsoleViewer.CSVParser;

abstract class CSVParser<T>
{
    protected string[] lines;
    protected Exception exception = new ArgumentException("Invalid file");
    public CSVParser(){

    }

    public void ReadFile(string filename)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                sr.ReadLine();
                lines = sr.ReadToEnd().Split('\n');
            }
        }
        catch (IOException e)
        {
            throw exception;
        }
    }

    public abstract T[] Parse();
}
