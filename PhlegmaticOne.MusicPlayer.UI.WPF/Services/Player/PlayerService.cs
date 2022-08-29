using PhlegmaticOne.MusicPlayer.Players.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ValueProviders;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services.Player;

public class PlayerService : IPlayerService
{
    private readonly IPlayer _player;

    public PlayerService(IPlayer player,
        IObservableQueue<TrackBaseViewModel> songQueue,
        IValueProvider<TrackBaseViewModel> songValueProvider)
    {
        _player = player;
        TrackValueProvider = songValueProvider;
        TracksQueue = songQueue;
        Subscribe();
    }

    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;
    public float Volume { get => _player.Volume; set => _player.Volume = value; }
    public IValueProvider<TrackBaseViewModel> TrackValueProvider { get; }
    public IObservableQueue<TrackBaseViewModel> TracksQueue { get; }

    public event EventHandler<bool>? PauseChanged;
    public event EventHandler<bool>? StopChanged;
    public event EventHandler<TimeSpan>? TimeChanged;
    public event EventHandler<CollectionChangedEventArgs<TrackBaseViewModel>>? QueueChanged;

    public void SetAndPlay(TrackBaseViewModel? song)
    {
        TrackValueProvider.Set(song);
        if (song is not null)
        {
            TracksQueue.Current = song;
            _player.Stop();
            _player.Play(ChooseFilePath(song));
        }
    }

    public void Stop()
    {
        _player.Stop();
    }

    public void Rewind(TimeSpan rewindToTime)
    {
        _player.Rewind(rewindToTime);
    }

    public void MoveNext(QueueMoveType queueMoveType)
    {
        if (TracksQueue.Any())
        {
            TracksQueue.MoveNext(queueMoveType);
            SetAndPlay(TracksQueue.Current);
        }
    }

    public void MovePrevious()
    {
        if (TracksQueue.Any())
        {
            TracksQueue.MovePrevious();
            SetAndPlay(TracksQueue.Current);
        }
    }

    public void ChangeShuffleType(ShuffleType shuffleType)
    {
        TracksQueue.ShuffleType = shuffleType;
    }

    public void ChangeRepeatType(RepeatType repeatType)
    {
        TracksQueue.RepeatType = repeatType;
    }

    public void Pause()
    {
        _player.PauseOrUnpause();
    }

    public void Enqueue(IEnumerable<TrackBaseViewModel> songs, bool isClear)
    {
        if (isClear)
        {
            TracksQueue.Clear();
        }
        TracksQueue.Enqueue(songs);
    }

    public void RaiseEvents()
    {
        SongQueueOnQueueChanged(this, new CollectionChangedEventArgs<TrackBaseViewModel>(TracksQueue, CollectionChangedType.Added));
    }

    private void Subscribe()
    {
        _player.PauseChanged += OnPlayerOnPauseChanged;

        _player.StopChanged += OnPlayerOnStopChanged;

        _player.TimeChanged += OnPlayerOnTimeChanged;

        _player.SongEnded += OnPlayerOnSongEnded;

        TracksQueue.QueueChanged += SongQueueOnQueueChanged;
    }

    private void SongQueueOnQueueChanged(object? sender, CollectionChangedEventArgs<TrackBaseViewModel> e)
    {
        QueueChanged?.Invoke(this, e);
    }

    private void OnPlayerOnTimeChanged(object? _, TimeSpan newTime)
    {
        TimeChanged?.Invoke(this, newTime);
    }

    private void OnPlayerOnSongEnded(object? _, EventArgs eventArgs)
    {
        TracksQueue.MoveNext(QueueMoveType.AccordingToRepeatType);
        var currentSong = TracksQueue.Current;
        if (currentSong is not null)
        {
            SetAndPlay(currentSong);
        }
    }

    private void OnPlayerOnStopChanged(object? _, bool isStopped)
    {
        IsStopped = isStopped;
        StopChanged?.Invoke(this, isStopped);
    }

    private void OnPlayerOnPauseChanged(object? _, bool isPaused)
    {
        IsPaused = isPaused;
        PauseChanged?.Invoke(this, isPaused);
    }

    private static string ChooseFilePath(TrackBaseViewModel song) =>
        string.IsNullOrEmpty(song.LocalUrl) ? song.OnlineUrl : song.LocalUrl;

    public void Dispose()
    {
        _player.Dispose();
    }
}