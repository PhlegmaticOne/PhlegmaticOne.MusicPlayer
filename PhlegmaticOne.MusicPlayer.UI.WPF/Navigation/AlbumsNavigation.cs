using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class AlbumsNavigation : MusicNavigationBase<AlbumEntityViewModel>
{
    private readonly IPlayer _player;
    private readonly IDownloader _downloader;
    private readonly IDownloadSettings _downloadSettings;
    private readonly IObservableQueue<SongEntityViewModel> _songsQueue;
    private readonly IValueProvider<SongEntityViewModel> _valueProvider;
    private readonly IValueProvider<AlbumEntityViewModel> _albumValueProvider;

    public AlbumsNavigation(INavigator navigator, IPlayer player, IDownloader downloader, IDownloadSettings downloadSettings, IObservableQueue<SongEntityViewModel> songsQueue,
        IValueProvider<SongEntityViewModel> valueProvider, IValueProvider<AlbumEntityViewModel> albumValueProvider) : base(navigator)
    {
        _player = player;
        _downloader = downloader;
        _downloadSettings = downloadSettings;
        _songsQueue = songsQueue;
        _valueProvider = valueProvider;
        _albumValueProvider = albumValueProvider;
    }
    protected override BaseViewModel CreateViewModel(AlbumEntityViewModel entity)
    {
        return new AlbumViewModel(entity, _songsQueue, _player, _downloader, _downloadSettings, _valueProvider, _albumValueProvider);
    }
}