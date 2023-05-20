namespace DataConsoleViewer.ScreenActions;

/// <summary>
/// Переводит строки в необходимые типы данных
/// </summary>
static class InputConverters
{
    public static Tuple<bool, string> ParseStringInput(string value)
    {
        return new Tuple<bool, string>(true, value);
    }

    public static Tuple<bool, DateOnly> ParseDateYYYYMM(string value)
    {
        try
        {
            return new Tuple<bool, DateOnly>(
                true,
                new DateOnly(int.Parse(value[..4]), int.Parse(value[4..6]), 1)
            );
        }
        catch (ArgumentException e)
        {
            return new Tuple<bool, DateOnly>(
                false,
                new DateOnly()
            );
        }
    }

    public static Tuple<bool, DateOnly> ParseDateYYYYMMDD(string value)
    {
        try
        {
            return new Tuple<bool, DateOnly>(
                true,
                new DateOnly(int.Parse(value[..4]), int.Parse(value[4..6]), int.Parse(value[6..8]))
            );
        }
        catch (ArgumentException e)
        {
            return new Tuple<bool, DateOnly>(
                false,
                new DateOnly()
            );
        }
    }

}
