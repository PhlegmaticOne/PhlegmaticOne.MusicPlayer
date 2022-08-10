using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public interface INavigator
{
    public INavigationHistory History { get; set; }
    public BaseViewModel CurrentViewModel { get; set; }
    public ICommand NavigateCommand { get; }
    public ICommand MoveBackCommand { get; }
}

public static class NavigationExtensions
{
    public static void NavigateTo(this INavigator navigator, BaseViewModel baseViewModel)
    {
        navigator.History.Add(baseViewModel);
        navigator.CurrentViewModel = baseViewModel;
    }
}