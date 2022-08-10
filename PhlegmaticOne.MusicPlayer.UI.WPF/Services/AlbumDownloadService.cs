﻿using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;
using PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;
using System.IO;
using System.Threading.Tasks;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services;

public class AlbumDownloadService : IDownloadService<AlbumEntityViewModel>
{
    private readonly IDownloadSettings _downloadSettings;
    private readonly IDownloader _downloader;

    public AlbumDownloadService(IDownloadSettings downloadSettings, IDownloader downloader)
    {
        _downloadSettings = downloadSettings;
        _downloader = downloader;
    }
    public async Task Download(AlbumEntityViewModel entity)
    {
        var path = _downloadSettings.DownloadDirectoryPath;
        foreach (var song in entity.Songs)
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