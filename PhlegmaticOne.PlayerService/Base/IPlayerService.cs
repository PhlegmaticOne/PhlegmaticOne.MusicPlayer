using PhlegmaticOne.Players.Base;
using PhlegmaticOne.PlayerService.Models;

namespace PhlegmaticOne.PlayerService.Base;


public interface IPlayerService<T> : ICollection<T>, IDisposable where T : class, IHaveUrl
{
    RepeatType RepeatType { get; set; }
    ShuffleType ShuffleType { get; set; }
    T? CurrentEntityInPlayer { get; set; }
    IPlayer Player { get; }

    void SetAndPlay(T entity);
    void MoveNext(QueueMoveType queueMoveType);
    void MovePrevious();
    void AddRange(IEnumerable<T> entities);

    event EventHandler<T> CurrentEntityChanged;
    event EventHandler<PlayerQueueChangedEventArgs<T>> EntitiesChanged;
}