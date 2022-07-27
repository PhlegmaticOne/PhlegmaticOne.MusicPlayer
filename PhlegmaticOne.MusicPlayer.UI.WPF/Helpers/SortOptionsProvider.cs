using System.Collections.Generic;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

public class SortOptionsProvider : ISortOptionsProvider
{
    public IEnumerable<SortDescription> GetSortDescriptions()
    {
        yield return new SortDescription(SortType.Title, "By title");
        yield return new SortDescription(SortType.ArtistName, "By artist name");
        yield return new SortDescription(SortType.DateAdded, "By date added");
    }
}