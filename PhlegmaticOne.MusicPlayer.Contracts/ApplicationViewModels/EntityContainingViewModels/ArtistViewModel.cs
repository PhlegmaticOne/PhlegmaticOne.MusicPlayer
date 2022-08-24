using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;

public class ArtistViewModel : PlayerTrackableViewModel, IEntityContainingViewModel<ActiveArtistViewModel>
{
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    public ActiveArtistViewModel Entity { get; set; }
    public ObservableCollection<TrackBaseViewModel> TopTracks { get; set; }
    public ArtistViewModel(IPlayerService playerService, IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService) : base(playerService)
    {
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        TopTracks = new();

        ActiveArtistNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationFromTrackCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveCollectionFromTrack, _ => true);
        ActiveCollectionNavigationFromPreviewCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveCollectionFromPreview, _ => true);
    }

    public IDelegateCommand ActiveArtistNavigationCommand { get; }
    public IDelegateCommand ActiveCollectionNavigationFromTrackCommand { get; }
    public IDelegateCommand ActiveCollectionNavigationFromPreviewCommand { get; }

    private async void NavigateToActiveArtist(object? parameter)
    {
        if (parameter is ArtistLinkViewModel artistLinkViewModel)
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