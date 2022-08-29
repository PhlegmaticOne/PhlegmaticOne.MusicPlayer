using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;

public class AlbumViewModel : PlayerTrackableViewModel, IEntityContainingViewModel<ActiveAlbumViewModel>
{
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private bool _isFirstSongWillPlay;
    public AlbumViewModel(IPlayerService playerService, 
        ILikeService likeService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService) :
        base(playerService, likeService)
    {
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _isFirstSongWillPlay = true;

        NavigateToArtistCommand = DelegateCommandFactory.CreateCommand(NavigateToArtistAction, _ => true);

        TrySetSong();
    }

    public ActiveAlbumViewModel Entity { get; set; } = null!;
    public IDelegateCommand NavigateToArtistCommand { get; }

    protected override void PlaySongAction(object? parameter)
    {
        if (_isFirstSongWillPlay)
        {
            PlayerService.Enqueue(Entity.Tracks, true);
            _isFirstSongWillPlay = false;
        }
        base.PlaySongAction(parameter);
    }

    private async void NavigateToArtistAction(object? parameter)
    {
        if (parameter is ArtistLinkViewModel artistLinkViewModel)
        {
            await _entityContainingViewModelsNavigationService
                .From<ArtistLinkViewModel, ActiveArtistViewModel>()
                .NavigateAsync<ArtistViewModel>(artistLinkViewModel);
        }
    }
}