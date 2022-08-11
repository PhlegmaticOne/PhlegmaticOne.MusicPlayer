using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public MusicNavigation<AlbumPreviewViewModel, AlbumViewModel> MusicNavigation { get; }

    public AlbumsCollectionViewModel(IPlayerService playerService, 
        ReloadViewModelBase<AlbumsCollectionViewModel> reloadViewModel,
        SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel> sortViewModelBase,
        MusicNavigation<AlbumPreviewViewModel, AlbumViewModel> musicNavigation) :
        base(playerService, reloadViewModel, sortViewModelBase)
    {
        MusicNavigation = musicNavigation;
    }
}