using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : BaseViewModel
{
    public Album Album { get; set; }

    public AlbumViewModel(Album album)
    {
        Album = album;
    }
}