using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;

public interface INavigationService
{
    public event EventHandler<bool> CanMoveBackChanged;
    public event EventHandler ViewModelChanged;
    public ApplicationBaseViewModel CurrentViewModel { get; }
    public void NavigateTo(ApplicationBaseViewModel viewModel, bool clearBeforeAdd = false);
    public void MoveBack();
}