using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Models;
using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.EntityContainingViewModels;
using PhlegmaticOne.MusicPlayer.ViewModels.PagedList;
using PhlegmaticOne.MusicPlayerService.Base;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public AlbumsCollectionViewModel(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        PagedListViewModelBase<AlbumPreviewViewModel> pagedListViewModel) :
        base(playerService, likeService, entityContainingViewModelsNavigationService, pagedListViewModel)
    {
        ActiveAlbumNavigationCommand = RelayCommandFactory
            .CreateRequiredParameterAsyncCommand<AlbumPreviewViewModel>(NavigateToActiveAlbum);
    }
    public IRelayCommand ActiveAlbumNavigationCommand { get; }
    
    private async Task NavigateToActiveAlbum(AlbumPreviewViewModel parameter)
    {
        await EntityContainingViewModelsNavigationService
            .From<AlbumPreviewViewModel, ActiveAlbumViewModel>()
            .NavigateAsync<AlbumViewModel>(parameter);
    }
}