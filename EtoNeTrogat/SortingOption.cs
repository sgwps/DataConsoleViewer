class SortingOption<T> : IScreenAction
{


    /// <summary>
    /// Предикат сравнения
    /// <summary/>
    public Comparison<T> Comparer { get; private set; }

    MenuOption _menuOption;
    public MenuOption MenuOption {
        get{
            return _menuOption;
        }
    }


    /// <param name="comarer">Предикат сравнения</param>
    public SortingOption(Comparison<T> comparer, MenuOption menuOption)
    {
        Comparer = comparer;
        _menuOption = menuOption;
    }


    
}
