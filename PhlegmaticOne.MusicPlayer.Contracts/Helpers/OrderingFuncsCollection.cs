using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;

namespace PhlegmaticOne.MusicPlayer.Contracts.Helpers;

public static class OrderingFuncsCollection
{
    public static Func<IHaveTitle, string> OrderByTitle => x => x.Title;
    public static Func<IHaveDuration, object> OrderByDuration => x => x.Duration;
    public static Func<IHaveDateAdded, object> OrderByDateAdded => x => x.DateAdded;
    public static Func<IHaveArtistName, string> OrderByArtistName => x => x.ArtistName;
    public static Func<IHaveYear, object> OrderByYear => x => x.YearReleased;
    public static Func<IHaveTimePlayed, object> OrderByTimePlayed => x => x.TimePlayed;
}