using System.Collections.Generic;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : BaseViewModel
{
    private readonly IPlayer _player;
    public Song CurrentSong => _player.CurrentSong;
    public Album Album { get; set; }
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(Album album, IAlbumFeaturesProvider albumFeaturesProvider, IPlayer player)
    {
        _player = player;
        Album = album;
        AlbumActions = albumFeaturesProvider.AlbumFeatures;
        PlaySongCommand = new(PlaySong, _ => true);
    }
    public DelegateCommand PlaySongCommand { get; set; }

    private void PlaySong(object? parameter)
    {
        var song = (Song)parameter;
        _player.CurrentSong = song;
        _player.CurrentAlbum = Album;
        _player.Play();
    }
}