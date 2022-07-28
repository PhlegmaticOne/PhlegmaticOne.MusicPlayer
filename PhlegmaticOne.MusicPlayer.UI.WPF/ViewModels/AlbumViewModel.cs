using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : BaseViewModel
{
    public Album Album { get; set; }
    public ObservableCollection<string> AlbumActions { get; set; } = new();
    public AlbumViewModel(Album album)
    {
        Album = album;
        AlbumActions.Add("Shuffle");
    }
}