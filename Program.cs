class Program{

    static void FileParceInterface(out FileRecord[] array){
        while (!(FileRecord.ParseFileCSV("abcnews-date-text-short.csv", out array))){
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
        SortingParameter<FileRecord>[] par = new SortingParameter<FileRecord>[] {
            new SortingParameter<FileRecord>("by date", ConsoleKey.D, (x, y) => x.Date.CompareTo(y.Date)),
            new SortingParameter<FileRecord>("by name", ConsoleKey.N, (x, y) => x.Name.CompareTo(y.Name))
        };
        
        Validator<FileRecord>[] val = new Validator<FileRecord>[]{
            new FinderParameter<FileRecord, string> ( (i, val) => i.Name.Contains(val), i => i, "Enter value:")
        };

        (new Screen<FileRecord>(array, val)).Iterate();


    }
}