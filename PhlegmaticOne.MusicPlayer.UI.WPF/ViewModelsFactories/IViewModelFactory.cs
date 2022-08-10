using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public interface IViewModelFactory
{
    public BaseViewModel CreateViewModel(ViewType viewType);
}