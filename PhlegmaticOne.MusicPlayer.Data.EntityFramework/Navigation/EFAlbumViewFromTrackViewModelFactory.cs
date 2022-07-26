﻿using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Actions;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;
using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Navigation;

public class EFAlbumViewFromTrackViewModelFactory : NavigationFactoryBase<TrackBaseViewModel, ActiveAlbumViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperService;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;

    public EFAlbumViewFromTrackViewModelFactory(ApplicationDbContext dbContext,
        IHandMapperService handMapperService,
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