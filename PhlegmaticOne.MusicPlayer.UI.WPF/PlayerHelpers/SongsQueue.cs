using System;
using System.Collections.Generic;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Extensions;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

public class SongsQueue : ISongsQueue
{
    private readonly List<Song> _songs;
    private int _currentSongIndex;
    public SongsQueue()
    {
        _songs = new();
    }

    public IReadOnlyCollection<Song> Songs => _songs;
    public event EventHandler<CollectionChangedEventArgs<Song>>? QueueChanged;
    public void Enqueue(Song song)
    {
        _songs.Add(song);
        Invoke(song.ToOneItemEnumerable(), CollectionChangedType.Added);
    }

    public void Enqueue(IEnumerable<Song> songs)
    {
        var collection = songs.ToList();
        _songs.AddRange(collection);
        Invoke(collection, CollectionChangedType.Added);
    }

    public void Remove(Song song)
    {
        var songIndex = _songs.IndexOf(song);
        if (songIndex < _currentSongIndex)
        {
            _currentSongIndex--;
        }
        _songs.RemoveAt(songIndex);
        Invoke(song.ToOneItemEnumerable(), CollectionChangedType.Removed);
    }

    public Song? Current
    {
        get
        {
            if (_currentSongIndex < _songs.Count)
            {
                return _songs[_currentSongIndex];
            }

            return null;
        }
        set
        {
            var index = _songs.IndexOf(value);
            if (index != -1)
            {
                _currentSongIndex = index;
            }
        }
    }

    public void MoveNext() => _currentSongIndex++;
    public void Clear()
    {
        _songs.Clear();
    }

    private void Invoke(IEnumerable<Song> songs, CollectionChangedType collectionChangedType) =>
        QueueChanged?.Invoke(this, new(songs, collectionChangedType));
}

public class CollectionChangedEventArgs<T> : EventArgs where T: class
{
    public IEnumerable<T> Entities { get; }
    public CollectionChangedType CollectionChangedType { get; }

    public CollectionChangedEventArgs(IEnumerable<T> entities, CollectionChangedType collectionChangedType)
    {
        Entities = entities;
        CollectionChangedType = collectionChangedType;
    }
}

public enum CollectionChangedType
{
    Added,
    Removed
}