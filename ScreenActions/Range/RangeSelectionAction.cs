namespace DataConsoleViewer.ScreenActions.RangeSelection;

class RangeSelectionAction<ElementType, ParamType> : IScreenAction<ElementType> where ParamType : IComparable<ParamType>
{
    public MenuOption MenuOption {get;}
    Func<string, Tuple<bool, ParamType>> InputConverter;
    string _instructionMin;
    string _instructionMax;

    private Func<ElementType, ParamType> ParameterGetter;

    public ElementType[] HandleData(in ElementType[] initialData)
    {
        (ParamType, ParamType) parametres = GetUserInput();
        (bool, bool) borders = GetBorderIncludedInput();
        RangeSelector<ElementType, ParamType> selector = new RangeSelector<ElementType, ParamType>(ParameterGetter, parametres.Item1, parametres.Item2, borders.Item1, borders.Item2);
        ElementType[] result = initialData.Where<ElementType>(selector.Check).ToArray<ElementType>();
        return result;
    }

    public (bool, bool) GetBorderIncludedInput(){
        bool min = InputParser.GetBoolInput("Do ypu want to include minimal border in range? y/n");
        bool max = InputParser.GetBoolInput("Do ypu want to include minimal border in range? y/n");
        return (min, max);
    }

    public (ParamType, ParamType) GetUserInput(){
        ParamType min = InputParser.GetParamType<ParamType>(InputConverter, _instructionMin);
        ParamType max = InputParser.GetParamType<ParamType>(InputConverter, _instructionMax);
        return (min, max);

    }

    public RangeSelectionAction(MenuOption menuOption, Func<string, Tuple<bool, ParamType>> inputConverter) {
        MenuOption = menuOption;
        inputConverter = inputConverter;
    }
}