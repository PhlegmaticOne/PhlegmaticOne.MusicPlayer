using System.Diagnostics;

namespace PhlegmaticOne.HandMapper;

public abstract class HandMapperBase<TIn, IOut> : IHandMapper<TIn, IOut>
{
    protected readonly Dictionary<string, object> Parameters;
    public HandMapperBase() => Parameters = new();

    public IHandMapper AddParameters(IDictionary<string, object> parameters)
    {
        foreach(var parameter in parameters)
        {
            Parameters.Add(parameter.Key, parameter.Value);
        }
        return this;
    }

    public abstract IOut? Map(TIn from);
    public object? Map(object from)
    {
        var mapped = Map((TIn)from);
        Parameters.Clear();
        return mapped;
    }
}