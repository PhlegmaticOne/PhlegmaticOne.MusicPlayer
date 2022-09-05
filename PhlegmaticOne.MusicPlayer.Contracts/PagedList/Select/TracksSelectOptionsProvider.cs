using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.Select;

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