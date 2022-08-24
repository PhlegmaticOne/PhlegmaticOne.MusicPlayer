using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.WPF.Navigation;

public interface INavigationService
{
    event EventHandler<ApplicationBaseViewModel> ViewModelChanged;
    void NavigateTo(Type applicationViewModelType);
    void NavigateTo<T>() where T : ApplicationBaseViewModel;
    void NavigateTo(ApplicationBaseViewModel applicationBaseViewModel);
}