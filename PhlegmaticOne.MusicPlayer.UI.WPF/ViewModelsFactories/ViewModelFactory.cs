using System.Collections.Generic;
using PhlegmaticOne.DependencyInjectionFactoryExtension;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public class ViewModelFactory : IViewModelFactory
{
    private readonly Dictionary<ViewType, IDependencyFactory<BaseViewModel>> _viewModels;
    public ViewModelFactory(IDependencyFactory<HomeViewModel> homeFactory,
        IDependencyFactory<AddingNewAlbumViewModel> addingNewAlbumFactory,
        IDependencyFactory<ArtistsViewModel> artistFactory,
        IDependencyFactory<AlbumsViewModel> collectionFactory,
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
    public BaseViewModel CreateViewModel(ViewType viewType) => _viewModels[viewType].Create();
}