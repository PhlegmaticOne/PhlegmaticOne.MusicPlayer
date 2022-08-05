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

public class PlayerViewModel : PlayerTrackableViewModel, IDisposable
{
    private readonly ISongQueueViewModelFactory _songQueueViewModelFactory;
    private readonly INavigator _navigator;
    public TimeSpan CurrentTime { get; set; }

    public PlayerViewModel(IPlayer player, ISongsQueue songsQueue, IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider,
        ISongQueueViewModelFactory songQueueViewModelFactory, INavigator navigator) : base(player, songsQueue, songValueProvider, albumValueProvider)
    {
        _songQueueViewModelFactory = songQueueViewModelFactory;
        _navigator = navigator;

        RewindCommand = new(Rewind, _ => true);
        OpenSongsQueueCommand = new(OpenQueue, _ => true);

        player.TimeChanged += (_, newTime) => CurrentTime = newTime;
        player.SongEnded += PlayerOnSongEnded;

        albumValueProvider.ValueChanged += (_, newAlbum) => CurrentAlbum = newAlbum;

        SetIsPausedAndIsStopped();
    }

    private void PlayerOnSongEnded(object? sender, EventArgs e)
    {
        SongsQueue.MoveNext();
        var currentSong = SongsQueue.Current;
        if (currentSong is not null)
        {
            PlaySongAction(currentSong);
        }
    }

    public DelegateCommand RewindCommand { get; set; }
    public DelegateCommand OpenSongsQueueCommand { get; set; }

    private void OpenQueue(object? parameter)
    {
        var queueViewModel = _songQueueViewModelFactory.CreateQueueViewModel();
        _navigator.NavigateTo(queueViewModel);
    }

    private void Rewind(object? parameter)
    {
        if (parameter is double ticks)
        {
            Player.Rewind(ParseTime(ticks));
        }
    }

    private TimeSpan ParseTime(double value) => TimeSpan.FromTicks(Convert.ToInt64(value));

    public void Dispose()
    {
        Player.Dispose();
    }
}