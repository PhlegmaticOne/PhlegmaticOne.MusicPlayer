using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class NavigationViewModel : ApplicationBaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IViewModelFactoryService _viewModelFactory;
    public NavigationViewModel(INavigationService navigationService, IViewModelFactoryService viewModelFactory)
    {
        _navigationService = navigationService;
        _viewModelFactory = viewModelFactory;
        _navigationService.CanMoveBackChanged += NavigationServiceOnCanMoveBackChanged;
        _navigationService.ViewModelChanged += NavigationServiceOnViewModelChanged;
        MoveBackCommand = new(MoveBack, _ => CanMoveBack);
        NavigateCommand = new(Navigate, _ => true);
        
        Navigate(ViewType.Home);
    }
    public bool CanMoveBack { get; private set; }
    public ApplicationBaseViewModel CurrentViewModel { get; private set; }
    public DelegateCommand NavigateCommand { get; }
    public DelegateCommand MoveBackCommand { get; }
    private void MoveBack(object? parameter)
    {
        _navigationService.MoveBack();
    }
    private void Navigate(object? parameter)
    {
        if (parameter is ViewType viewType)
        {
            var viewModel = _viewModelFactory.CreateViewModel(viewType);
            _navigationService.NavigateTo(viewModel, true);
        }
    }
    private void NavigationServiceOnCanMoveBackChanged(object? sender, bool e)
    {
        CanMoveBack = e;
        MoveBackCommand.RaiseCanExecute();
    }
    private void NavigationServiceOnViewModelChanged(object? sender, EventArgs e)
    {
        CurrentViewModel = _navigationService.CurrentViewModel;
    }
}