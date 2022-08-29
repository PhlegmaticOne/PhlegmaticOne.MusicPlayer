using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Download;

public class TrackDownloadService : IFileOperatingService<TrackBaseViewModel>
{
    private readonly ILocalSystemSettings _downloadSettings;
    private readonly IDownloader _downloader;

    public TrackDownloadService(ILocalSystemSettings downloadSettings, IDownloader downloader)
    {
        _downloadSettings = downloadSettings;
        _downloader = downloader;
    }
    public async Task Download(TrackBaseViewModel entity)
    {
        var path = _downloadSettings.DownloadDirectoryPath;
        var album = entity.CollectionLink.Title + "_" + entity.CollectionLink.Id;
        var localCollectionDirectoryPath = Path.Combine(path, album);

        var fileName = string.Join("/", entity.ArtistLinks) + " - " + entity.Title + ".mp3";
        entity.LocalUrl = Path.Combine(localCollectionDirectoryPath, fileName);

        await _downloader.DownloadAsync(entity.OnlineUrl, localCollectionDirectoryPath, fileName);
    }

    public Task Delete(TrackBaseViewModel entity)
    {
        var lastSlashIndex = entity.LocalUrl.LastIndexOf('\\');
        var directoryPath = entity.LocalUrl.Substring(0, lastSlashIndex);
        var directory = new DirectoryInfo(directoryPath);
        File.Delete(entity.LocalUrl);
        if (directory.GetFiles().Any() == false)
        {
            directory.Delete();
        }
        return Task.CompletedTask;
    }
}