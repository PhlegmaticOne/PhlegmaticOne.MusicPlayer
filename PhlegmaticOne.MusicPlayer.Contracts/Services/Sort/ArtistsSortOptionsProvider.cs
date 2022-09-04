using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Sort;

public class ArtistsSortOptionsProvider : ISortOptionsProvider<ArtistPreviewViewModel>
{
    public IDictionary<string, Func<ArtistPreviewViewModel, object>> GetSortOptions()
    {
        return new Dictionary<string, Func<ArtistPreviewViewModel, object>>
        {
            { "By title", OrderingFuncsCollection.OrderByTitle }
        };
    }
}