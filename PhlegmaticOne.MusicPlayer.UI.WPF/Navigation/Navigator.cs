using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.UI.WPF.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class Navigator : ObservableObject, INavigator
{
    private readonly DelegateCommand _navigationCommand;
    public Navigator(IViewModelFactory viewModelFactory)
    {
        CurrentViewModel = new HomeViewModel();
        _navigationCommand = new(o =>
            {
                CurrentViewModel = o switch
                {
                    ViewType viewType => viewModelFactory.CreateViewModel(viewType),
                    _ => CurrentViewModel
                };
            }, 
            _ => true);
    }
    public ObservableObject CurrentViewModel { get; set; }
    public ICommand NavigateCommand => _navigationCommand;
}