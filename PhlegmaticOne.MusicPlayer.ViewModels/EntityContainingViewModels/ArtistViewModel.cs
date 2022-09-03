using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.EntityContainingViewModels;

public class ArtistViewModel : PlayerTrackableViewModel, IEntityContainingViewModel<ActiveArtistViewModel>
{
    private bool _isFirst;
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private readonly IUIThreadInvokerService _uiThreadInvokerService;
    public ActiveArtistViewModel Entity { get; set; }
    public ObservableCollection<TrackBaseViewModel> TopTracks { get; set; }
    public ArtistViewModel(IPlayerService<TrackBaseViewModel> playerService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService, 
        IUIThreadInvokerService uiThreadInvokerService,
        ILikeService likeService) : base(playerService, likeService)
    {
        _isFirst = true;
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _uiThreadInvokerService = uiThreadInvokerService;
        TopTracks = new();

        ActiveArtistNavigationCommand = RelayCommandFactory.CreateCommand(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationFromTrackCommand = RelayCommandFactory.CreateCommand(NavigateToActiveCollectionFromTrack, _ => true);
        ActiveCollectionNavigationFromPreviewCommand = RelayCommandFactory.CreateCommand(NavigateToActiveCollectionFromPreview, _ => true);
        SelectTopTracksCommand = RelayCommandFactory.CreateCommand(SelectTopTracks, _ => true);

        TrySetSong();
    }

    public IRelayCommand ActiveArtistNavigationCommand { get; }
    public IRelayCommand ActiveCollectionNavigationFromTrackCommand { get; }
    public IRelayCommand ActiveCollectionNavigationFromPreviewCommand { get; }
    public IRelayCommand SelectTopTracksCommand { get; }

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

    protected override void PlaySongAction(TrackBaseViewModel? parameter)
    {
        PlayerService.Clear();
        PlayerService.AddRange(TopTracks);
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