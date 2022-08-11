using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services;

public interface IPlayerService : IDisposable
{
    public bool IsPaused { get; set; }
    public bool IsStopped { get; set; }
    public float Volume { get; set; }
    public event EventHandler<bool> PauseChanged;
    public event EventHandler<bool> StopChanged;
    public event EventHandler<TimeSpan> TimeChanged;
    public event EventHandler<CollectionChangedEventArgs<TrackBaseViewModel>> QueueChanged;
    public IValueProvider<T>? ValueProvider<T>() where T : BaseViewModel;
    public void SetAndPlay(TrackBaseViewModel song);
    public void Stop();
    public void Rewind(TimeSpan rewindToTime);
    public void MoveNext(QueueMoveType queueMoveType);
    public void MovePrevious();
    public void ChangeShuffleType(ShuffleType shuffleType);
    public void ChangeRepeatType(RepeatType repeatType);
    public void Pause();
    public void Enqueue(IEnumerable<TrackBaseViewModel> songs, bool isClear);
    public void RaiseEvents();
}