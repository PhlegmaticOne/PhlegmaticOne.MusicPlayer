using Calabonga.UnitOfWork;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;

public class EFAllArtistsViewModelGet : IEntityPagedListGet<ArtistPreviewViewModel>
{
    private readonly ApplicationDbContext _applicationDb;

    public EFAllArtistsViewModelGet(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }
    public async Task<IPagedList<ArtistPreviewViewModel>> GetPagedListAsync(int pageSize, int pageIndex,
        Func<ArtistPreviewViewModel, object>? sortFunc = null, Func<ArtistPreviewViewModel, bool>? selectFunc = null)
    {
        var set = _applicationDb.Set<Artist>();

        var modelSortExpression = ArtistExpressionsConverter.ConvertToSortExpression(sortFunc);

        var modelSelectExpression = ArtistExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Artist> query = set;
        query = modelSelectExpression is null ? query : query.Where(modelSelectExpression);
        query = modelSortExpression is null ? query : query.OrderBy(modelSortExpression);

        var artists = query.Select(x => new ArtistPreviewViewModel
        {
            Id = x.Id,
            Title = x.Title,
            Cover = x.Albums.First().AlbumCover.Cover,
            TracksCount = x.Albums.SelectMany(y => y.Songs).Count(),
            Genres = x.Albums.SelectMany(y => y.Genres).Distinct().Select(i => i.Title).ToList()
        });

        return await artists.ToPagedListAsync(pageSize: pageSize, pageIndex: pageIndex); ;
    }
}