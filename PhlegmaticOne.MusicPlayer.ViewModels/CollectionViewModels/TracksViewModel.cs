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

public class TracksViewModel : CollectionViewModelBase<TracksViewModel, TrackBaseViewModel>
{
    private bool _isFirst = true;
    public TracksViewModel(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        PagedListViewModelBase<TrackBaseViewModel> pagedListViewModel) :
        base(playerService, likeService, entityContainingViewModelsNavigationService, pagedListViewModel)
    {
        ActiveArtistNavigationCommand = RelayCommandFactory
            .CreateRequiredParameterAsyncCommand<ArtistLinkViewModel>(NavigateToActiveArtist, _ => true);

        ActiveCollectionNavigationCommand = RelayCommandFactory
            .CreateRequiredParameterAsyncCommand<TrackBaseViewModel>(NavigateToActiveCollection, _ => true);
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

    private async Task NavigateToActiveArtist(ArtistLinkViewModel parameter)
    {
        await EntityContainingViewModelsNavigationService
            .From<ArtistLinkViewModel, ActiveArtistViewModel>()
            .NavigateAsync<ArtistViewModel>(parameter);
    }

    private async Task NavigateToActiveCollection(TrackBaseViewModel parameter)
    {
        await EntityContainingViewModelsNavigationService
            .From<TrackBaseViewModel, ActiveAlbumViewModel>()
            .NavigateAsync<AlbumViewModel>(parameter);
    }
}