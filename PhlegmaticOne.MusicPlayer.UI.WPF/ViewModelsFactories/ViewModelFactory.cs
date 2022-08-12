using PhlegmaticOne.DependencyInjectionFactoryExtension;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public class ViewModelFactory : IViewModelFactory
{
    private readonly Dictionary<ViewType, IDependencyFactory<ApplicationBaseViewModel>> _viewModels;
    public ViewModelFactory(IDependencyFactory<HomeViewModel> homeFactory,
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