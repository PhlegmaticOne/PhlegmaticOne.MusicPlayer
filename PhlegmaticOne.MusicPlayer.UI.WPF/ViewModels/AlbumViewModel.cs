using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumViewModel : PlayerTrackableViewModel
{
    private readonly IDownloadService<AlbumEntityViewModel> _downloadService;
    private bool _isFirstSongWillPlay;
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(AlbumEntityViewModel album, IPlayerService playerService, IDownloadService<AlbumEntityViewModel> downloadService) : base(playerService)
    {
        _downloadService = downloadService;
        _isFirstSongWillPlay = true;

        CurrentAlbum = album;

        DownloadAlbumCommand = new(DownloadAlbum, _ => true);

        TrySetSong();
    }
    public DelegateCommand DownloadAlbumCommand { get; set; }
    protected override void PlaySongAction(object? parameter)
    {
        if (_isFirstSongWillPlay)
        {
            PlayerService.ValueProvider<CollectionBaseViewModel>()!.Set(CurrentAlbum);
            PlayerService.Enqueue(CurrentAlbum.Songs, true);
            _isFirstSongWillPlay = false;
        }
        base.PlaySongAction(parameter);
    }

    private async void DownloadAlbum(object? parameter)
    {
        if (CurrentAlbum is AlbumEntityViewModel { IsDownloaded: false } albumEntityViewModel)
        {
            await _downloadService.Download(albumEntityViewModel);
        }
    }
}