using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PhlegmaticOne.MusicPlayer.UI.WPF.Extensions;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

public class ObservableQueue<T> : IObservableQueue<T> where T : class
{
    private readonly List<T> _entities;
    private int _currentSongIndex;
    private bool _isQueueOver;
    public ObservableQueue()
    {
        _entities = new();
        RepeatType = RepeatType.RepeatOff;
        ShuffleType = ShuffleType.ShuffleOff;
    }

    public IReadOnlyCollection<T> Entities => _entities;
    public RepeatType RepeatType { get; set; }
    public ShuffleType ShuffleType { get; set; }
    public event EventHandler<CollectionChangedEventArgs<T>>? QueueChanged;
    public void Enqueue(T entity)
    {
        _entities.Add(entity);
        Invoke(entity.ToOneItemEnumerable(), CollectionChangedType.Added);
    }

    public void Enqueue(IEnumerable<T> entities)
    {
        var collection = entities.ToList();
        _entities.AddRange(collection);
        Invoke(collection, CollectionChangedType.Added);
    }

    public void Remove(T entity)
    {
        var songIndex = _entities.IndexOf(entity);
        if (songIndex < _currentSongIndex)
        {
            _currentSongIndex--;
        }
        _entities.RemoveAt(songIndex);
        Invoke(entity.ToOneItemEnumerable(), CollectionChangedType.Removed);
    }

    public T? Current
    {
        get => _isQueueOver ? null : _entities[_currentSongIndex];
        set
        {
            var index = _entities.IndexOf(value);
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
        _currentSongIndex = Random.Shared.Next(0, _entities.Count);
    }

    private void IncreaseQueueIndex()
    {
        if (_currentSongIndex < _entities.Count)
        {
            _isQueueOver = false;
        }

        if (_isQueueOver == false)
        {
            ++_currentSongIndex;
        }

        if (_currentSongIndex == _entities.Count)
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
        _entities.Clear();
    }

    private void Invoke(IEnumerable<T> songs, CollectionChangedType collectionChangedType) =>
        QueueChanged?.Invoke(this, new(songs, collectionChangedType));

    public IEnumerator<T> GetEnumerator() => _entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_entities).GetEnumerator();
}