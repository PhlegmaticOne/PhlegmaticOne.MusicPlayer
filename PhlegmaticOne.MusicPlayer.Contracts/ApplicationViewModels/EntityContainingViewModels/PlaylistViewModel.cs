using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;

public class PlaylistViewModel : ApplicationBaseViewModel, IEntityContainingViewModel<ActivePlaylistViewModel>
{
    public ActivePlaylistViewModel Entity { get; set; }
}