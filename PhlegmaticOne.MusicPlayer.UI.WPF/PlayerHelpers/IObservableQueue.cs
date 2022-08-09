using System;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

public interface IObservableQueue<T> : IMoveableQueue<T> where T : class
{
    public event EventHandler<CollectionChangedEventArgs<T>> QueueChanged;
}