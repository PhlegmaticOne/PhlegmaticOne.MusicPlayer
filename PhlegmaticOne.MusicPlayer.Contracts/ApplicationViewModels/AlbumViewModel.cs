using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class AlbumViewModel : PlayerTrackableViewModel
{
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;
    private bool _isFirstSongWillPlay;
    public IDictionary<string, ICommand> AlbumActions { get; set; }
    public AlbumViewModel(ActiveAlbumViewModel album, IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService) : base(playerService)
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
            if (CurrentAlbum is ActiveAlbumViewModel activeAlbumViewModel)
            {
                PlayerService.Enqueue(activeAlbumViewModel.Tracks, true);
            }
            _isFirstSongWillPlay = false;
        }
        base.PlaySongAction(parameter);
    }

    private async void DownloadAlbum(object? parameter)
    {
        if (CurrentAlbum is ActiveAlbumViewModel { IsDownloaded: false } albumEntityViewModel)
        {
            await _downloadService.Download(albumEntityViewModel);
        }
    }
}