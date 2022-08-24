using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;

public class PlaylistViewModel : ApplicationBaseViewModel, IEntityContainingViewModel<ActivePlaylistViewModel>
{
    public ActivePlaylistViewModel Entity { get; set; }
}