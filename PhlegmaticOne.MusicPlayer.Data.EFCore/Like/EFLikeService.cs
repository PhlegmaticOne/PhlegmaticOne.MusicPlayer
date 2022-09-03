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

    public Task SetNewLike<TEntity>(TEntity entity, bool likeValue) where TEntity : IHaveId, IIsFavorite
    {
        throw new NotImplementedException();
    }

    public event EventHandler<LikeEventArgs>? LikeChanged;


    private void InvokeOnLikeChanged(IIsFavorite entity, bool newIsFavoriteValue) =>
        LikeChanged?.Invoke(this, new(entity, newIsFavoriteValue));
}