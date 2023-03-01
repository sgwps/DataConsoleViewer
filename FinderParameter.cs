public delegate bool finderWithType<T, T1>(T item, T1 parameter);


interface Validator<T> {
    bool Compare(T item);

    void GetInput(string input);

    string Output {get;}

}


class FinderParameter<ElementType, ParamType> : Validator<ElementType>
{
    ParamType _parameter;

    Func<ElementType, ParamType, bool> CheckerWithParam;


    public bool Compare(ElementType item)
    {
        return CheckerWithParam(item, _parameter);
    }

    public void GetInput(string input)
    {
        _parameter = Converter(input);
    }

    Func<string, ParamType> Converter;

    string _output;
    public string Output{
        get {return _output;}
    }


    public FinderParameter (Func<ElementType, ParamType, bool> checkerWithParam, Func<string, ParamType> converter, string output){
        CheckerWithParam = checkerWithParam;
        Converter = converter;
        _output = output;
    }


}