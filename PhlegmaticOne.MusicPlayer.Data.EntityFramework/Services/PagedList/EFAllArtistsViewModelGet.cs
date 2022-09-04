using Calabonga.UnitOfWork;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;

public class EFAllArtistsViewModelGet : EntityPagedListGetBase<ArtistPreviewViewModel>
{
    private readonly ApplicationDbContext _applicationDb;

    public EFAllArtistsViewModelGet(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }
    public override async Task<IPagedList<ArtistPreviewViewModel>> GetPagedListAsync(int pageSize, int pageIndex)
    {
        var artists = _applicationDb.Set<Artist>();
        
        var info = await artists.Select(x => new ArtistPreviewViewModel
        {
            Id = x.Id,
            Title = x.Title,
            Cover = x.Albums.Last().AlbumCover.Cover,
            TracksCount = x.Albums.SelectMany(y => y.Songs).Count(),
            Genres = x.Albums.SelectMany(y => y.Genres).Distinct().Select(i => i.Title).ToList()
        }).ToPagedListAsync(pageSize:pageSize, pageIndex:pageIndex);

        return info;
    }
}