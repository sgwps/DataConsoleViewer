class MenuOption {
    ConsoleKey _key;

    public ConsoleKey Key{
        get{ return _key; }
    }

    string _action;

    public override string ToString()
    {
        return $"Enter {Key} to {_action}";
    }

    public MenuOption(ConsoleKey key, string action){
        _key = key;
        _action = action;
    }
}