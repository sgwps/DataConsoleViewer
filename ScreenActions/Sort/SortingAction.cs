namespace DataConsoleViewer.ScreenActions.Sorting;

class SortingAction<T> : IScreenAction<T>
{
    /// <summary>
    /// Предикат сравнения
    /// <summary/>
    Comparison<T> _comparer;

    public MenuOption MenuOption { get; }

    /// <param name="comparer">Предикат сравнения</param>
    public SortingAction(Comparison<T> comparer, MenuOption menuOption)
    {
        _comparer = comparer;
        MenuOption = menuOption;
    }

    public T[] HandleData(in T[] initialData)
    {
        T[] result = new T[initialData.Length];
        initialData.CopyTo(result, 0);
        Array.Sort<T>(result, _comparer);
        return result;
    }
}
