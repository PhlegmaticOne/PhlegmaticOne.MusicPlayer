using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.ViewModelGetters;

public class EFAllAlbumsViewModelGet : ViewModelGetBase<AllAlbumsPreviewViewModel>
{
    private readonly ApplicationDbContext _dbContext;

    public EFAllAlbumsViewModelGet(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override async Task<AllAlbumsPreviewViewModel> GetAsync()
    {
        var set = _dbContext.Set<Album>();
        var result = await set.Select(x => new AlbumPreviewViewModel
        {
            Id = x.Id,
            Artists = x.Artists.Select(a => new ArtistLinkViewModel
            {
                Id = a.Id,
                Name = a.Name
            }).ToList(),
            Title = x.Title,
            Cover = x.AlbumCover,
            IsFavorite = x.IsFavorite,
            IsDownloaded = false,
            IsDownloading = false,
            DateAdded = x.DateAdded,
            YearReleased = x.YearReleased,
            AlbumType = x.AlbumType
        }).ToListAsync();
        return new AllAlbumsPreviewViewModel() { Albums = result };
    }
}