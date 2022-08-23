using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using PhlegmaticOne.WPF.Navigation;
using PhlegmaticOne.WPF.Navigation.EntityContainingViewModels;


namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public AlbumsCollectionViewModel(IPlayerService playerService, 
        ReloadViewModelBase<AlbumsCollectionViewModel> reloadViewModel,
        SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel> sortViewModelBase,
        IUIThreadInvokerService uiThreadInvokerService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService) :
        base(playerService, reloadViewModel, sortViewModelBase, uiThreadInvokerService, entityContainingViewModelsNavigationService)
    {
        ActiveAlbumNavigationCommand = new(NavigateToActiveAlbum, _ => true);
    }
    public DelegateCommand ActiveAlbumNavigationCommand { get; }

    private async void NavigateToActiveAlbum(object? parameter)
    {
        if (parameter is AlbumPreviewViewModel albumPreviewViewModel)
        {
            await EntityContainingViewModelsNavigationService
                .From<AlbumPreviewViewModel, ActiveAlbumViewModel>()
                .NavigateAsync<AlbumViewModel>(albumPreviewViewModel);
        }
    }
}