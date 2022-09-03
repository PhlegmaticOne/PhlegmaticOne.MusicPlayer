using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.PagedList;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.ViewModels.EntityContainingViewModels;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public AlbumsCollectionViewModel(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService,
        IUiThreadInvokerService uiThreadInvokerService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        ReloadViewModelBase<AlbumsCollectionViewModel> reloadViewModel,
        SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel> sortViewModel,
        PagedListViewModelBase<AlbumPreviewViewModel> pagedListViewModel) :
        base(playerService, likeService, uiThreadInvokerService, entityContainingViewModelsNavigationService, reloadViewModel, sortViewModel, pagedListViewModel)
    {
        ActiveAlbumNavigationCommand = RelayCommandFactory
            .CreateRequiredParameterAsyncCommand<AlbumPreviewViewModel>(NavigateToActiveAlbum, _ => true);
    }
    public IRelayCommand ActiveAlbumNavigationCommand { get; }
    
    private async Task NavigateToActiveAlbum(AlbumPreviewViewModel parameter)
    {
        await EntityContainingViewModelsNavigationService
            .From<AlbumPreviewViewModel, ActiveAlbumViewModel>()
            .NavigateAsync<AlbumViewModel>(parameter);
    }
}