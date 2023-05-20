namespace DataConsoleViewer.ScreenActions;

/// <summary>
/// Действие для обработки массива данных
/// </summary>
interface IScreenAction<T>
{
    MenuOption MenuOption { get; }

    sealed ConsoleKey Key
    {
        get { return MenuOption.Key; }
    }

    T[] HandleData(in T[] initialData);

}
