using System;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

public interface ISongsQueue : IEnumerable<SongEntityViewModel>
{
    public IReadOnlyCollection<SongEntityViewModel> Songs { get; }
    public RepeatType RepeatType { get; set; }
    public ShuffleType ShuffleType { get; set; }
    public event EventHandler<CollectionChangedEventArgs<SongEntityViewModel>> QueueChanged; 
    public void Enqueue(SongEntityViewModel song);
    public void Enqueue(IEnumerable<SongEntityViewModel> songs);
    public void Remove(SongEntityViewModel song);
    public SongEntityViewModel? Current { get; set; }
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