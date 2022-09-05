using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.Select;

public class ArtistsSelectOptionsProvider : ISelectOptionsProvider<ArtistPreviewViewModel>
{
    public IDictionary<string, Func<ArtistPreviewViewModel, bool>> GetSelectOptions()
    {
        return new Dictionary<string, Func<ArtistPreviewViewModel, bool>>
        {
            { "All", SelectingFuncsCollection.SelectAll },
            { "Favorite", SelectingFuncsCollection.SelectFavorite },
        };
    }
}