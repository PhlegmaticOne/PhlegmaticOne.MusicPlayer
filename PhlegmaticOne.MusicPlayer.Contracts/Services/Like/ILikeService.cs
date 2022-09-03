using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Data.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Like;

public interface ILikeService
{
    Task SetNewLike<TSet, TEntity>(TEntity entity, bool likeValue)
        where TEntity : IHaveId, IIsFavorite
        where TSet : EntityBase, IIsFavorite;

    event EventHandler<LikeEventArgs> LikeChanged;
}