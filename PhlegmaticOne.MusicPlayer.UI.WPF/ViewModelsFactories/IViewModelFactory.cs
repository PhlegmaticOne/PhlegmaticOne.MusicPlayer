using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories;

public interface IViewModelFactory
{
    public ApplicationBaseViewModel CreateViewModel(ViewType viewType);
}