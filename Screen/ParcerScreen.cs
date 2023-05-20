using DataConsoleViewer.CSVParser;

static class ParserScreen<T>
{
    /// <summary>
    /// Реализует диалог с пользователем для чтения данных из файла
    /// </summary>
    public static T[] Parse(CSVParser<T> parser)
    {
        Console.Write("Enter file name: ");
        try
        {
            parser.ReadFile(Console.ReadLine());
            T[] result = parser.Parse();
            Console.WriteLine("File Parsed");
            Console.Write("Enter any key to continue: ");
            Console.ReadKey();
            Console.Clear();
            return result;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error occured. File not parced.");
            Console.Write("Enter y to try again: ");
            ConsoleKey key = Console.ReadKey().Key;
            Console.WriteLine();
            if (key == ConsoleKey.Y)
                return Parse(parser);
            else 
                throw e;
        }
    }
}
