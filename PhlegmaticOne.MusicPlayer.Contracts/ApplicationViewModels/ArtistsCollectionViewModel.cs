using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class ArtistsCollectionViewModel : CollectionViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    private readonly IViewModelGetService _viewModelGetService;
    public ObservableCollection<ArtistPreviewViewModel> Artists { get; }
    public ArtistsCollectionViewModel(IPlayerService playerService, 
        ReloadViewModelBase<ArtistsCollectionViewModel> reloadViewModel, 
        SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel> sortViewModelBase,
        IUIThreadInvokerService uiThreadInvokerService,
        IViewModelGetService viewModelGetService) : base(playerService, reloadViewModel, sortViewModelBase, uiThreadInvokerService)
    {
        _viewModelGetService = viewModelGetService;
        Artists = new();
        GetArtists();
    }

    private async void GetArtists()
    {
        var artist =
            await _viewModelGetService.GetViewModelAsync<ArtistPreviewViewModel>(
                Guid.Parse("C6759305-EC5B-40FD-BD44-08DA7EEAD8B5"));
        Artists.Add(artist);
    }
}