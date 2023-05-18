namespace DataConsoleViewer.ScreenActions;

interface IScreenAction<T>
{
    MenuOption MenuOption { get; }

    sealed ConsoleKey Key
    {
        get { return MenuOption.Key; }
    }

    T[] HandleData(in T[] initialData);

}
