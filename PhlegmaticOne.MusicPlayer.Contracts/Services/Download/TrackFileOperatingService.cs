using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Download;

public class TrackFileOperatingService : IFileOperatingService<TrackBaseViewModel>
{
    private readonly ILocalSystemSettings _downloadSettings;
    private readonly IDownloader _downloader;
    private readonly ApplicationDbContext _dbContext;

    public TrackFileOperatingService(ILocalSystemSettings downloadSettings, IDownloader downloader, ApplicationDbContext dbContext)
    {
        _downloadSettings = downloadSettings;
        _downloader = downloader;
        _dbContext = dbContext;
    }
    public async Task Download(TrackBaseViewModel entity)
    {
        var path = _downloadSettings.DownloadDirectoryPath;
        var album = entity.CollectionLink.Title + "_" + entity.CollectionLink.Id;
        var localCollectionDirectoryPath = Path.Combine(path, album);
        var fileName = string.Join("/", entity.ArtistLinks) + " - " + entity.Title + ".mp3";

        await UpdateTrack(entity, Path.Combine(localCollectionDirectoryPath, fileName));

        await _downloader.DownloadAsync(entity.OnlineUrl, localCollectionDirectoryPath, fileName);
    }

    public async Task Delete(TrackBaseViewModel entity)
    {
        var lastSlashIndex = entity.LocalUrl.LastIndexOf('\\');
        var directoryPath = entity.LocalUrl.Substring(0, lastSlashIndex);
        var directory = new DirectoryInfo(directoryPath);
        File.Delete(entity.LocalUrl);
        if (directory.GetFiles().Any() == false)
        {
            directory.Delete();
        }

        await UpdateTrack(entity, string.Empty);
    }

    private async Task UpdateTrack(TrackBaseViewModel trackBaseViewModel, string newLocalPath)
    {
        trackBaseViewModel.LocalUrl = newLocalPath;
        var set = _dbContext.Set<Song>();
        var track = await set
            .Where(x => x.Id == trackBaseViewModel.Id)
            .FirstAsync();
        track.LocalUrl = newLocalPath;
        set.Update(track);
        await _dbContext.SaveChangesAsync();
    }
}