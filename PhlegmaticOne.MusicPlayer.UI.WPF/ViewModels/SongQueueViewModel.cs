using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class SongQueueViewModel : BaseViewModel
{
    private readonly IValueProvider<Song> _songValueProvider;
    private readonly IValueProvider<Album> _albumValueProvider;
    private readonly ISongsQueue _songsQueue;
    private readonly IPlayer _player;
    public Song CurrentSong { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;
    public ObservableCollection<Song> Songs { get; }
    public SongQueueViewModel(ISongsQueue songsQueue, IPlayer player, IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider)
    {
        _songValueProvider = songValueProvider;
        _albumValueProvider = albumValueProvider;
        _songsQueue = songsQueue;
        _player = player;

        Songs = new();

        player.PauseChanged += PlayerOnPauseChanged;
        player.StopChanged += PlayerOnStopChanged;

        _songsQueue.QueueChanged += SongsQueueOnQueueChanged;
        _songValueProvider.ValueChanged += SongValueProviderOnValueChanged;

        if (Songs.Any() == false)
        {
            SongsQueueOnQueueChanged(null, songsQueue.Songs);
        }
    }

    private void PlayerOnStopChanged(object? sender, bool e)
    {
        IsStopped = e;
    }

    private void PlayerOnPauseChanged(object? sender, bool e)
    {
        IsPaused = e;
    }

    private void SongValueProviderOnValueChanged(object? sender, Song? e)
    {
        CurrentSong = e;
    }

    public DelegateCommand PlaySongCommand { get; set; }
    private void SongsQueueOnQueueChanged(object? sender, IEnumerable<Song> newSongs)
    {
        Songs.Clear();
        foreach (var newSong in newSongs)
        {
            Songs.Add(newSong);
        }
    }
}