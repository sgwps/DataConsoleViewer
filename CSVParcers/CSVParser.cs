namespace DataConsoleViewer.CSVParser;

abstract class CSVParser<T>
{
    /// <summary>
    /// Строки CSV файла
    /// </summary>
    protected string[] lines;

    /// <summary>
    /// Исключение, выбрасываемое при ошибке чтения данных
    /// </summary>
    protected Exception exception = new ArgumentException("Invalid file");

    public CSVParser(){

    }

    /// <summary>
    /// Считывание файла в массив сток
    /// </summary>
    /// <param name="filename">Имя CSV-файла</param>
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

    /// <summary>
    /// Парсинг элементов. Перед вызовом данного метода необходимо вызвать ReadFile()
    /// </summary>
    /// <returns>Массив элементов, считанных из файла</returns>
    public abstract T[] Parse();
}
