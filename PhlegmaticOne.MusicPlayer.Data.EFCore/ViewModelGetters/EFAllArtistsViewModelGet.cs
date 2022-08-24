using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;


namespace PhlegmaticOne.MusicPlayer.Data.EFCore.ViewModelGetters;

public class EFAllArtistsViewModelGet : ViewModelGetBase<AllArtistsPreviewViewModel>
{
    private readonly ApplicationDbContext _applicationDb;

    public EFAllArtistsViewModelGet(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }
    public override async Task<AllArtistsPreviewViewModel> GetAsync()
    {
        var artists = _applicationDb.Set<Artist>();
        
        var info = await artists.Select(x => new ArtistPreviewViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Cover = x.Albums.Last().AlbumCover,
            TracksCount = x.Albums.SelectMany(y => y.Songs).Count(),
            Genres = x.Albums.SelectMany(y => y.Genres).Distinct().Select(i => i.Name).ToList()
        }).ToListAsync();

        return new AllArtistsPreviewViewModel
        {
            Artists = info
        };
    }
}