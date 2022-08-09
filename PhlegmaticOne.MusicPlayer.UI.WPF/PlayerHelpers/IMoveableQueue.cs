using System.Collections.Generic;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

public interface IMoveableQueue<T> : IEnumerable<T> where T : class
{
    public IReadOnlyCollection<T> Entities { get; }
    public RepeatType RepeatType { get; set; }
    public ShuffleType ShuffleType { get; set; }
    public void Enqueue(T song);
    public void Enqueue(IEnumerable<T> songs);
    public void Remove(T song);
    public T? Current { get; set; }
    public void MoveNext(QueueMoveType queueMoveType);
    public void MovePrevious();
    public void Clear();
}