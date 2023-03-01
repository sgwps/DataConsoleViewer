interface IScreenAction{
    MenuOption MenuOption {get;}

    sealed ConsoleKey Key {get {
        return MenuOption.Key;
    }}

}