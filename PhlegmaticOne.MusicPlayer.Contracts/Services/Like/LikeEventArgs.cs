using PhlegmaticOne.MusicPlayer.Contracts.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Like;

public class LikeEventArgs : EventArgs
{
    public IIsFavorite Entity { get; }
    public bool NewLikeValue { get; }

    public LikeEventArgs(IIsFavorite entity, bool newLikeValue)
    {
        Entity = entity;
        NewLikeValue = newLikeValue;
    }
}