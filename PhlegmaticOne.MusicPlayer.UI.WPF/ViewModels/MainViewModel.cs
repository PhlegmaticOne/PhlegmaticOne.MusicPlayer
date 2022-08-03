using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.Players;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }
    public IPlayer Player { get; set; }

    public MainViewModel(INavigator navigator, PlayersFactory playersFactory)
    {
        Navigator = navigator;
        Player = playersFactory.CreatePlayer();
    }

    public void Close()
    {
        //if(Player.CurrentSong is null) return;
        //Settings.Default.LatestSongId = Player.CurrentSong.Id;
        //Settings.Default.Save();
    }
}