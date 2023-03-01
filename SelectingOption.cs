class SelectingOption<ElementType, ParamType> : ISelector<ElementType>, IScreenAction
{
    
    MenuOption _menuOption;
    public MenuOption MenuOption {
        get{
            return _menuOption;
        }
    }

    string _instruction;
    public string Instruction{
        get{ return _instruction; }
    }

    public Type SelectorParameterType {
        get{
            return typeof(ParamType);
        }
    }


    public Func<ElementType, ParamType, bool> SelectingPredicate;

    public Func<string, ParamType> ConvertInput;


    public SelectingOption (Func<ElementType, ParamType, bool> selectingPredicate, Func<string, ParamType> inputConverter, MenuOption menuOption, string instruction){
        SelectingPredicate = selectingPredicate;
        ConvertInput = inputConverter;
        _menuOption = menuOption;
        _instruction = instruction;
    }


}