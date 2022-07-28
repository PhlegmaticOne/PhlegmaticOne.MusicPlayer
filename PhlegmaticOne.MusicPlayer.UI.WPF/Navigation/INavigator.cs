using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public interface INavigator
{
    public INavigationHistory History { get; set; }
    public BaseViewModel CurrentViewModel { get; set; }
    public ICommand NavigateCommand { get; }
    public ICommand MoveBackCommand { get; }
}