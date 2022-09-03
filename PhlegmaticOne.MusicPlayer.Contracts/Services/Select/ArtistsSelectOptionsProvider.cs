using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Contracts.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Select;

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