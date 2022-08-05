using System;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Entities;

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
    public event EventHandler<IEnumerable<Song>>? QueueChanged;
    public void Enqueue(Song song)
    {
        _songs.Add(song);
        Invoke();
    }

    public void Enqueue(IEnumerable<Song> songs)
    {
        _songs.AddRange(songs);
        Invoke();
    }

    public void Remove(Song song)
    {
        var songIndex = _songs.IndexOf(song);
        if (songIndex < _currentSongIndex)
        {
            _currentSongIndex--;
        }
        _songs.RemoveAt(songIndex);
        Invoke();
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

    private void Invoke() => QueueChanged?.Invoke(this, _songs);
}