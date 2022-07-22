using System;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.UI.WPF.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class Navigator : ObservableObject, INavigator
{
    private readonly DelegateCommand _navigationCommand;
    public Navigator()
    {
        CurrentViewModel = new SettingsViewModel();
        _navigationCommand = new((o) =>
        {
            switch (o)
            {
                case ViewType viewType:
                    switch (viewType)
                    {
                        case ViewType.Home:
                            CurrentViewModel = new HomeViewModel();
                            break;
                        default: break;
                    }
                    break;
            }
        }, s => true);
    }
    public ObservableObject CurrentViewModel { get; private set; }
    public ICommand NavigateCommand => _navigationCommand;
}