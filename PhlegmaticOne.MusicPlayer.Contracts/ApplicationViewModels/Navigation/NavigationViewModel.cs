using PhlegmaticOne.MusicPlayer.WPF.Core;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Navigation;

public class NavigationViewModel : ApplicationBaseViewModel
{
    private readonly IChainNavigationService _chainNavigationService;
    public NavigationViewModel(IChainNavigationService chainNavigationService)
    {
        _chainNavigationService = chainNavigationService;
        _chainNavigationService.DirectionCanMoveChanged += ChainNavigationServiceOnDirectionCanMoveChanged;
        _chainNavigationService.ViewModelChanged += ChainNavigationServiceOnViewModelChanged;

        MoveBackCommand = new(MoveBack, _ => CanMoveBack);
        NavigateCommand = new(Navigate, _ => true);

        Navigate(typeof(HomeViewModel));
    }

    private void ChainNavigationServiceOnViewModelChanged(object? sender, ApplicationBaseViewModel e)
    {
        CurrentViewModel = e;
    }

    private void ChainNavigationServiceOnDirectionCanMoveChanged(object? sender, NavigationMoveDirectionChangedArgs e)
    {
        if (e.NavigationMoveDirection == NavigationMoveDirection.Back)
        {
            CanMoveBack = e.CanMove;
            MoveBackCommand.RaiseCanExecute();
        }
    }

    public bool CanMoveBack { get; private set; }
    public ApplicationBaseViewModel CurrentViewModel { get; private set; }
    public DelegateCommand NavigateCommand { get; }
    public DelegateCommand MoveBackCommand { get; }
    private void MoveBack(object? parameter)
    {
        _chainNavigationService.Move(NavigationMoveDirection.Back);
    }

    private void Navigate(object? parameter)
    {
        if (parameter is Type viewModelType)
        {
            _chainNavigationService.Reset();
            _chainNavigationService.NavigateTo(viewModelType);
        }
    }
}