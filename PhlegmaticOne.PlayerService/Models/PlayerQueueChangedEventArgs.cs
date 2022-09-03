namespace PhlegmaticOne.PlayerService.Models;

public class PlayerQueueChangedEventArgs<T> : EventArgs where T : class
{
    public IEnumerable<T> Entities { get; }
    public PlayerQueueChangedType CollectionChangedType { get; }

    public PlayerQueueChangedEventArgs(IEnumerable<T> entities, PlayerQueueChangedType collectionChangedType)
    {
        Entities = entities;
        CollectionChangedType = collectionChangedType;
    }
}