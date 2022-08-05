using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;

public interface ISongQueueViewModelFactory
{
    public BaseViewModel CreateQueueViewModel();
}