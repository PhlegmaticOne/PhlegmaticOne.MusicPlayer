using PhlegmaticOne.MusicPlayer.Contracts.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAllFavoriteTracksViewModelGet : AdoNetAllTracksViewModelGetBase
{
    public AdoNetAllFavoriteTracksViewModelGet(ISqlClient sqlClient, IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider) : base(sqlClient, trackActionsProvider) { }
    protected override string CommandName => "Get_All_Favorite_Tracks";
}