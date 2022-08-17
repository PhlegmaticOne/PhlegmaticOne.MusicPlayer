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
    private readonly IEntityCollectionGetService _viewModelGetService;

    public ActiveAlbumViewModelFactory(IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService,
        IEntityCollectionGetService viewModelGetService)
    {
        _playerService = playerService;
        _downloadService = downloadService;
        _viewModelGetService = viewModelGetService;
    }
    public async Task<AlbumViewModel> CreateViewModelAsync(AlbumPreviewViewModel entity)
    {
        //var album = await _viewModelGetService.GetEntityCollectionAsync<ActiveAlbumViewModel>(entity.Id);
        return new AlbumViewModel(new ActiveAlbumViewModel(), _playerService, _downloadService);
    }
}