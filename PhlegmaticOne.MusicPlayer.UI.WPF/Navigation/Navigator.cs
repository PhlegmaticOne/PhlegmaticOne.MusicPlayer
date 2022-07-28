using System;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class Navigator : ObservableObject, INavigator
{
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IMusicViewModelsFactory _musicViewModelsFactory;
    private readonly DelegateCommand _navigationCommand;
    private readonly DelegateCommand _moveBackCommand;
    public Navigator(IViewModelFactory viewModelFactory,
        IMusicViewModelsFactory musicViewModelsFactory,
        INavigationHistory navigationHistory)
    {
        _viewModelFactory = viewModelFactory;
        _musicViewModelsFactory = musicViewModelsFactory;
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
            Album album => ProcessAlbum(album),
        _ => CurrentViewModel
        };

    private void MoveBack(object? parameter)
    {
        History.MoveBack();
        CurrentViewModel = History.Current.ViewModel;
    }

    private BaseViewModel ProcessAlbum(Album album)
    {
        var viewModel = _musicViewModelsFactory.CreateAlbumViewModel(album);
        History.Add(CurrentViewModel);
        History.Add(viewModel);
        return viewModel;
    }

    private BaseViewModel CreateViewModel(ViewType viewType)
    {
        History.Reset();
        return _viewModelFactory.CreateViewModel(viewType);
    }
}