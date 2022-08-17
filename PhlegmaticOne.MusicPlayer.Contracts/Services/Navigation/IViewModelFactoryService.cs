using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;

public interface IViewModelFactoryService
{
    public ApplicationBaseViewModel CreateViewModel(ViewType viewType);
}