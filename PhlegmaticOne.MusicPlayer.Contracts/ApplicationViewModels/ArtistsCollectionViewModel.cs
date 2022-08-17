using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class ArtistsCollectionViewModel : CollectionViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    private readonly IEntityCollectionGetService _viewModelGetService;
    public ObservableCollection<ArtistPreviewViewModel> Artists { get; }
    public ArtistsCollectionViewModel(IPlayerService playerService, 
        ReloadViewModelBase<ArtistsCollectionViewModel> reloadViewModel, 
        SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel> sortViewModelBase,
        IUIThreadInvokerService uiThreadInvokerService,
        IEntityCollectionGetService viewModelGetService) : base(playerService, reloadViewModel, sortViewModelBase, uiThreadInvokerService)
    {
        _viewModelGetService = viewModelGetService;
        Artists = new();
        GetArtists();
    }

    private async void GetArtists()
    {
        var artists = await _viewModelGetService.GetEntityCollectionAsync<AllArtistsPreviewViewModel>();
        await UiThreadInvokerService.InvokeAsync(() =>
        {
            foreach (var artist in artists.Artists)
            {
                Artists.Add(artist);
            }
        });
    }
}