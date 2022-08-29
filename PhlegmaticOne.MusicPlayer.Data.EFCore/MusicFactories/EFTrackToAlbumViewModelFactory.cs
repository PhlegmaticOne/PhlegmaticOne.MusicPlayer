using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class EFTrackToAlbumViewModelFactory : NavigationFactoryBase<TrackBaseViewModel, ActiveAlbumViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperService;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;

    public EFTrackToAlbumViewModelFactory(ApplicationDbContext dbContext, IHandMapperService handMapperService,
        IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider)
    {
        _dbContext = dbContext;
        _handMapperService = handMapperService;
        _trackActionsProvider = trackActionsProvider;
    }

    public override async Task<ActiveAlbumViewModel> CreateViewModelAsync(TrackBaseViewModel entity)
    {
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
        foreach (var trackBaseViewModel in result.Tracks)
        {
            trackBaseViewModel.Actions = _trackActionsProvider.GetActions(trackBaseViewModel);
        }
        return result;
    }
}