﻿using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class EFActiveAlbumFromAlbumPreviewViewModelFactory : NavigationFactoryBase<AlbumPreviewViewModel, ActiveAlbumViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperProvider;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;

    public EFActiveAlbumFromAlbumPreviewViewModelFactory(ApplicationDbContext dbContext, IHandMapperService handMapperProvider,
        IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider)
    {
        _dbContext = dbContext;
        _handMapperProvider = handMapperProvider;
        _trackActionsProvider = trackActionsProvider;
    }

    public override async Task<ActiveAlbumViewModel> CreateViewModelAsync(AlbumPreviewViewModel entity)
    {
        var set = _dbContext.Set<Album>();
        var songs = await set
            .Where(x => x.Id == entity.Id)
            .Include(x => x.Songs)
            .ThenInclude(x => x.Artists)
            .SelectMany(x => x.Songs)
            .ToListAsync();
        var mapped = _handMapperProvider
            .AddParameter("CollectionSongs", songs)
            .Map<ActiveAlbumViewModel>(entity);
        foreach (var trackBaseViewModel in mapped.Tracks)
        {
            trackBaseViewModel.Actions = _trackActionsProvider.GetActions(trackBaseViewModel);
        }
        return mapped;
    }
}