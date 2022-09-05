using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Count;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Count;

public class AlbumsEntityCountGetService : IGetEntitiesCountGetService<AlbumPreviewViewModel>
{
    private readonly ApplicationDbContext _dbContext;

    public AlbumsEntityCountGetService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> GetEntitiesCountAsync(Func<AlbumPreviewViewModel, bool>? selectFunc)
    {
        var set = _dbContext.Set<Album>();
        var selectExpression = AlbumExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Album> query = set;
        query = selectExpression is null ? query : query.Where(selectExpression);

        return await query.CountAsync();
    }

    public int GetEntitiesCount(Func<AlbumPreviewViewModel, bool>? selectFunc)
    {
        var set = _dbContext.Set<Album>();
        var selectExpression = AlbumExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Album> query = set;
        query = selectExpression is null ? query : query.Where(selectExpression);

        return query.Count();
    }
}