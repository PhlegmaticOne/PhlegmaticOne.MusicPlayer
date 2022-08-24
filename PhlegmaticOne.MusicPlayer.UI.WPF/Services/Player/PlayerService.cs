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
    private readonly IObservableQueue<TrackBaseViewModel> _songQueue;
    private readonly IValueProvider<TrackBaseViewModel> _songValueProvider;

    public PlayerService(IPlayer player,
        IObservableQueue<TrackBaseViewModel> songQueue,
        IValueProvider<TrackBaseViewModel> songValueProvider)
    {
        _player = player;
        _songQueue = songQueue;
        _songValueProvider = songValueProvider;

        Subscribe();
    }

    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;
    public float Volume { get => _player.Volume; set => _player.Volume = value; }
    public IValueProvider<TrackBaseViewModel> TrackValueProvider => _songValueProvider;

    public event EventHandler<bool>? PauseChanged;
    public event EventHandler<bool>? StopChanged;
    public event EventHandler<TimeSpan>? TimeChanged;
    public event EventHandler<CollectionChangedEventArgs<TrackBaseViewModel>>? QueueChanged;

    public void SetAndPlay(TrackBaseViewModel? song)
    {
        _songValueProvider.Set(song);
        if (song is not null)
        {
            _songQueue.Current = song;
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
        if (_songQueue.Any())
        {
            _songQueue.MoveNext(queueMoveType);
            SetAndPlay(_songQueue.Current);
        }
    }

    public void MovePrevious()
    {
        if (_songQueue.Any())
        {
            _songQueue.MovePrevious();
            SetAndPlay(_songQueue.Current);
        }
    }

    public void ChangeShuffleType(ShuffleType shuffleType)
    {
        _songQueue.ShuffleType = shuffleType;
    }

    public void ChangeRepeatType(RepeatType repeatType)
    {
        _songQueue.RepeatType = repeatType;
    }

    public void Pause()
    {
        _player.PauseOrUnpause();
    }

    public void Enqueue(IEnumerable<TrackBaseViewModel> songs, bool isClear)
    {
        if (isClear)
        {
            _songQueue.Clear();
        }
        _songQueue.Enqueue(songs);
    }

    public void RaiseEvents()
    {
        SongQueueOnQueueChanged(this, new CollectionChangedEventArgs<TrackBaseViewModel>(_songQueue.Entities, CollectionChangedType.Added));
    }

    private void Subscribe()
    {
        _player.PauseChanged += OnPlayerOnPauseChanged;

        _player.StopChanged += OnPlayerOnStopChanged;

        _player.TimeChanged += OnPlayerOnTimeChanged;

        _player.SongEnded += OnPlayerOnSongEnded;

        _songQueue.QueueChanged += SongQueueOnQueueChanged;
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
        _songQueue.MoveNext(QueueMoveType.AccordingToRepeatType);
        var currentSong = _songQueue.Current;
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