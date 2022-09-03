using PhlegmaticOne.MusicPlayer.ViewModels.Navigation;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels;

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
    }
}