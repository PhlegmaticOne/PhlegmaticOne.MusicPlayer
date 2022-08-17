namespace PhlegmaticOne.MusicPlayer.Contracts.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> ToOneItemEnumerable<T>(this T entity) => new List<T>() { entity };
}