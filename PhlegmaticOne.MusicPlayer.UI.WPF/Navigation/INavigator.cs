using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.UI.WPF.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public interface INavigator
{
    public ObservableObject CurrentViewModel { get; set; }
    public ICommand NavigateCommand { get; }
}