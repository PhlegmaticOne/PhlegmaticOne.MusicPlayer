using System;
using System.Collections.Generic;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

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