using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;

namespace PhlegmaticOne.MusicPlayer.Contracts.Helpers;

public static class SelectingFuncsCollection
{
    public static Func<IIsFavorite, bool> SelectFavorite => x => x.IsFavorite;
    public static Func<IIsDownloaded, bool> SelectDownloaded => x => x.IsDownloaded;
    public static Func<object, bool> SelectAll => _ => true;
}