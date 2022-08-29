using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAllFavoriteTracksViewModelGet : AdoNetAllTracksViewModelGetBase
{
    public AdoNetAllFavoriteTracksViewModelGet(ISqlClient sqlClient) : base(sqlClient) { }
    protected override string CommandName => "Get_All_Favorite_Tracks";
}