namespace DataConsoleViewer.ScreenActions;

/// <summary>
/// Реализует консольный интерфейс для считывания данных от пользователя
/// </summary>
static class InputParser {
    public static bool GetBoolInput(string instruction){
        Func<string, Tuple<bool, bool>> converter = (input) => {
            switch (input.ToUpper()) {
                case("Y"):
                    return new Tuple<bool, bool>(true, true);
                case("N"):
                    return new Tuple<bool, bool>(true, false);
                default:
                    return new Tuple<bool, bool>(false, false);
            }
        };
        return GetParamType<bool> (converter, instruction);
    }

    public static ParamType GetParamType<ParamType> (Func<string, Tuple<bool, ParamType>> converter, string instruction) {
        Tuple<bool, ParamType> tuple;
        Console.WriteLine(instruction);
        while (!(tuple = converter(Console.ReadLine())).Item1) {
            Console.WriteLine("Invalid input, press enter to continue, press any key to exit");
            if (Console.ReadKey().Key != ConsoleKey.Enter) {
                throw new ArgumentException();
            }
            Console.WriteLine(instruction);
        }
        return tuple.Item2;
    }
}