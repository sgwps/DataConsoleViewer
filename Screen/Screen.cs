using System.Reflection;
using System.Text.Json;
using DataConsoleViewer.ScreenActions;

namespace DataConsoleViewer.Screen;

class Screen<T>
{
    IScreenAction<T>[] _actions = new IScreenAction<T>[] { };

    /// <summary>
    /// Cписок объектов, с которыми работает программа
    /// <summary/>
    ScreenCollection<T> Data;

    public Screen(T[] array, IScreenAction<T>[] actions)
    {
        int screenLength = Console.WindowHeight - 5 - actions.Length - 3;
        Data = new ScreenCollection<T>(array, screenLength);
        _actions = actions;
    }

    public void HandleAction(IScreenAction<T> action)
    {
        Console.Clear();
        try
        {
            T[] newArray = action.HandleData(Data.Data);
            (new Screen<T>(newArray, _actions)).Iterate();
        }
        catch (ArgumentException e)
        {
            return;
        }
    }

    /// <summary>
    /// Пролистывание объектов
    /// <summary/>
    void Scroll(int count)
    {
        Data.First += count;
    }

    /// <summary>
    /// Выгрузка в JSON
    /// <summary/>
    void Upload()
    {
        Console.Clear();
        Console.Write("Enter filename: ");
        try
        {
            Data.SaveToJSON(Console.ReadLine());
            Console.WriteLine("File written");
        }
        catch (IOException)
        {
            Console.WriteLine("Error occured. File not writter");
            Console.Write("Enter a to try again, enter any key to exit file writer");
            if (Console.ReadKey().Key == ConsoleKey.A)
                Upload();
        }
    }

    static string menu =
        @"
    Enter s to scroll;
    Enter t to scroll up;
    Enter u to upload data to json;
    Enter e to exit;
    ";

    /// <summary>
    /// Пользовательское меню в консоли
    /// <summary/>
    string Menu
    {
        get
        {
            string[] sorting = Array.ConvertAll<IScreenAction<T>, string>(
                _actions,
                x => x.MenuOption.ToString()
            );
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
            Data.PrintData();
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
                    Upload();
                    break;
                case (ConsoleKey.E):
                    return;
                default:
                    IScreenAction<T>? action = _actions.FirstOrDefault<IScreenAction<T>>(
                        x => x.Key == key
                    );
                    if (!(action is null))
                    {
                        HandleAction(action);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input.");
                    }
                    continue;
            }
            Console.Write("Press any key to continue: ");
            key = Console.ReadKey().Key;
        }
    }
}
