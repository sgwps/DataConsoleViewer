using DataConsoleViewer.Entities;
using DataConsoleViewer.Screen;
using DataConsoleViewer.ScreenActions;
using DataConsoleViewer.ScreenActions.Selection;
using DataConsoleViewer.ScreenActions.Sorting;

class Program
{
    static void FileParceInterface(out FileRecord[] array)
    {
        while (!(FileRecord.ParseFileCSV("abcnews-date-text.csv", out array)))
        {
            Console.WriteLine("Error occured. File not parced.");
            Console.Write("Enter y to continue: ");
            ConsoleKey key = Console.ReadKey().Key;
            Console.WriteLine();
            if (key != ConsoleKey.Y)
                break;
        }
        Console.WriteLine("File Parsed");
        Console.Write("Enter any key to continue: ");
        Console.ReadKey();
        Console.Clear();
    }

    //добавить RangeOption


    static void Main()
    {
        FileParceInterface(out FileRecord[] array);

        IScreenAction<FileRecord>[] actions = new IScreenAction<FileRecord>[]
        {
            new SortingAction<FileRecord>(
                (x, y) => x.Date.CompareTo(y.Date),
                new MenuOption(ConsoleKey.D, "sort by date")
            ),
            new SortingAction<FileRecord>(
                (x, y) => x.Name.CompareTo(y.Name),
                new MenuOption(ConsoleKey.N, "sort by name")
            ),
            new SelectingAction<FileRecord, string>(
                (x, y) => x.Name.Split(" ").Contains(y),
                i => new Tuple<bool, string>(true, i),
                new MenuOption(ConsoleKey.F, "find by name"),
                "enter the key word"
            ),
            new SelectingAction<FileRecord, DateOnly>(
                (x, y) => x.Date.Month == y.Month & x.Date.Year == y.Year,
                i =>
                {
                    try
                    {
                        return new Tuple<bool, DateOnly>(
                            true,
                            new DateOnly(int.Parse(i[..4]), int.Parse(i[4..6]), 1)
                        );
                    }
                    catch (ArgumentException e)
                    {
                        return new Tuple<bool, DateOnly>(
                            false,
                            new DateOnly(int.Parse(i[..4]), int.Parse(i[4..6]), 1)
                        );
                    }
                },
                new MenuOption(ConsoleKey.O, "find by date"),
                "Enter month in format YYYYMM"
            )
        };

        (new Screen<FileRecord>(array, actions)).Iterate();
    }
}
