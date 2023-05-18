namespace DataConsoleViewer.ScreenActions.Selection;

class SelectingAction<ElementType, ParamType> : IScreenAction<ElementType>
{
    public MenuOption MenuOption
    {
        get;
    }

    private string _instruction;


    public ElementType[] HandleData (in ElementType[] array)
    {
        Selector<ElementType, ParamType> selector = ConsoleDialogue();
        return array.Where<ElementType>(selector.ItemChecker).ToArray<ElementType>();
    }

    public virtual Selector<ElementType, ParamType> ConsoleDialogue(){
        Console.Write($"{_instruction}: ");
        String input = Console.ReadLine();
        Selector<ElementType, ParamType> selector = new Selector<ElementType, ParamType>(_converter(input), _predicate);
        Console.Clear();
        return selector;
    }

    private Func<ElementType, ParamType, bool> _predicate;

    private Func<string, ParamType> _converter;

    public SelectingAction(
        Func<ElementType, ParamType, bool> predicate,
        Func<string, ParamType> converter,
        MenuOption menuOption,
        string instruction
    )
    
    {
        _predicate = predicate;
        _converter = converter;
        MenuOption = menuOption;
        _instruction = instruction;
    }
}
