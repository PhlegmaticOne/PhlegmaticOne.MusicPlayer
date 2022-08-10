using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumEntityViewModel, AlbumsCollectionViewModel>
{
    public AlbumsCollectionViewModel(ReloadViewModelBase<AlbumsCollectionViewModel> reloadViewModel,
        SortViewModelBase<AlbumsCollectionViewModel, AlbumEntityViewModel> sortViewModelBase,
        MusicNavigationBase<AlbumEntityViewModel> musicNavigationBase,
        IPlayerService playerService) :
        base(reloadViewModel, sortViewModelBase, musicNavigationBase, playerService)
    {
    }
}