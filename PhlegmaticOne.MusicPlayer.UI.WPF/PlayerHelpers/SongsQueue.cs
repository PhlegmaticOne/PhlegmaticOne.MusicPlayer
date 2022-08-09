using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Extensions;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
public class SongsQueue : ISongsQueue
{
    private readonly List<SongEntityViewModel> _songs;
    private int _currentSongIndex;
    private bool _isQueueOver;
    public SongsQueue()
    {
        _songs = new();
        RepeatType = RepeatType.RepeatOff;
        ShuffleType = ShuffleType.ShuffleOff;
    }

    public IReadOnlyCollection<SongEntityViewModel> Songs => _songs;
    public RepeatType RepeatType { get; set; }
    public ShuffleType ShuffleType { get; set; }
    public event EventHandler<CollectionChangedEventArgs<SongEntityViewModel>>? QueueChanged;
    public void Enqueue(SongEntityViewModel song)
    {
        _songs.Add(song);
        Invoke(song.ToOneItemEnumerable(), CollectionChangedType.Added);
    }

    public void Enqueue(IEnumerable<SongEntityViewModel> songs)
    {
        var collection = songs.ToList();
        _songs.AddRange(collection);
        Invoke(collection, CollectionChangedType.Added);
    }

    public void Remove(SongEntityViewModel song)
    {
        var songIndex = _songs.IndexOf(song);
        if (songIndex < _currentSongIndex)
        {
            _currentSongIndex--;
        }
        _songs.RemoveAt(songIndex);
        Invoke(song.ToOneItemEnumerable(), CollectionChangedType.Removed);
    }

    public SongEntityViewModel? Current
    {
        get => _isQueueOver ? null : _songs[_currentSongIndex];
        set
        {
            var index = _songs.IndexOf(value);
            if (index != -1)
            {
                _currentSongIndex = index;
            }
        }
    }

    public void MoveNext(QueueMoveType queueMoveType)
    {
        if (RepeatType == RepeatType.RepeatSong && queueMoveType == QueueMoveType.AccordingToRepeatType)
        {
            return;
        }
        if (ShuffleType == ShuffleType.ShuffleOn)
        {
            SetRandomIndex();
            return;
        }   
        switch (RepeatType)
        {
            case RepeatType.RepeatOff:
            {
                IncreaseQueueIndex();
                break;
            }
            case RepeatType.RepeatQueue:
            {
                IncreaseIndexAnyway();
                break;
            }
            case RepeatType.RepeatSong:
            {
                IncreaseIndexAnyway();
                break;
            }
        }
    }

    private void IncreaseIndexAnyway()
    {
        IncreaseQueueIndex();
        if (_isQueueOver)
        {
            _currentSongIndex = 0;
            _isQueueOver = false;
        }
    }

    private void SetRandomIndex()
    {
        _currentSongIndex = Random.Shared.Next(0, _songs.Count);
    }

    private void IncreaseQueueIndex()
    {
        if (_currentSongIndex < _songs.Count)
        {
            _isQueueOver = false;
        }

        if (_isQueueOver == false)
        {
            ++_currentSongIndex;
        }

        if (_currentSongIndex == _songs.Count)
        {
            _isQueueOver = true;
        }
    }

    public void MovePrevious()
    {
        if (ShuffleType == ShuffleType.ShuffleOn)
        {
            SetRandomIndex();
            return;
        }
        _currentSongIndex = _currentSongIndex == 0 ? 0 : --_currentSongIndex;
    }

    public void Clear()
    {
        _songs.Clear();
    }

    private void Invoke(IEnumerable<SongEntityViewModel> songs, CollectionChangedType collectionChangedType) =>
        QueueChanged?.Invoke(this, new(songs, collectionChangedType));

    public IEnumerator<SongEntityViewModel> GetEnumerator() => _songs.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _songs).GetEnumerator();
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