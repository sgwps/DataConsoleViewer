class Program{

    static void FileParceInterface(out FileRecord[] array){
        while (!(FileRecord.ParseFileCSV("abcnews-date-text.csv", out array))){
            Console.WriteLine("Error occured. File not parced.");
            Console.Write("Enter y to continue: ");
            ConsoleKey key = Console.ReadKey().Key;
            Console.WriteLine();
            if (key != ConsoleKey.Y) break;
        }
        Console.WriteLine("File Parsed");
        Console.Write("Enter any key to continue: ");
        Console.ReadKey();
        Console.Clear();
    }




    static void Main(){
        FileParceInterface(out FileRecord[] array);


        IScreenAction[] actions = new IScreenAction[] {
            new SortingOption<FileRecord>((x, y) => x.Date.CompareTo(y.Date), new MenuOption(ConsoleKey.D, "sort by date")),
            new SortingOption<FileRecord>((x, y) => x.Name.CompareTo(y.Name), new MenuOption(ConsoleKey.N, "sort by name")),
            new SelectingOption<FileRecord, string>((x, y) => x.Name.Split(" ").Contains(y), i => i, new MenuOption(ConsoleKey.F, "find by name"), "enter the key word"),
            new SelectingOption<FileRecord, DateOnly>((x, y) => x.Date.Month == y.Month & x.Date.Year == y.Year, i => new DateOnly(int.Parse(i[..4]), int.Parse(i[4..6]), 1), new MenuOption(ConsoleKey.O, "find by date"), "Enter month in format YYYYMM")

        };
        

        (new Screen<FileRecord>(array, actions)).Iterate();


    }
}