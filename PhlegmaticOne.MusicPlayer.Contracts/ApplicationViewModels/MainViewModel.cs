using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

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