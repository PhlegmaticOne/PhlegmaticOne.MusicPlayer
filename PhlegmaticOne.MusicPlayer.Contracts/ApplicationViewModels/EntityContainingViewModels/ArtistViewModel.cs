using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;

public class ArtistViewModel : PlayerTrackableViewModel, IEntityContainingViewModel<ActiveArtistViewModel>
{
    private bool _isFirst;
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private readonly IUIThreadInvokerService _uiThreadInvokerService;
    public ActiveArtistViewModel Entity { get; set; }
    public ObservableCollection<TrackBaseViewModel> TopTracks { get; set; }
    public ArtistViewModel(IPlayerService playerService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService, 
        IUIThreadInvokerService uiThreadInvokerService,
        ILikeService likeService) : base(playerService, likeService)
    {
        _isFirst = true;
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _uiThreadInvokerService = uiThreadInvokerService;
        TopTracks = new();

        ActiveArtistNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationFromTrackCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveCollectionFromTrack, _ => true);
        ActiveCollectionNavigationFromPreviewCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveCollectionFromPreview, _ => true);
        SelectTopTracksCommand = DelegateCommandFactory.CreateCommand(SelectTopTracks, _ => true);

        TrySetSong();
    }

    public IDelegateCommand ActiveArtistNavigationCommand { get; }
    public IDelegateCommand ActiveCollectionNavigationFromTrackCommand { get; }
    public IDelegateCommand ActiveCollectionNavigationFromPreviewCommand { get; }
    public IDelegateCommand SelectTopTracksCommand { get; }

    private async void SelectTopTracks(object? parameter)
    {
        await Task.Run(async () =>
        {
            var tracks = Entity.Tracks.OrderBy(x => x.TimePlayed).Take(10);
            await _uiThreadInvokerService.InvokeAsync(() =>
            {
                foreach (var trackBaseViewModel in tracks)
                {
                    TopTracks.Add(trackBaseViewModel);
                }
            });
        });
    }

    protected override void PlaySongAction(object? parameter)
    {
        PlayerService.TracksQueue.Clear();
        PlayerService.TracksQueue.Enqueue(TopTracks);
        base.PlaySongAction(parameter);
    }

    private async void NavigateToActiveArtist(object? parameter)
    {
        if (parameter is ArtistLinkViewModel artistLinkViewModel && artistLinkViewModel.Id != Entity.Id)
        {
            await _entityContainingViewModelsNavigationService
                .From<ArtistLinkViewModel, ActiveArtistViewModel>()
                .NavigateAsync<ArtistViewModel>(artistLinkViewModel);
        }
    }

    private async void NavigateToActiveCollectionFromTrack(object? parameter)
    {
        if (parameter is TrackBaseViewModel trackBaseViewModel)
        {
            await _entityContainingViewModelsNavigationService
                .From<TrackBaseViewModel, ActiveAlbumViewModel>()
                .NavigateAsync<AlbumViewModel>(trackBaseViewModel);
        }
    }

    private async void NavigateToActiveCollectionFromPreview(object? parameter)
    {
        if (parameter is AlbumPreviewViewModel albumPreviewViewModel)
        {
            await _entityContainingViewModelsNavigationService
                .From<AlbumPreviewViewModel, ActiveAlbumViewModel>()
                .NavigateAsync<AlbumViewModel>(albumPreviewViewModel);
        }
    }
}