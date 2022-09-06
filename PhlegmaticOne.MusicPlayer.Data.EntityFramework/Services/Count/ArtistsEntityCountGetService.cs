using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Count;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Count;

public class ArtistsEntityCountGetService : IGetEntitiesCountGetService<ArtistPreviewViewModel>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ArtistsEntityCountGetService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<int> GetEntitiesCountAsync(Func<ArtistPreviewViewModel, bool>? selectFunc = null)
    {
        var set = _applicationDbContext.Set<Artist>();
        var selectExpression = ArtistExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Artist> query = set;
        query = selectExpression is null ? query : query.Where(selectExpression);

        return await query.CountAsync();
    }

    public int GetEntitiesCount(Func<ArtistPreviewViewModel, bool>? selectFunc = null)
    {
        var set = _applicationDbContext.Set<Artist>();
        var selectExpression = ArtistExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Artist> query = set;
        query = selectExpression is null ? query : query.Where(selectExpression);

        return query.Count();
    }
}