namespace PhlegmaticOne.HandMapper;

public class HandMapperService : IHandMapperService
{
    private readonly Dictionary<string, object> _parameters;
    private readonly IEnumerable<IHandMapper> _handMappers;

    public HandMapperService(IEnumerable<IHandMapper> handMappers)
    {
        _handMappers = handMappers;
        _parameters = new();
    }
    public IHandMapperService AddParameter(string key, object value)
    {
        _parameters.Add(key, value);
        return this;
    }

    public TOut? Map<TOut>(object from)
    {
        var fromType = from.GetType();
        var toType = typeof(TOut);
        var mapper = _handMappers.FirstOrDefault(m =>
        {
            var generics = m.GetType().BaseType!.GetGenericArguments();
            return fromType == generics[0] && toType == generics[1];
        });

        TOut? mapped = default;
        if(mapper is not null)
        {
            mapper.AddParameters(_parameters);
            mapped = (TOut)mapper.Map(from);
        }
        _parameters.Clear();
        return mapped;
    }
}
