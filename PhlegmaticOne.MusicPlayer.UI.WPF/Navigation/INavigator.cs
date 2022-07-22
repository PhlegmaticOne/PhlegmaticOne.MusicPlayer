using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.UI.WPF.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public interface INavigator
{
    public ObservableObject CurrentViewModel { get; }
    public ICommand NavigateCommand { get; }
}