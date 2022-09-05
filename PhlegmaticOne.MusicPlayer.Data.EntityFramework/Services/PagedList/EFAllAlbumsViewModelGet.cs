using System.Linq.Expressions;
using Calabonga.UnitOfWork;
using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;

public class EFAllAlbumsViewModelGet : IEntityPagedListGet<AlbumPreviewViewModel>
{
    private readonly ApplicationDbContext _dbContext;

    public EFAllAlbumsViewModelGet(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IPagedList<AlbumPreviewViewModel>> GetPagedListAsync(int pageSize, int pageIndex,
        Func<AlbumPreviewViewModel, object>? sortFunc = null, Func<AlbumPreviewViewModel, bool>? selectFunc = null)
    {
        var set = _dbContext.Set<Album>();

        var modelSortExpression = AlbumExpressionsConverter.ConvertToSortExpression(sortFunc);

        var modelSelectExpression = AlbumExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Album> query = set;
        query = modelSelectExpression is null ? query : query.Where(modelSelectExpression);
        query = modelSortExpression is null ? query.OrderByDescending(x => x.DateAdded) : query.OrderBy(modelSortExpression);

        var result = query.Select(x => new AlbumPreviewViewModel
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
        });

        return await result.ToPagedListAsync(pageSize: pageSize, pageIndex: pageIndex);
    }
}