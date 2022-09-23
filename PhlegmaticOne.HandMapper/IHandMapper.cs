namespace PhlegmaticOne.HandMapper;

public interface IHandMapper<TIn, TOut> : IHandMapper
{
    TOut? Map(TIn from);
}

public interface IHandMapper 
{
    object? Map(object from);
    IHandMapper AddParameters(IDictionary<string, object> parameters);
}