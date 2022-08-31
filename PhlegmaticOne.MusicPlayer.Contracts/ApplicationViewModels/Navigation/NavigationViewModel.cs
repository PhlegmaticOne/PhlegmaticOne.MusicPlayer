using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Logo;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Navigation;

public class NavigationViewModel : ApplicationBaseViewModel, IDisposable
{
    private readonly IChainNavigationService _chainNavigationService;
    public NavigationViewModel(IChainNavigationService chainNavigationService, ILogoProvider logoProvider)
    {
        _chainNavigationService = chainNavigationService;
        _chainNavigationService.DirectionCanMoveChanged += ChainNavigationServiceOnDirectionCanMoveChanged;
        _chainNavigationService.ViewModelChanged += ChainNavigationServiceOnViewModelChanged;

        MoveBackCommand = DelegateCommandFactory.CreateCommand(MoveBack, _ => CanMoveBack);
        NavigateCommand = DelegateCommandFactory.CreateCommand(Navigate, _ => true);
        Logo = logoProvider.GetApplicationLogo();
        Navigate(typeof(HomeViewModel));
    }
    public Bitmap Logo { get; }
    public bool CanMoveBack { get; private set; }
    public ApplicationBaseViewModel CurrentViewModel { get; private set; } = null!;
    public IDelegateCommand NavigateCommand { get; }
    public IDelegateCommand MoveBackCommand { get; }
    private void MoveBack(object? parameter)
    {
        if (_chainNavigationService.CurrentViewModel is IDisposable disposable)
        {
            disposable.Dispose();
        }
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

    public void Dispose()
    {
        _chainNavigationService.DirectionCanMoveChanged -= ChainNavigationServiceOnDirectionCanMoveChanged;
        _chainNavigationService.ViewModelChanged -= ChainNavigationServiceOnViewModelChanged;
    }
}