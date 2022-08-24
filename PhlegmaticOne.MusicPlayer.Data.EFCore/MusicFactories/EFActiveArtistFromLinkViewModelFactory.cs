using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class EFActiveArtistFromLinkViewModelFactory : NavigationFactoryBase<ArtistLinkViewModel, ActiveArtistViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperService;

    public EFActiveArtistFromLinkViewModelFactory(ApplicationDbContext dbContext, IHandMapperService handMapperService)
    {
        _dbContext = dbContext;
        _handMapperService = handMapperService;
    }

    public override async Task<ActiveArtistViewModel> CreateViewModelAsync(ArtistLinkViewModel entity)
    {
        var songs = await _dbContext.Set<Song>()
            .Include(x => x.Album)
            .ThenInclude(x => x.Artists)
            .Include(x => x.Album)
            .ThenInclude(x => x.AlbumCover)
            .Where(x => x.Album.Artists.Any(y => y.Id == entity.Id))
            .OrderByDescending(x => x.Duration)
            .ToListAsync();

        var artistPreviewViewModel = new ArtistPreviewViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Cover = songs.First().Album.AlbumCover
        };

        var result = _handMapperService
            .AddParameter("ArtistPreview", artistPreviewViewModel)
            .Map<ActiveArtistViewModel>(songs);

        return result;
    }
}