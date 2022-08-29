using System.Collections.ObjectModel;
using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class HomeViewModel : PlayerTrackableViewModel
{
    public HomeViewModel(IPlayerService playerService, ILikeService likeService) : base(playerService, likeService)
    {
    }
}