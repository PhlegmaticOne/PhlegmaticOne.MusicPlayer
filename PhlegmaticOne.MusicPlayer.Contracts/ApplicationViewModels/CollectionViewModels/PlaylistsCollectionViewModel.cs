using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.CollectionViewModels;

public class PlaylistsCollectionViewModel : ApplicationBaseViewModel
{
    public PlaylistsCollectionViewModel()
    {
        
    }
    public IDelegateCommand CreatePlaylistCommand { get; }

    private void CreatePlaylist(object? parameter)
    {
    }
}