using System.Collections.Generic;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : BaseViewModel
{
    public PlayerViewModel PlayerViewModel { get; set; }
    public Song CurrentSong { get; set; }
    public Album Album { get; set; }
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(Album album, IAlbumFeaturesProvider albumFeaturesProvider, PlayerViewModel playerViewModel)
    {
        PlayerViewModel = playerViewModel;
        Album = album;
        AlbumActions = albumFeaturesProvider.AlbumFeatures;
        PlaySongCommand = new(PlaySong, _ => true);
        PlayerViewModel.CurrentAlbum = album;
    }
    public DelegateCommand PlaySongCommand { get; set; }

    private void PlaySong(object? parameter)
    {
        if (parameter is Song song)
        {
            CurrentSong = song;

            PlayerViewModel.CurrentSong = song;
            PlayerViewModel.Player.Play(song.OnlineUrl);
        }
    }
}