using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class CollectionLinkToAlbumNavigation : IMusicViewModelsFactory<CollectionLinkViewModel, AlbumViewModel>
{
    private readonly IEntityCollectionGetService _viewModelGetService;
    private readonly IPlayerService _playerService;
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;

    public CollectionLinkToAlbumNavigation(IEntityCollectionGetService viewModelGetService, IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService)
    {
        _viewModelGetService = viewModelGetService;
        _playerService = playerService;
        _downloadService = downloadService;
    }
    public async Task<AlbumViewModel> CreateViewModelAsync(CollectionLinkViewModel entity)
    {
        ///var album = await _viewModelGetService.GetEntityCollectionAsync<ActiveAlbumViewModel>();
        return new AlbumViewModel(new ActiveAlbumViewModel(), _playerService, _downloadService);
    }
}