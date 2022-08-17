using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

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