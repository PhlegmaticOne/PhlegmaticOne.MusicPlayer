using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.Players;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class AlbumsNavigation : MusicNavigationBase<Album>
{
    private readonly IAlbumFeaturesProvider _albumFeaturesProvider;
    private readonly PlayersFactory _playersFactory;
    private readonly IPlayer _player;

    public AlbumsNavigation(INavigator navigator, IAlbumFeaturesProvider albumFeaturesProvider, PlayersFactory playersFactory) : base(navigator)
    {
        _albumFeaturesProvider = albumFeaturesProvider;
        _playersFactory = playersFactory;
    }
    protected override BaseViewModel CreateViewModel(Album entity)
    {
        return new AlbumViewModel(entity, _albumFeaturesProvider, _playersFactory.CreatePlayer());
    }
}