using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.Sort;

public class TracksSortOptionsProvider : ISortOptionsProvider<TrackBaseViewModel>
{
    public IDictionary<string, Func<TrackBaseViewModel, object>> GetSortOptions()
    {
        return new Dictionary<string, Func<TrackBaseViewModel, object>>()
        {
            { "By title", OrderingFuncsCollection.OrderByTitle },
            { "By duration", OrderingFuncsCollection.OrderByDuration },
            { "By time played", OrderingFuncsCollection.OrderByTimePlayed },
        };
    }
}