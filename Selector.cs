class Selector<ElementType, ParamType> {
    ParamType parameter;

    Func<ElementType, ParamType, bool> _predicateWithParams;

    public bool ItemChecker(ElementType item)
    {
        return _predicateWithParams(item, parameter);
    }

    public Selector(SelectingOption<ElementType, ParamType> option, string input){
        parameter = option.ConvertInput(input);
        _predicateWithParams = option.SelectingPredicate;


    }
}




