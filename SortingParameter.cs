class SortingParameter<T>
{

    /// <summary>
    /// Строка для вывода на экран, показывает по какому праметру сортируем
    /// <summary/>
    string _screenOutput;
    public string ScreenOutput
    {
        get { return $"Enter {Key.ToString().ToLower()} to sort {_screenOutput}"; }
    }

    /// <summary>
    /// Клавиша для данной сортировки
    /// <summary/>
    public ConsoleKey Key { get; private set; }


    /// <summary>
    /// Предикат сравнения
    /// <summary/>
    public Comparison<T> Comparer { get; private set; }


    /// <param name="screenOutput">Строка для вывода на экран</param>
    /// <param name="key">Клавиша для данной сортировки</param>
    /// <param name="comarer">Предикат сравнения</param>
    public SortingParameter(string screenOutput, ConsoleKey key, Comparison<T> comarer)
    {
        _screenOutput = screenOutput;
        Key = key;
        Comparer = comarer;
    }


    
}
