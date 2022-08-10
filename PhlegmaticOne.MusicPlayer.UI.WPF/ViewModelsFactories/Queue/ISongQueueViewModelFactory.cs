using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;

public interface ISongQueueViewModelFactory
{
    public BaseViewModel CreateQueueViewModel();
}