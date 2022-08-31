using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class HomeViewModel : PlayerTrackableViewModel
{
    public HomeViewModel(IPlayerService playerService, ILikeService likeService) : base(playerService, likeService)
    {
    }
}