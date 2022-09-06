using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Count;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Count;

public class TracksEntityCountGetService : IGetEntitiesCountGetService<TrackBaseViewModel>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public TracksEntityCountGetService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<int> GetEntitiesCountAsync(Func<TrackBaseViewModel, bool>? selectFunc = null)
    {
        var set = _applicationDbContext.Set<Song>();
        var selectExpression = TrackExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Song> query = set;
        query = selectExpression is null ? query : query.Where(selectExpression);

        return await query.CountAsync();
    }

    public int GetEntitiesCount(Func<TrackBaseViewModel, bool>? selectFunc = null)
    {
        var set = _applicationDbContext.Set<Song>();
        var selectExpression = TrackExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Song> query = set;
        query = selectExpression is null ? query : query.Where(selectExpression);

        return query.Count();
    }
}