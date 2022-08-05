using System;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;

public interface IValueProvider<T> where T : class
{
    public T? Get();
    public void Set(T? value);
    public event EventHandler<T?> ValueChanged;
}