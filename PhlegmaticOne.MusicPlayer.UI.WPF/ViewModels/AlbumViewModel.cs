using System.Collections.Generic;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : BaseViewModel
{
    public Album Album { get; set; }
    public Song PlayingSong { get; set; }
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(Album album, IAlbumFeaturesProvider albumFeaturesProvider)
    {
        Album = album;
        AlbumActions = albumFeaturesProvider.AlbumFeatures;
        PlaySongCommand = new(PlaySong, _ => true);
    }
    public DelegateCommand PlaySongCommand { get; set; }

    private void PlaySong(object? parameter)
    {
        var song = (Song)parameter;
        PlayingSong = song;
    }
}