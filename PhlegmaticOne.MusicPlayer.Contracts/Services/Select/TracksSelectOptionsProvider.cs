using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Select;

public class TracksSelectOptionsProvider : ISelectOptionsProvider<TrackBaseViewModel>
{
    public IDictionary<string, Func<TrackBaseViewModel, bool>> GetSelectOptions()
    {
        return new Dictionary<string, Func<TrackBaseViewModel, bool>>
        {
            { "All", SelectingFuncsCollection.SelectAll },
            { "Favorite", SelectingFuncsCollection.SelectFavorite },
            { "Downloaded", SelectingFuncsCollection.SelectDownloaded }
        };
    }
}