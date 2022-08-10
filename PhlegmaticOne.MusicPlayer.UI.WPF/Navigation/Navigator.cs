using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class Navigator : ObservableObject, INavigator
{
    private readonly IViewModelFactory _viewModelFactory;
    private readonly DelegateCommand _navigationCommand;
    private readonly DelegateCommand _moveBackCommand;
    public Navigator(IViewModelFactory viewModelFactory, INavigationHistory navigationHistory)
    {
        _viewModelFactory = viewModelFactory;
        History = navigationHistory;

        _moveBackCommand = new(MoveBack, _ => History.CanMoveBack);
        _navigationCommand = new(Navigate, _ => true);

        History.HistoryChanged += HistoryOnHistoryChanged;

        Navigate(ViewType.Home);
    }

    private void HistoryOnHistoryChanged(object? sender, EventArgs e) =>
        _moveBackCommand.RaiseCanExecute();

    public INavigationHistory History { get; set; }
    public BaseViewModel CurrentViewModel { get; set; }
    public ICommand NavigateCommand => _navigationCommand;
    public ICommand MoveBackCommand => _moveBackCommand;

    private void Navigate(object? parameter) =>
        CurrentViewModel = parameter switch
        {
            ViewType viewType => CreateViewModel(viewType),
            _ => CurrentViewModel
        };

    private void MoveBack(object? parameter)
    {
        History.MoveBack();
        CurrentViewModel = History.Current.ViewModel;
    }

    private BaseViewModel CreateViewModel(ViewType viewType)
    {
        var newViewModel = _viewModelFactory.CreateViewModel(viewType);
        History.Reset();
        History.Add(newViewModel);
        return newViewModel;
    }
}