using System;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class PlayerViewModel : BaseViewModel, IDisposable
{
    private readonly IPlayer _player;
    private readonly ISongsQueue _songsQueue;
    private readonly ISongQueueViewModelFactory _songQueueViewModelFactory;
    private readonly INavigator _navigator;
    private readonly IValueProvider<Song> _songValueProvider;
    private readonly IValueProvider<Album> _albumValueProvider;
    public TimeSpan CurrentTime { get; set; }
    public Song CurrentSong { get; set; }
    public Album CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;
    public PlayerViewModel(IPlayer player, ISongsQueue songsQueue, ISongQueueViewModelFactory songQueueViewModelFactory, INavigator navigator,
        IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider)
    {
        _player = player;
        _songsQueue = songsQueue;
        _songQueueViewModelFactory = songQueueViewModelFactory;
        _navigator = navigator;
        _songValueProvider = songValueProvider;
        _albumValueProvider = albumValueProvider;

        RewindCommand = new(Rewind, _ => true);
        PlayPauseCommand = new(PlayPause, _ => true);
        PlaySongCommand = new(PlaySong, _ => true);
        OpenSongsQueueCommand = new(OpenQueue, _ => true);

        player.TimeChanged += (_, newTime) => CurrentTime = newTime;
        player.PauseChanged += (_, isPaused) => IsPaused = isPaused;
        player.StopChanged += (_, isStopped) => IsStopped = isStopped;
        player.SongEnded += PlayerOnSongEnded;

        _songValueProvider.ValueChanged += (_, newSong) => CurrentSong = newSong;
        _albumValueProvider.ValueChanged += (_, newAlbum) => CurrentAlbum = newAlbum;
    }

    private void PlayerOnSongEnded(object? sender, EventArgs e)
    {
        _songsQueue.MoveNext();
        SetAndPlay(_songsQueue.Current);
    }

    public DelegateCommand RewindCommand { get; set; }
    public DelegateCommand PlayPauseCommand { get; set; }
    public DelegateCommand PlaySongCommand { get; set; }
    public DelegateCommand OpenSongsQueueCommand { get; set; }

    private void OpenQueue(object? parameter)
    {
        var queueViewModel = _songQueueViewModelFactory.CreateQueueViewModel();
        _navigator.NavigateTo(queueViewModel);
    }

    private void PlaySong(object? parameter)
    {
        if (parameter is Song song)
        {
            SetAndPlay(song);
        }
    }
    private void Rewind(object? parameter)
    {
        if (parameter is double ticks)
        {
            _player.Rewind(ParseTime(ticks));
        }
    }

    private void PlayPause(object? parameter)
    {
        _player.PauseOrUnpause();
    }

    private void SetAndPlay(Song? song)
    {
        _songValueProvider.Set(song);
        if (song is not null)
        {
            _player.Play(CurrentSong.OnlineUrl);
        }
    }

    private TimeSpan ParseTime(double value) => TimeSpan.FromTicks(Convert.ToInt64(value));

    public void Dispose()
    {
        _player.Dispose();
    }
}