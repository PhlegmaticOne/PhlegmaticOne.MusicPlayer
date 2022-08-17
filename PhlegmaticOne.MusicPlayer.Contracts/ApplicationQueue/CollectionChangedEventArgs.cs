namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;

public class CollectionChangedEventArgs<T> : EventArgs where T : class
{
    public IEnumerable<T> Entities { get; }
    public CollectionChangedType CollectionChangedType { get; }

    public CollectionChangedEventArgs(IEnumerable<T> entities, CollectionChangedType collectionChangedType)
    {
        Entities = entities;
        CollectionChangedType = collectionChangedType;
    }
}