using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }
    public PlayerViewModel PlayerViewModel { get; set; }

    public MainViewModel(INavigator navigator, PlayerViewModel playerViewModel)
    {
        Navigator = navigator;
        PlayerViewModel = playerViewModel;
    }

    public void Close()
    {
        PlayerViewModel.Dispose();
        //if(Player.CurrentSong is null) return;
        //Settings.Default.LatestSongId = Player.CurrentSong.Id;
        //Settings.Default.Save();
    }
}