using System.Reflection;
using System.Text.Json;
using DataConsoleViewer.ScreenActions;

class Screen<T>
{
    IScreenAction<T>[] _actions = new IScreenAction<T>[] { };

    /// <summary>
    /// Cписок объектов, с которыми работает программа
    /// <summary/>
    T[] _array;

    
    public void HandleAction(IScreenAction<T> action) {
        T[] newArray = action.HandleData(_array);
        (new Screen<T>(newArray, _actions)).Iterate();
    }

    /// <param name="arr">Список объектов для работы в консоли</param>
    public Screen(T[] arr)
    {
        _array = arr;
    }

    public Screen(T[] arr, IScreenAction<T>[] actions)
    {
        _array = arr;
        _actions = actions;
    }

    /// <summary>
    /// Первый объект, выводящийся на экран
    /// <summary/>
    int _first = 0;
    int First
    {
        get { return _first; }
        set
        {
            if (!(value < 0 || value >= _array.Length))
                _first = value;
        }
    }

    /// <summary>
    /// Количество объектов, выводящихся на экран
    /// <summary/>
    int _len
    {
        get { return Console.WindowHeight - 5 - _actions.Length - 3; }
    }

    /// <summary>
    /// Индекс, до которого объекты выводятся на экран
    /// <summary/>
    int ScreenEnd
    {
        get { return Math.Min(First + _len, _array.Length); }
    }

    /// <summary>
    /// Флаг, показывающий, есть ли объекты ниже последнего выведенного
    /// <summary/>
    bool EndFlag
    {
        get { return First + _len >= _array.Length; }
    }

    /// <summary>
    /// Вывод объектов на экран
    /// <summary/>
    void Print()
    {
        for (int i = _first; i < ScreenEnd; i++)
        {
            Console.WriteLine(_array[i]);
        }
        if (EndFlag)
        {
            Console.WriteLine("You reached the end of the file");
        }
    }

    /// <summary>
    /// Пролистывание объектов
    /// <summary/>
    void Scroll(int count)
    {
        First += count;
    }

    /// <summary>
    /// Выгрузка в JSON
    /// <summary/>
    void Upload(int begin, int end)
    {
        Console.Clear();
        Console.Write("Enter filename: ");
        try
        {
            using (FileStream fs = new FileStream(Console.ReadLine(), FileMode.Create))
            {
                JsonSerializer.Serialize(fs, _array[begin..end]);
            }
            Console.WriteLine("File written");
        }
        catch (IOException)
        {
            Console.WriteLine("Eccor occured. File not writter");
            Console.Write("Enter a to try again, enter any key to exit file writer");
            if (Console.ReadKey().Key == ConsoleKey.A)
                Upload(begin, end);
        }
    }

    static string menu =
        @"
    Enter s to scroll;
    Enter t to scroll up;
    Enter u to upload all data;
    Enter a to upload all data on the screen;
    Enter e to exit;
    ";

    /// <summary>
    /// Пользовательское меню в консоли
    /// <summary/>
    string Menu
    {
        get
        {
            string[] sorting = Array.ConvertAll<IScreenAction<T>, string>(_actions, x => x.MenuOption.ToString());
            return menu + String.Join("\n    ", sorting);
        }
    }

    /// <summary>
    /// Цикл работы с программой
    /// <summary/>
    public void Iterate()
    {
        while (true)
        {
            Console.Clear();
            Print();
            Console.WriteLine(Menu);
            ConsoleKey key = Console.ReadKey().Key;
            Console.Clear();
            switch (key)
            {
                case (ConsoleKey.S):
                    Scroll(1);
                    continue;
                case (ConsoleKey.T):
                    Scroll(-1);
                    continue;

                case (ConsoleKey.U):
                    Upload(_first, ScreenEnd);
                    continue;
                case (ConsoleKey.A):
                    Upload(0, _array.Length);
                    continue;
                case (ConsoleKey.E):
                    return;
                default:
                    IScreenAction<T>? action = _actions.FirstOrDefault<IScreenAction<T>>(
                        x => x.Key == key
                    );
                    if (!(action is null)) {
                        HandleAction(action);
                    }
                    
                    else{
                        Console.WriteLine("Invalid Input.");
                    }
                    continue;
            }
            Console.Write("Press any key to continue: ");
            key = Console.ReadKey().Key;
        }
    }
}