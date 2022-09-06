using PhlegmaticOne.MusicPlayer.Contracts.Services.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Models;
using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.EntityContainingViewModels;

public class AlbumViewModel : PlayerTrackableViewModel, IEntityContainingViewModel<ActiveAlbumViewModel>
{
    private readonly IUiThreadInvokerService _uiThreadInvokerService;
    private readonly IFileOperatingService<TrackBaseViewModel> _trackOperatingService;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private bool _isFirstSongWillPlay;
    public AlbumViewModel(IPlayerService<TrackBaseViewModel> playerService, 
        ILikeService likeService,
        IUiThreadInvokerService uiThreadInvokerService,
        IFileOperatingService<TrackBaseViewModel> trackOperatingService,
        IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService) :
        base(playerService, likeService)
    {
        _uiThreadInvokerService = uiThreadInvokerService;
        _trackOperatingService = trackOperatingService;
        _trackActionsProvider = trackActionsProvider;
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _isFirstSongWillPlay = true;

        NavigateToArtistCommand = RelayCommandFactory
            .CreateRequiredParameterAsyncCommand<ArtistLinkViewModel>(NavigateToArtistAction, _ => true);
        DownloadAlbumCommand = RelayCommandFactory
            .CreateCommand(DownloadAlbum, _ => Entity.IsDownloaded == false || Entity.IsDownloading == false);
        TrySetSong();
    }

    public ActiveAlbumViewModel Entity { get; set; } = null!;
    public IRelayCommand NavigateToArtistCommand { get; }
    public IRelayCommand DownloadAlbumCommand { get; }

    protected override void PlaySongAction(TrackBaseViewModel? parameter)
    {
        if (_isFirstSongWillPlay)
        {
            PlayerService.Clear();
            PlayerService.AddRange(Entity.Tracks);
            _isFirstSongWillPlay = false;
        }
        base.PlaySongAction(parameter);
    }

    private async Task NavigateToArtistAction(ArtistLinkViewModel parameter)
    {
        await _entityContainingViewModelsNavigationService
            .From<ArtistLinkViewModel, ActiveArtistViewModel>()
            .NavigateAsync<ArtistViewModel>(parameter);
    }

    private async void DownloadAlbum(object? parameter)
    {
        Entity.IsDownloading = true;
        DownloadAlbumCommand.RaiseCanExecute();

        await _uiThreadInvokerService.InvokeAsync(async () =>
        {
            foreach (var trackBaseViewModel in Entity.Tracks)
            {
                if (trackBaseViewModel.IsDownloaded == false)
                {
                    trackBaseViewModel.IsDownloading = true;
                    await _trackOperatingService.Download(trackBaseViewModel);
                    trackBaseViewModel.IsDownloading = false;
                    trackBaseViewModel.IsDownloaded = true;
                    trackBaseViewModel.Actions = _trackActionsProvider.GetActions(trackBaseViewModel);
                }
            }
        });
        Entity.IsDownloaded = true;
    }
}