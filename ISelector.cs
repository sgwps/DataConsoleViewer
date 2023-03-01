interface ISelector<T> : IScreenAction{
    Type SelectorParameterType {get; }
    string Instruction {get;}
}