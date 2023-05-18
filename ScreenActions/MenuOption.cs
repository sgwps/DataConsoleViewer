namespace DataConsoleViewer.ScreenActions;

/// <summary>
/// Соотносит клавишу и команды
/// <summary/>
class MenuOption
{
    /// <summary>
    /// Клавиша для вызова команды
    /// <summary/>
    public ConsoleKey Key { get; }

    /// <summary>
    /// Описание команды
    /// <summary/>
    string _action;

    public override string ToString()
    {
        return $"Enter {Key} to {_action}";
    }

    public MenuOption(ConsoleKey key, string action)
    {
        Key = key;
        _action = action;
    }
}
