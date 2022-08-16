using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.Services;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Application;

public class ActiveAlbumViewModelFactory : IMusicViewModelsFactory<AlbumPreviewViewModel, AlbumViewModel>
{
    private readonly IPlayerService _playerService;
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;
    private readonly IViewModelGetService _viewModelGetService;

    public ActiveAlbumViewModelFactory(IPlayerService playerService,
        IDownloadService<ActiveAlbumViewModel> downloadService,
        IViewModelGetService viewModelGetService)
    {
        _playerService = playerService;
        _downloadService = downloadService;
        _viewModelGetService = viewModelGetService;
    }
    public async Task<AlbumViewModel> CreateViewModelAsync(AlbumPreviewViewModel entity)
    {
        var album = await _viewModelGetService.GetViewModelAsync<ActiveAlbumViewModel>(entity.Id);
        return new AlbumViewModel(album, _playerService, _downloadService);
    }
}