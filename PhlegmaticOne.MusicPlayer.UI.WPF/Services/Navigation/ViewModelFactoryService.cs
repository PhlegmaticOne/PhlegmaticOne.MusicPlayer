using PhlegmaticOne.DependencyInjectionFactoryExtension;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services.Navigation;

public class ViewModelFactoryService : IViewModelFactoryService
{
    private readonly Dictionary<ViewType, IDependencyFactory<ApplicationBaseViewModel>> _viewModels;
    public ViewModelFactoryService(IDependencyFactory<HomeViewModel> homeFactory,
        IDependencyFactory<AddingNewAlbumViewModel> addingNewAlbumFactory,
        IDependencyFactory<ArtistsCollectionViewModel> artistFactory,
        IDependencyFactory<AlbumsCollectionViewModel> collectionFactory,
        IDependencyFactory<DownloadedTracksViewModel> downloadedTracksFactory,
        IDependencyFactory<PlaylistsViewModel> playlistFactory,
        IDependencyFactory<SettingsViewModel> settingsFactory,
        IDependencyFactory<TracksViewModel> tracksFactory)
    {
        _viewModels = new()
        {
            {ViewType.Home, homeFactory},
            {ViewType.AddingNewAlbum, addingNewAlbumFactory},
            {ViewType.Artists, artistFactory},
            {ViewType.Collection, collectionFactory},
            {ViewType.DownloadedTracks, downloadedTracksFactory},
            {ViewType.Playlists, playlistFactory},
            {ViewType.Settings, settingsFactory},
            {ViewType.Tracks, tracksFactory}
        };
    }
    public ApplicationBaseViewModel CreateViewModel(ViewType viewType) => _viewModels[viewType].Create();
}