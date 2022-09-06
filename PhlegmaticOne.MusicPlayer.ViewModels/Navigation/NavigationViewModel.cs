using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Logo;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.Navigation;

public class NavigationViewModel : ApplicationBaseViewModel, IDisposable
{
    private readonly IChainNavigationService _chainNavigationService;
    public NavigationViewModel(IChainNavigationService chainNavigationService, ILogoProvider logoProvider)
    {
        _chainNavigationService = chainNavigationService;
        _chainNavigationService.DirectionCanMoveChanged += ChainNavigationServiceOnDirectionCanMoveChanged;
        _chainNavigationService.ViewModelChanged += ChainNavigationServiceOnViewModelChanged;

        MoveBackCommand = RelayCommandFactory.CreateCommand(MoveBack, _ => CanMoveBack);
        NavigateCommand = RelayCommandFactory.CreateRequiredParameterCommand<Type>(Navigate, _ => true);
        Logo = logoProvider.GetApplicationLogo();
        Navigate(typeof(HomeViewModel));
    }
    public Bitmap Logo { get; }
    public bool CanMoveBack { get; private set; }
    public ApplicationBaseViewModel CurrentViewModel { get; private set; }
    public IRelayCommand NavigateCommand { get; }
    public IRelayCommand MoveBackCommand { get; }
    private void MoveBack(object? parameter)
    {
        _chainNavigationService.Move(NavigationMoveDirection.Back);
    }

    private void Navigate(Type parameter)
    {
        _chainNavigationService.Reset();
        _chainNavigationService.NavigateTo(parameter);
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