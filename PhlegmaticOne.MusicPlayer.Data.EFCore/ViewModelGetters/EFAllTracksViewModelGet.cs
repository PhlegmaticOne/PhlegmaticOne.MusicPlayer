using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.ViewModelGetters;

public class EFAllTracksViewModelGet : ViewModelGetBase<AllTracksViewModel>
{
    private readonly ApplicationDbContext _dbContext;

    public EFAllTracksViewModelGet(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override async Task<AllTracksViewModel> GetAsync()
    {
        var set = _dbContext.Set<Song>();
        var query = set
            .Include(x => x.Album)
            .ThenInclude(x => x.AlbumCover)
            .Include(x => x.Album)
            .ThenInclude(x => x.Artists)
            .Include(x => x.Playlists)
            .ThenInclude(x => x.AlbumCover);

        var result = await query.ToListAsync();
        var tracks = new List<TrackBaseViewModel>();

        foreach (var song in result)
        {
            var trackModel = new TrackBaseViewModel
            {
                Title = song.Title,
                Duration = song.Duration,
                Id = song.Id,
                IsDownloaded = string.IsNullOrWhiteSpace(song.LocalUrl) == false,
                IsDownloading = false,
                LocalUrl = song.LocalUrl,
                IsFavorite = song.IsFavorite,
                OnlineUrl = song.OnlineUrl,
                TimePlayed = song.TimePlayed,
                CollectionLink = new CollectionLinkViewModel()
                {
                    Id = song.AlbumId,
                    Cover = song.Album.AlbumCover,
                    Title = song.Album.Title,
                },
                ArtistLinks = song.Album.Artists.Select(x => new ArtistLinkViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
            tracks.Add(trackModel);
            foreach (var songAlbumAppearance in song.Playlists)
            {
                var trackBase = new TrackBaseViewModel
                {
                    Title = song.Title,
                    Duration = song.Duration,
                    Id = song.Id,
                    IsDownloaded = string.IsNullOrWhiteSpace(song.LocalUrl),
                    IsDownloading = false,
                    LocalUrl = song.LocalUrl,
                    IsFavorite = song.IsFavorite,
                    OnlineUrl = song.OnlineUrl,
                    TimePlayed = song.TimePlayed,
                    CollectionLink = new CollectionLinkViewModel
                    {
                        Id = songAlbumAppearance.Id,
                        Cover = songAlbumAppearance.AlbumCover,
                        Title = songAlbumAppearance.Title,
                    },
                    ArtistLinks = new List<ArtistLinkViewModel>()
                };
                tracks.Add(trackBase);
            }
        }

        return new AllTracksViewModel { Tracks = tracks };
    }
}