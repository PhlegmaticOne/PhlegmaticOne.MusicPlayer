using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.Like;

public class EFLikeService : ILikeService
{
    private readonly ApplicationDbContext _dbContext;

    public EFLikeService(ApplicationDbContext dbContext) => 
        _dbContext = dbContext;

    public Task SetNewLike<TEntity>(TEntity entity, bool likeValue) where TEntity : IHaveId, IIsFavorite
    {
        throw new NotImplementedException();
    }

    public event EventHandler<LikeEventArgs>? LikeChanged;


    private void InvokeOnLikeChanged(IIsFavorite entity, bool newIsFavoriteValue) =>
        LikeChanged?.Invoke(this, new(entity, newIsFavoriteValue));
}