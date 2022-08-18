using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation.ViewModelFactories;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class EFActiveAlbumViewModelFactory : IMusicViewModelsFactory<AlbumPreviewViewModel, AlbumViewModel>
{
    private readonly IPlayerService _playerService;
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperProvider;
    private readonly ICacheService _cacheService;

    public EFActiveAlbumViewModelFactory(IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService,
        ApplicationDbContext dbContext, IHandMapperService handMapperProvider, ICacheService cacheService)
    {
        _playerService = playerService;
        _downloadService = downloadService;
        _dbContext = dbContext;
        _handMapperProvider = handMapperProvider;
        _cacheService = cacheService;
    }


    public async Task<AlbumViewModel> CreateViewModelAsync(AlbumPreviewViewModel entity)
    {
        if (_cacheService.TryGetCachedValue(entity.Id, out AlbumViewModel albumViewModel))
        {
            return albumViewModel;
        }
        var set = _dbContext.Set<Album>();
        var songs = await set.Where(x => x.Id == entity.Id).SelectMany(x => x.Songs).ToListAsync();
        var mapped = _handMapperProvider
            .AddParameter("CollectionSongs", songs)
            .Map<ActiveAlbumViewModel>(entity);
        var viewModel = new AlbumViewModel(mapped, _playerService, _downloadService);

        _cacheService.Set(entity.Id, viewModel);
        foreach (var trackBaseViewModel in mapped.Tracks)
        {
            _cacheService.Set(trackBaseViewModel.Id, trackBaseViewModel);
        }

        return viewModel;
    }
}