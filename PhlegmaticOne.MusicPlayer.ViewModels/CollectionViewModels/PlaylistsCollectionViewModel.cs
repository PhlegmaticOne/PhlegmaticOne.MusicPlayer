using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

public class PlaylistsCollectionViewModel : ApplicationBaseViewModel
{
    public PlaylistsCollectionViewModel()
    {
        
    }
    public IRelayCommand CreatePlaylistCommand { get; }

    private void CreatePlaylist(object? parameter)
    {
    }
}