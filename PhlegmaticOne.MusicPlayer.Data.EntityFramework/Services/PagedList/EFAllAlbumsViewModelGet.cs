using Calabonga.UnitOfWork;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;

public class EFAllAlbumsViewModelGet : EntityPagedListGetBase<AlbumPreviewViewModel>
{
    private readonly ApplicationDbContext _dbContext;

    public EFAllAlbumsViewModelGet(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override async Task<IPagedList<AlbumPreviewViewModel>> GetPagedListAsync(int pageSize, int pageIndex)
    {
        var set = _dbContext.Set<Album>();
        var result = await set.Select(x => new AlbumPreviewViewModel
        {
            Id = x.Id,
            Artists = x.Artists.Select(a => new ArtistLinkViewModel
            {
                Id = a.Id,
                Title = a.Title
            }).ToList(),
            Title = x.Title,
            Cover = x.AlbumCover.Cover,
            IsFavorite = x.IsFavorite,
            IsDownloaded = false,
            IsDownloading = false,
            DateAdded = x.DateAdded,
            YearReleased = x.YearReleased,
            AlbumType = x.AlbumType.ToString()
        }).ToPagedListAsync(pageSize:pageSize, pageIndex:pageIndex);
        return result;
    }
}