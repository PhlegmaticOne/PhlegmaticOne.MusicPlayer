using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class AlbumsNavigation : MusicNavigationBase<AlbumEntityViewModel>
{
    private readonly IAlbumFeaturesProvider _albumFeaturesProvider;
    private readonly IPlayer _player;
    private readonly ISongsQueue _songsQueue;
    private readonly IValueProvider<SongEntityViewModel> _valueProvider;
    private readonly IValueProvider<AlbumEntityViewModel> _albumValueProvider;

    public AlbumsNavigation(INavigator navigator, IPlayer player, ISongsQueue songsQueue,
        IValueProvider<SongEntityViewModel> valueProvider, IValueProvider<AlbumEntityViewModel> albumValueProvider) : base(navigator)
    {
       // _albumFeaturesProvider = albumFeaturesProvider;
        _player = player;
        _songsQueue = songsQueue;
        _valueProvider = valueProvider;
        _albumValueProvider = albumValueProvider;
    }
    protected override BaseViewModel CreateViewModel(AlbumEntityViewModel entity)
    {
        return new AlbumViewModel(entity, _songsQueue, _player, _valueProvider, _albumValueProvider);
    }
}