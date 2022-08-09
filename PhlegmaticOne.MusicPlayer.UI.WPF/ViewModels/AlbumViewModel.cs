using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : PlayerTrackableViewModel
{
    private readonly IDownloader _downloader;
    private readonly IDownloadSettings _settings;
    private bool _isFirstSongWillPlay;
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(AlbumEntityViewModel album, 
        IObservableQueue<SongEntityViewModel> songsQueue, 
        IPlayer player, 
        IDownloader downloader,
        IDownloadSettings settings,
        IValueProvider<SongEntityViewModel> songValueProvider, 
        IValueProvider<AlbumEntityViewModel> albumValueProvider) : base(player, songsQueue, songValueProvider, albumValueProvider)
    {
        _downloader = downloader;
        _settings = settings;
        _isFirstSongWillPlay = true;
        AlbumValueProvider.Set(album);
        DownloadAlbumCommand = new(DownloadAlbum, _ => true);
        TrySetSong();
    }
    public DelegateCommand DownloadAlbumCommand { get; set; }
    protected override void PlaySongAction(object? parameter)
    {
        if (_isFirstSongWillPlay)
        {
            SongsQueue.Clear();
            SongsQueue.Enqueue(CurrentAlbum.Songs);
            _isFirstSongWillPlay = false;
        }
        base.PlaySongAction(parameter);
    }

    private async void DownloadAlbum(object? parameter)
    {
        if(CurrentAlbum.IsDownloaded) return;
        
        var path = _settings.DownloadDirectoryPath;
        foreach (var song in CurrentAlbum.Songs)
        {
            var localSongPath = Path.Combine(path, CurrentAlbum.ToString());
            var fileName = song.Title + ".mp3";
            song.IsDownloading = true;
            song.LocalUrl = localSongPath;
            await _downloader.DownloadAsync(song.OnlineUrl, localSongPath, fileName);
            song.IsDownloading = false;
            song.IsDownloaded = true;
        }

        CurrentAlbum.IsDownloaded = true;
    }
}