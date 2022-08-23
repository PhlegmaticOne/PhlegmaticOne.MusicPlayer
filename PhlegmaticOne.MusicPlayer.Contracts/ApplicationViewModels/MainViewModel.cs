using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Navigation;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

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