using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.PagedList;

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
            Name = x.Name,
            Cover = x.Albums.Last().AlbumCover,
            TracksCount = x.Albums.SelectMany(y => y.Songs).Count(),
            Genres = x.Albums.SelectMany(y => y.Genres).Distinct().Select(i => i.Name).ToList()
        }).ToPagedListAsync(pageSize:pageSize, pageIndex:pageIndex);

        return info;
    }
}