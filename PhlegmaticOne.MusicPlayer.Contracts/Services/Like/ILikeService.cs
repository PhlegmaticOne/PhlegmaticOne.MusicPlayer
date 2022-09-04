using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Like;

public interface ILikeService
{
    Task SetNewLike<TEntity>(TEntity entity, bool likeValue) where TEntity : IHaveId, IIsFavorite;

    event EventHandler<LikeEventArgs> LikeChanged;
}