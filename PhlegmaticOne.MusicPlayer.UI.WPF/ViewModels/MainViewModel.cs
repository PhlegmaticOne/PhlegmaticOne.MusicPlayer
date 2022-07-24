using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }

    public MainViewModel(INavigator navigator)
    {
        Navigator = navigator;
    }
}