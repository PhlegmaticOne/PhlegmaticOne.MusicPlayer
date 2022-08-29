using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Like;

public interface ILikeService
{
    Task SetNewLike<TSet, TEntity>(TEntity entity, bool likeValue)
        where TEntity : IHaveId, IIsFavorite
        where TSet : EntityBase, IIsFavorite;

    event EventHandler<LikeEventArgs> LikeChanged;
}