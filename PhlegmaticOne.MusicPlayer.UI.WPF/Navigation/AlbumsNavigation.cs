using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class AlbumsNavigation : MusicNavigationBase<Album>
{
    private readonly IAlbumFeaturesProvider _albumFeaturesProvider;
    private readonly PlayerViewModel _playerViewModel;

    public AlbumsNavigation(INavigator navigator, IAlbumFeaturesProvider albumFeaturesProvider, PlayerViewModel playerViewModel) : base(navigator)
    {
        _albumFeaturesProvider = albumFeaturesProvider;
        _playerViewModel = playerViewModel;
    }
    protected override BaseViewModel CreateViewModel(Album entity)
    {
        return new AlbumViewModel(entity, _albumFeaturesProvider, _playerViewModel);
    }
}