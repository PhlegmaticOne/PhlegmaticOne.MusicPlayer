namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;

public interface IMoveableQueue<T> : IEnumerable<T> where T : class
{
    public RepeatType RepeatType { get; set; }
    public ShuffleType ShuffleType { get; set; }
    public void Enqueue(T song);
    public void Enqueue(IEnumerable<T> songs);
    public void Remove(T song);
    public bool Contains(T item);
    public T? Current { get; set; }
    public void MoveNext(QueueMoveType queueMoveType);
    public void MovePrevious();
    public void Clear();
}