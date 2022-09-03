using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.EntityContainingViewModels;

public class PlaylistViewModel : ApplicationBaseViewModel, IEntityContainingViewModel<ActivePlaylistViewModel>
{
    public ActivePlaylistViewModel Entity { get; set; }
}