namespace DataConsoleViewer.ScreenActions.Selection;

class SelectingAction<ElementType, ParamType> : IScreenAction<ElementType>
{
    Func<string, Tuple<bool, ParamType>> InputConverter;

    public MenuOption MenuOption
    {
        get;
    }

    private string _instruction;


    public ElementType[] HandleData (in ElementType[] array)
    {
        ParamType parameters = InputParser.GetParamType<ParamType>(InputConverter, _instruction);
        
        Selector<ElementType, ParamType> selector = new Selector<ElementType, ParamType>(parameters, _predicate);
        return array.Where<ElementType>(selector.ItemChecker).ToArray<ElementType>();
    }

    private Func<ElementType, ParamType, bool> _predicate;


    public SelectingAction(
        Func<ElementType, ParamType, bool> predicate,
        Func<string, Tuple<bool, ParamType>> converter,
        MenuOption menuOption,
        string instruction
    )
    
    {
        _predicate = predicate;
        InputConverter = converter;
        MenuOption = menuOption;
        _instruction = instruction;
    }
}
