using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.Sort;

public class AlbumsSortOptionsProvider : ISortOptionsProvider<AlbumPreviewViewModel>
{
    public IDictionary<string, Func<AlbumPreviewViewModel, object>> GetSortOptions()
    {
        return new Dictionary<string, Func<AlbumPreviewViewModel, object>>
        {
            { "By title", OrderingFuncsCollection.OrderByTitle },
            { "By date added", OrderingFuncsCollection.OrderByDateAdded },
            { "By year", OrderingFuncsCollection.OrderByYear },
            { "By artists", OrderingFuncsCollection.OrderByArtistName },
        };
    }
}