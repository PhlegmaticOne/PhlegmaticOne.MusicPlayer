namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;

public interface IObservableQueue<T> : IMoveableQueue<T> where T : class
{
    public event EventHandler<CollectionChangedEventArgs<T>> QueueChanged;
}