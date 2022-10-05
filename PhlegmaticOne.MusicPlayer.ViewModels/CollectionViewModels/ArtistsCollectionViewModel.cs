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

public class ArtistsCollectionViewModel : CollectionViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    public ArtistsCollectionViewModel(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        PagedListViewModelBase<ArtistPreviewViewModel> pagedListViewModel) :
        base(playerService, likeService, entityContainingViewModelsNavigationService, pagedListViewModel)
    {
        ActiveArtistNavigationCommand = RelayCommandFactory
            .CreateRequiredParameterAsyncCommand<ArtistPreviewViewModel>(NavigateToArtist);
    }
    public IRelayCommand ActiveArtistNavigationCommand { get; }

    private async Task NavigateToArtist(ArtistPreviewViewModel parameter)
    {
        await EntityContainingViewModelsNavigationService
            .From<ArtistPreviewViewModel, ActiveArtistViewModel>()
            .NavigateAsync<ArtistViewModel>(parameter);
    }
}