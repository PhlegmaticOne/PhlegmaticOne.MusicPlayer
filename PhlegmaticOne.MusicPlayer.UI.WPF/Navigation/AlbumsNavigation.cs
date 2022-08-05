using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Features.Album;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class AlbumsNavigation : MusicNavigationBase<Album>
{
    private readonly IAlbumFeaturesProvider _albumFeaturesProvider;
    private readonly IPlayer _player;
    private readonly ISongsQueue _songsQueue;
    private readonly IValueProvider<Song> _valueProvider;
    private readonly IValueProvider<Album> _albumValueProvider;

    public AlbumsNavigation(INavigator navigator, IPlayer player, ISongsQueue songsQueue,
        IValueProvider<Song> valueProvider, IValueProvider<Album> albumValueProvider) : base(navigator)
    {
       // _albumFeaturesProvider = albumFeaturesProvider;
        _player = player;
        _songsQueue = songsQueue;
        _valueProvider = valueProvider;
        _albumValueProvider = albumValueProvider;
    }
    protected override BaseViewModel CreateViewModel(Album entity)
    {
        return new AlbumViewModel(entity, _songsQueue, _player, _valueProvider, _albumValueProvider);
    }
}