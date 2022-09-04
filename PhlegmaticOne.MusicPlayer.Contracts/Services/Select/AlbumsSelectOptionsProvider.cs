using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Select;

public class AlbumsSelectOptionsProvider : ISelectOptionsProvider<AlbumPreviewViewModel>
{
    public IDictionary<string, Func<AlbumPreviewViewModel, bool>> GetSelectOptions()
    {
        return new Dictionary<string, Func<AlbumPreviewViewModel, bool>>()
        {
            { "All", SelectingFuncsCollection.SelectAll },
            { "Favorite", SelectingFuncsCollection.SelectFavorite }
        };
    }
}