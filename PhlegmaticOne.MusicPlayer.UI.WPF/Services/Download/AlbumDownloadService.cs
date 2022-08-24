using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;
using System.IO;
using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services.Download;

public class AlbumDownloadService : IDownloadService<ActiveAlbumViewModel>
{
    private readonly IDownloadSettings _downloadSettings;
    private readonly IDownloader _downloader;

    public AlbumDownloadService(IDownloadSettings downloadSettings, IDownloader downloader)
    {
        _downloadSettings = downloadSettings;
        _downloader = downloader;
    }
    public async Task Download(ActiveAlbumViewModel entity)
    {
        var path = _downloadSettings.DownloadDirectoryPath;
        foreach (var song in entity.Tracks)
        {
            var localSongPath = Path.Combine(path, entity.ToString());
            var fileName = song.Title + ".mp3";
            song.IsDownloading = true;
            song.LocalUrl = localSongPath;
            await _downloader.DownloadAsync(song.OnlineUrl, localSongPath, fileName);
            song.IsDownloading = false;
            song.IsDownloaded = true;
        }

        entity.IsDownloaded = true;
    }
}