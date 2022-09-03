using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.Like;

public class EFLikeService : ILikeService
{
    private readonly ApplicationDbContext _dbContext;

    public EFLikeService(ApplicationDbContext dbContext) => 
        _dbContext = dbContext;

    public event EventHandler<LikeEventArgs>? LikeChanged;

    public async Task SetNewLike<TSet, TEntity>(TEntity entity, bool likeValue) 
        where TSet : EntityBase, IIsFavorite 
        where TEntity : IHaveId, IIsFavorite
    {
        var set = _dbContext.Set<TSet>();
        var found = await set
            .Where(x => x.Id == entity.Id)
            .FirstOrDefaultAsync();

        if (found is not null)
        {
            found.IsFavorite = likeValue;
            set.Update(found);
            await _dbContext.SaveChangesAsync();
            InvokeOnLikeChanged(entity, likeValue);
        }
    }

    private void InvokeOnLikeChanged(IIsFavorite entity, bool newIsFavoriteValue) =>
        LikeChanged?.Invoke(this, new(entity, newIsFavoriteValue));
}