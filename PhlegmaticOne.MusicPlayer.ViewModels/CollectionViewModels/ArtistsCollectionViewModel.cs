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

public class ArtistsCollectionViewModel : CollectionViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    public ArtistsCollectionViewModel(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService,
        IUIThreadInvokerService uiThreadInvokerService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        ReloadViewModelBase<ArtistsCollectionViewModel> reloadViewModel,
        SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel> sortViewModel) :
        base(playerService, likeService, uiThreadInvokerService, entityContainingViewModelsNavigationService, reloadViewModel, sortViewModel)
    {
        ActiveArtistNavigationCommand = RelayCommandFactory.CreateCommand(NavigateToArtist, _ => true);
    }
    public IRelayCommand ActiveArtistNavigationCommand { get; }

    private async void NavigateToArtist(object? parameter)
    {
        if (parameter is ArtistPreviewViewModel artistPreviewViewModel)
        {
            await EntityContainingViewModelsNavigationService
                .From<ArtistPreviewViewModel, ActiveArtistViewModel>()
                .NavigateAsync<ArtistViewModel>(artistPreviewViewModel);
        }
    }
}