using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
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
            .Include(x => x.AlbumAppearances)
            .ThenInclude(x => x.AlbumCover)
            .Include(x => x.AlbumAppearances)
            .ThenInclude(x => (x as Album).Artists);

        var result = await query.ToListAsync();
        var tracks = new List<TrackBaseViewModel>();
        foreach (var song in result)
        {
            foreach (var songAlbumAppearance in song.AlbumAppearances)
            {
                var trackViewModel = new TrackBaseViewModel()
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
                    ArtistLinks = songAlbumAppearance is Album album ? album.Artists.Select(x => new ArtistLinkViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList() : new List<ArtistLinkViewModel>(),
                    CollectionLink = new CollectionLinkViewModel
                    {
                        Id = songAlbumAppearance.Id,
                        Cover = songAlbumAppearance.AlbumCover,
                        Title = songAlbumAppearance.Title
                    }
                };
                tracks.Add(trackViewModel);
            }
        }

        return new AllTracksViewModel { Tracks = tracks };
    }
}