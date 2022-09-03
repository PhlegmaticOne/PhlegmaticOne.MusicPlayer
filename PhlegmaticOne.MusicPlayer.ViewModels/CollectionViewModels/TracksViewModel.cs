using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.ViewModels.EntityContainingViewModels;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

public class TracksViewModel : CollectionViewModelBase<TracksViewModel, TrackBaseViewModel>
{
    private bool _isFirst = true;
    public TracksViewModel(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        IUIThreadInvokerService uiThreadInvokerService,
        ReloadViewModelBase<TracksViewModel> reloadViewModel,
        SortViewModelBase<TracksViewModel, TrackBaseViewModel> sortViewModel) :
        base(playerService, likeService, uiThreadInvokerService, entityContainingViewModelsNavigationService, reloadViewModel, sortViewModel)
    {
        ActiveArtistNavigationCommand = RelayCommandFactory.CreateCommand(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationCommand = RelayCommandFactory.CreateCommand(NavigateToActiveCollection, _ => true);
    }
    public IRelayCommand ActiveArtistNavigationCommand { get; }
    public IRelayCommand ActiveCollectionNavigationCommand { get; }
    protected override void PlaySongAction(TrackBaseViewModel? parameter)
    {
        if (_isFirst)
        {
            PlayerService.Clear();
            PlayerService.AddRange(Items);
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