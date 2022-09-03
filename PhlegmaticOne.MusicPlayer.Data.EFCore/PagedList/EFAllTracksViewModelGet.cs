﻿using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.PagedList;

public class EFAllTracksViewModelGet : EntityPagedListGetBase<TrackBaseViewModel>
{
    private readonly ApplicationDbContext _dbContext;

    public EFAllTracksViewModelGet(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override async Task<IPagedList<TrackBaseViewModel>> GetPagedListAsync(int pageSize, int pageIndex)
    {
        var set = _dbContext.Set<Song>();
        var query = await set
            .Include(x => x.Album)
            .ThenInclude(x => x.AlbumCover)
            .Include(x => x.Artists)
            .Include(x => x.Playlists)
            .ThenInclude(x => x.AlbumCover)
            .Select(x => new TrackBaseViewModel())
            .ToPagedListAsync(pageSize: pageSize, pageIndex:pageIndex);

        //var result = await query.ToListAsync();
        //var tracks = new List<TrackBaseViewModel>();

        //foreach (var song in result)
        //{
        //    var trackModel = new TrackBaseViewModel
        //    {
        //        Title = song.Title,
        //        Duration = song.Duration,
        //        Id = song.Id,
        //        IsDownloaded = string.IsNullOrWhiteSpace(song.LocalUrl) == false,
        //        IsDownloading = false,
        //        LocalUrl = song.LocalUrl,
        //        IsFavorite = song.IsFavorite,
        //        OnlineUrl = song.OnlineUrl,
        //        TimePlayed = song.TimePlayed,
        //        CollectionLink = new CollectionLinkViewModel()
        //        {
        //            Id = song.AlbumId,
        //            Cover = song.Album.AlbumCover,
        //            Title = song.Album.Title,
        //        },
        //        ArtistLinks = song.Artists.Select(x => new ArtistLinkViewModel
        //        {
        //            Id = x.Id,
        //            Name = x.Name
        //        }).ToList()
        //    };
        //    tracks.Add(trackModel);
        //    foreach (var songAlbumAppearance in song.Playlists)
        //    {
        //        var trackBase = new TrackBaseViewModel
        //        {
        //            Title = song.Title,
        //            Duration = song.Duration,
        //            Id = song.Id,
        //            IsDownloaded = string.IsNullOrWhiteSpace(song.LocalUrl),
        //            IsDownloading = false,
        //            LocalUrl = song.LocalUrl,
        //            IsFavorite = song.IsFavorite,
        //            OnlineUrl = song.OnlineUrl,
        //            TimePlayed = song.TimePlayed,
        //            CollectionLink = new CollectionLinkViewModel
        //            {
        //                Id = songAlbumAppearance.Id,
        //                Cover = songAlbumAppearance.AlbumCover,
        //                Title = songAlbumAppearance.Title,
        //            },
        //            ArtistLinks = new List<ArtistLinkViewModel>()
        //        };
        //        tracks.Add(trackBase);
        //    }
        //}

        return query;
    }
}