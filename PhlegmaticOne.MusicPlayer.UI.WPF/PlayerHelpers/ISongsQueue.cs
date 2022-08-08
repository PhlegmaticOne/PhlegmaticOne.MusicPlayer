using System;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

public interface ISongsQueue : IEnumerable<Song>
{
    public IReadOnlyCollection<Song> Songs { get; }
    public RepeatType RepeatType { get; set; }
    public ShuffleType ShuffleType { get; set; }
    public event EventHandler<CollectionChangedEventArgs<Song>> QueueChanged; 
    public void Enqueue(Song song);
    public void Enqueue(IEnumerable<Song> songs);
    public void Remove(Song song);
    public Song? Current { get; set; }
    public void MoveNext(QueueMoveType queueMoveType);
    public void MovePrevious();
    public void Clear();
}

public enum RepeatType
{
    RepeatOff,
    RepeatQueue,
    RepeatSong
}

public enum ShuffleType
{
    ShuffleOn,
    ShuffleOff
}

public enum QueueMoveType
{
    AccordingToRepeatType,
    MoveAnyway
}