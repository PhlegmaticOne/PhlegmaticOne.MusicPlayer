using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Application;

public class ActiveAlbumViewModelFactory : IMusicViewModelsFactory<AlbumPreviewViewModel, AlbumViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPlayerService _playerService;
    private readonly IDownloadService<ActiveAlbumViewModel> _downloadService;

    public ActiveAlbumViewModelFactory(ApplicationDbContext dbContext, IMapper mapper,
        IPlayerService playerService, IDownloadService<ActiveAlbumViewModel> downloadService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _playerService = playerService;
        _downloadService = downloadService;
    }
    public async Task<AlbumViewModel> CreateViewModelAsync(AlbumPreviewViewModel entity)
    {
        var set = _dbContext.Set<Album>();
        var album = await set.AsNoTracking()
            .Include(x => x.Artists)
            .Include(x => x.Songs)
            .FirstOrDefaultAsync(x => x.Id == entity.Id);

        var result = await _mapper.From(album).AdaptToTypeAsync<ActiveAlbumViewModel>();
        result.Cover = entity.Cover;
        foreach (var track in result.Tracks)
        {
            track.CollectionLink = _mapper.Map<CollectionLinkViewModel>(album);
            track.ArtistLinks = _mapper.Map<ICollection<ArtistLinkViewModel>>(album.Artists);
        }

        return new AlbumViewModel(result, _playerService, _downloadService);
    }
}