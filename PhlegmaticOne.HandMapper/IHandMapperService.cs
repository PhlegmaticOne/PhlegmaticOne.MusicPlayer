namespace PhlegmaticOne.HandMapper;

public interface IHandMapperService
{
    TOut? Map<TOut>(object from);
    IHandMapperService AddParameter(string key, object value);
}