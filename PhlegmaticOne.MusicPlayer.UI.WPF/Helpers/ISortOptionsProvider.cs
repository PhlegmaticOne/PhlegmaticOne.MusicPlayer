using System.Collections.Generic;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

public interface ISortOptionsProvider
{
    public IEnumerable<SortDescription> GetSortDescriptions();
}