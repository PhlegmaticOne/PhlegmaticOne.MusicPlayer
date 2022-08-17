namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ValueProviders;

public interface IValueProvider<T> where T : class
{
    public T? Get();
    public void Set(T? value);
    public event EventHandler<T?> ValueChanged;
}