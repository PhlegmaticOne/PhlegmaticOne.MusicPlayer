using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public class AlbumViewModelFactory : IAlbumViewModelFactory
{
    public AlbumViewModelFactory()//Injection
    {
        
    }
    public BaseViewModel CreateViewModel(Album album)
    {
        return new AlbumViewModel(album);
    }
}