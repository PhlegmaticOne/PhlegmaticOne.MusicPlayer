using PhlegmaticOne.MusicPlayer.Contracts.Services.Actions;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.PagedList;

public class AdoNetAllFavoriteTracksViewModelGet : AdoNetTracksPagedListGetBase
{
    public AdoNetAllFavoriteTracksViewModelGet(ISqlClient sqlClient, IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider) : base(sqlClient, trackActionsProvider) { }
    protected override string CommandName => "Get_All_Favorite_Tracks";
}