using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public interface IAlbumViewModelFactory
{
    public BaseViewModel CreateViewModel(Album album);
}