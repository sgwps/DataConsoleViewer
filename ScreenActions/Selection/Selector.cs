namespace DataConsoleViewer.ScreenActions.Selection;

class Selector<ElementType, ParamType> {
    ParamType _parameter;

    Func<ElementType, ParamType, bool> _parametrizedPredicate;

    public bool ItemChecker(ElementType item)
    {
        return _parametrizedPredicate(item, _parameter);
    }

    public Selector(ParamType parameter, Func<ElementType, ParamType, bool> predicate){
        _parameter = parameter;
        _parametrizedPredicate = predicate;


    }
}




