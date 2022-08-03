using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class PlayerViewModel : BaseViewModel
{
    public IPlayer Player { get; set; }

    public PlayerViewModel(IPlayer player)
    {
        Player = player;
    }
}