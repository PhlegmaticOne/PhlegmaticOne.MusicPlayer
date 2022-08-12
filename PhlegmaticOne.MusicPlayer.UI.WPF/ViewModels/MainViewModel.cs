using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class MainViewModel : ApplicationBaseViewModel
{
    public NavigationViewModel NavigationViewModel { get; }
    public PlayerViewModel PlayerViewModel { get; }
    public MainViewModel(NavigationViewModel navigationViewModel, PlayerViewModel playerViewModel)
    {
        NavigationViewModel = navigationViewModel;
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