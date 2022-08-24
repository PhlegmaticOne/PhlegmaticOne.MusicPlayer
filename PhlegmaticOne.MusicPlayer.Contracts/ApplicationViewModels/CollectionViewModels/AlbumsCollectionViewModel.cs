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

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public AlbumsCollectionViewModel(IPlayerService playerService,
        IUIThreadInvokerService uiThreadInvokerService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        ReloadViewModelBase<AlbumsCollectionViewModel> reloadViewModel,
        SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel> sortViewModel) :
        base(playerService, uiThreadInvokerService, entityContainingViewModelsNavigationService, reloadViewModel, sortViewModel)
    {
        ActiveAlbumNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveAlbum, _ => true);
    }
    public IDelegateCommand ActiveAlbumNavigationCommand { get; }

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