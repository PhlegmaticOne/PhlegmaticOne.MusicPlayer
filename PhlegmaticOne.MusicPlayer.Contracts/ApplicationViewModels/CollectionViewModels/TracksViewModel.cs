using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.CollectionViewModels;

public class TracksViewModel : CollectionViewModelBase<TracksViewModel, TrackBaseViewModel>
{
    private bool _isFirst = true;
    public TracksViewModel(IPlayerService playerService, ILikeService likeService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        IUIThreadInvokerService uiThreadInvokerService,
        ReloadViewModelBase<TracksViewModel> reloadViewModel,
        SortViewModelBase<TracksViewModel, TrackBaseViewModel> sortViewModel) :
        base(playerService, likeService, uiThreadInvokerService, entityContainingViewModelsNavigationService, reloadViewModel, sortViewModel)
    {
        ActiveArtistNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveCollection, _ => true);
    }
    public IDelegateCommand ActiveArtistNavigationCommand { get; }
    public IDelegateCommand ActiveCollectionNavigationCommand { get; }
    protected override void PlaySongAction(object? parameter)
    {
        if (_isFirst)
        {
            PlayerService.Enqueue(Items, true);
            _isFirst = false;
        }
        base.PlaySongAction(parameter);
    }

    private async void NavigateToActiveArtist(object? parameter)
    {
        if (parameter is ArtistLinkViewModel artistLinkViewModel)
        {
            await EntityContainingViewModelsNavigationService
                .From<ArtistLinkViewModel, ActiveArtistViewModel>()
                .NavigateAsync<ArtistViewModel>(artistLinkViewModel);
        }
    }

    private async void NavigateToActiveCollection(object? parameter)
    {
        if (parameter is TrackBaseViewModel trackBaseViewModel)
        {
            await EntityContainingViewModelsNavigationService
                .From<TrackBaseViewModel, ActiveAlbumViewModel>()
                .NavigateAsync<AlbumViewModel>(trackBaseViewModel);
        }
    }
}