using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.Services;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Application;

public class CollectionLinkToAlbumNavigation : IMusicViewModelsFactory<CollectionLinkViewModel, AlbumViewModel>
{
    private readonly IViewModelGetService _viewModelGetService;
    private readonly IPlayerService _playerService;
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;

    public CollectionLinkToAlbumNavigation(IViewModelGetService viewModelGetService, IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService)
    {
        _viewModelGetService = viewModelGetService;
        _playerService = playerService;
        _downloadService = downloadService;
    }
    public async Task<AlbumViewModel> CreateViewModelAsync(CollectionLinkViewModel entity)
    {
        var album = await _viewModelGetService.GetViewModelAsync<ActiveAlbumViewModel>(entity.Id);
        return new AlbumViewModel(album, _playerService, _downloadService);
    }
}