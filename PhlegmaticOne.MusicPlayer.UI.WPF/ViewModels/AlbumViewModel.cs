using System.Collections.Generic;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : BaseViewModel
{
    private bool _isFirstSongWillPlay;
    private readonly ISongsQueue _songsQueue;
    private readonly IPlayer _player;
    private readonly IValueProvider<Song> _songValueProvider;
    private readonly IValueProvider<Album> _albumValueProvider;
    public Song CurrentSong { get; set; }
    public Album CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(Album album, ISongsQueue songsQueue, IPlayer player,
        IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider)
    {
        _isFirstSongWillPlay = true;
        _songsQueue = songsQueue;
        _player = player;
        _songValueProvider = songValueProvider;
        _albumValueProvider = albumValueProvider;

        CurrentAlbum = album;

        PlaySongCommand = new(PlaySong, _ => true);
        PlayPauseCommand = new(PlayPause, _ => true);

        player.PauseChanged += (_, isPaused) => IsPaused = isPaused;
        player.StopChanged += (_, isStopped) => IsStopped = isStopped;

        _songValueProvider.ValueChanged += (_, newSong) => CurrentSong = newSong;
    }


    public DelegateCommand PlaySongCommand { get; set; }
    public DelegateCommand PlayPauseCommand { get; set; }

    private void PlaySong(object? parameter)
    {
        if (parameter is Song song)
        {
            if (_isFirstSongWillPlay)
            {
                _albumValueProvider.Set(CurrentAlbum);
                _songsQueue.Clear();
                _songsQueue.Enqueue(CurrentAlbum.Songs);
                _isFirstSongWillPlay = false;
            }
            _songValueProvider.Set(song);
            _songsQueue.Current = song;
            _player.Stop();
            _player.Play(song.OnlineUrl);
        }
    }
    private void PlayPause(object? parameter)
    {
        _player.PauseOrUnpause();
    }
}