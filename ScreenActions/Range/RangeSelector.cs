namespace DataConsoleViewer.ScreenActions.RangeSelection;

class RangeSelector<ElementType, ParamType> where ParamType : IComparable<ParamType>{
    ParamType _minValue;
    ParamType _maxValue;
    bool _minIncluded;
    bool _maxIncluded;
    public Func<ElementType, ParamType> ValueToCompare;

    public bool Check(ElementType element) {
        bool minOk = false;
        bool maxOk = false;
        ParamType param = ValueToCompare(element);
        if (_minIncluded) {
            minOk = param.CompareTo(_minValue) >= 0;
        }
        else {
            minOk = param.CompareTo(_minValue) > 0;
        }
        if (_maxIncluded) {
            maxOk = param.CompareTo(_maxValue) <= 0;
        }
        else {
            minOk = param.CompareTo(_maxValue) < 0;
        }
        return minOk && maxOk;
    }

    public RangeSelector(Func<ElementType, ParamType> valueToCompare, ParamType minValue, ParamType maxValue, bool minIncluded, bool maxIncluded) {
        ValueToCompare = valueToCompare;
        _minValue = minValue;
        _maxValue = maxValue;
        _minIncluded = minIncluded;
        _maxIncluded = maxIncluded;
    }


}