using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;

public class AlbumViewModel : PlayerTrackableViewModel, IEntityContainingViewModel<ActiveAlbumViewModel>
{
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;
    private bool _isFirstSongWillPlay;
    public AlbumViewModel(IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService) : base(playerService)
    {
        _downloadService = downloadService;
        _isFirstSongWillPlay = true;

        DownloadAlbumCommand = DelegateCommandFactory.CreateCommand(DownloadAlbum, _ => true);

        TrySetSong();
    }

    public ActiveAlbumViewModel Entity { get; set; } = null!;
    public IDelegateCommand DownloadAlbumCommand { get; set; }

    protected override void PlaySongAction(object? parameter)
    {
        if (_isFirstSongWillPlay)
        {
            PlayerService.Enqueue(Entity.Tracks, true);
            _isFirstSongWillPlay = false;
        }
        base.PlaySongAction(parameter);
    }

    private async void DownloadAlbum(object? parameter)
    {
        if (Entity.IsDownloaded == false)
        {
            await _downloadService.Download(Entity);
        }
    }
}