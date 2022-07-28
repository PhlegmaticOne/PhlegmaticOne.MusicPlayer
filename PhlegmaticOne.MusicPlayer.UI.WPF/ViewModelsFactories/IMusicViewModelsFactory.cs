using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public interface IMusicViewModelsFactory
{
    public BaseViewModel CreateAlbumViewModel(Album album);
    public BaseViewModel CreatePlaylistViewModel(Playlist playlist);
    public BaseViewModel CreateArtistViewModel(Artist artist);
}