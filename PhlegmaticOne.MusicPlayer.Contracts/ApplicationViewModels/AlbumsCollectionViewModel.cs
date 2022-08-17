using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;


namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public MusicNavigation<AlbumPreviewViewModel, AlbumViewModel> MusicNavigation { get; }

    public AlbumsCollectionViewModel(IPlayerService playerService, 
        ReloadViewModelBase<AlbumsCollectionViewModel> reloadViewModel,
        SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel> sortViewModelBase,
        MusicNavigation<AlbumPreviewViewModel, AlbumViewModel> musicNavigation,
        IUIThreadInvokerService uiThreadInvokerService) :
        base(playerService, reloadViewModel, sortViewModelBase, uiThreadInvokerService)
    {
        MusicNavigation = musicNavigation;
    }
}