using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.WPF.Navigation;

public interface IViewModelFactoryService
{
    ApplicationBaseViewModel? CreateViewModel(Type viewModelType);
}