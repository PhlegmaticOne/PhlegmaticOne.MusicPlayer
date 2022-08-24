using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.CollectionViewModels;

public class ArtistsCollectionViewModel : CollectionViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    public ArtistsCollectionViewModel(IPlayerService playerService,
        IUIThreadInvokerService uiThreadInvokerService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        ReloadViewModelBase<ArtistsCollectionViewModel> reloadViewModel,
        SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel> sortViewModel) :
        base(playerService, uiThreadInvokerService, entityContainingViewModelsNavigationService, reloadViewModel, sortViewModel)
    {
        ActiveArtistNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToArtist, _ => true);
    }
    public IDelegateCommand ActiveArtistNavigationCommand { get; }

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