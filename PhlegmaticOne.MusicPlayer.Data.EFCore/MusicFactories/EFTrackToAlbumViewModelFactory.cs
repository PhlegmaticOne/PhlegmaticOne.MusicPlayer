using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation.ViewModelFactories;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class EFTrackToAlbumViewModelFactory : IMusicViewModelsFactory<TrackBaseViewModel, AlbumViewModel>
{
    private readonly IPlayerService _playerService;
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;
    private readonly ICacheService _cacheService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperService;

    public EFTrackToAlbumViewModelFactory(IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService,
        ICacheService cacheService, ApplicationDbContext dbContext, IHandMapperService handMapperService)
    {
        _playerService = playerService;
        _downloadService = downloadService;
        _cacheService = cacheService;
        _dbContext = dbContext;
        _handMapperService = handMapperService;
    }

    public async Task<AlbumViewModel> CreateViewModelAsync(TrackBaseViewModel entity)
    {
        if (_cacheService.TryGetCachedValue(entity.CollectionLink.Id, out AlbumViewModel albumViewModel))
        {
            return albumViewModel;
        }
        
        var set = _dbContext.Set<Album>();
        var other = await set.Where(x => x.Id == entity.CollectionLink.Id)
            .Select(x => new
            {
                x.Songs,
                x.IsFavorite,
                x.AlbumType,
                x.DateAdded,
                x.YearReleased
            })
            .FirstAsync();

        var result = _handMapperService
            .AddParameter("CollectionSongs", other.Songs)
            .AddParameter("IsFavorite", other.IsFavorite)
            .AddParameter("AlbumType", other.AlbumType)
            .AddParameter("DateAdded", other.DateAdded)
            .AddParameter("YearReleased", other.YearReleased)
            .Map<ActiveAlbumViewModel>(entity);

        var viewModel = new AlbumViewModel(result, _playerService, _downloadService);

        _cacheService.Set(entity.CollectionLink.Id, viewModel);

        return viewModel;
    }
}