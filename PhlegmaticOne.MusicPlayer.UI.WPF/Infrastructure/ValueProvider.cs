using System;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;

public class ValueProvider<T> : IValueProvider<T> where T : class
{
    private T? _value;
    public T? Get() => _value;
    public void Set(T? value)
    {
        _value = value;
        ValueChanged?.Invoke(this, _value);
    }

    public event EventHandler<T?>? ValueChanged;
}