using System.Collections.Generic;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> ToOneItemEnumerable<T>(this T entity) => new List<T>() { entity };
}