using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class EFActiveArtistFromArtistPreviewViewModelFactory : NavigationFactoryBase<ArtistPreviewViewModel, ActiveArtistViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperService;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;

    public EFActiveArtistFromArtistPreviewViewModelFactory(ApplicationDbContext dbContext, IHandMapperService handMapperService, IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider)
    {
        _dbContext = dbContext;
        _handMapperService = handMapperService;
        _trackActionsProvider = trackActionsProvider;
    }

    public override async Task<ActiveArtistViewModel> CreateViewModelAsync(ArtistPreviewViewModel entity)
    {
        var songs = await _dbContext.Set<Song>()
            .Include(x => x.Artists)
            .Include(x => x.Album)
            .ThenInclude(x => x.AlbumCover)
            .Include(x => x.Album)
            .ThenInclude(x => x.Artists)
            .Where(x => x.Artists.Any(y => y.Id == entity.Id))
            .ToListAsync();

        var result = _handMapperService
            .AddParameter("ArtistPreview", entity)
            .Map<ActiveArtistViewModel>(songs);
        foreach (var trackBaseViewModel in result.Tracks)
        {
            trackBaseViewModel.Actions = _trackActionsProvider.GetActions(trackBaseViewModel);
        }
        return result;
    }
}