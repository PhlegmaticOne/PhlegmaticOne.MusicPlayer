using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Sort;

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