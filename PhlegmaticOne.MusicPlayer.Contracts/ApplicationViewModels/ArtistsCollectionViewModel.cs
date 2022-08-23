using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using PhlegmaticOne.WPF.Navigation;
using PhlegmaticOne.WPF.Navigation.EntityContainingViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class ArtistsCollectionViewModel : CollectionViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    private readonly IEntityCollectionGetService _viewModelGetService;
    public ObservableCollection<ArtistPreviewViewModel> Artists { get; }
    public ArtistsCollectionViewModel(IPlayerService playerService, 
        ReloadViewModelBase<ArtistsCollectionViewModel> reloadViewModel, 
        SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel> sortViewModelBase,
        IUIThreadInvokerService uiThreadInvokerService,
        IEntityCollectionGetService viewModelGetService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService) :
        base(playerService, reloadViewModel, sortViewModelBase, uiThreadInvokerService, entityContainingViewModelsNavigationService)
    {
        _viewModelGetService = viewModelGetService;
        Artists = new();
        ActiveArtistNavigationCommand = new(NavigateToArtist, _ => true);
        GetArtists();
    }
    public DelegateCommand ActiveArtistNavigationCommand { get; }
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

    private async void NavigateToArtist(object? parameter)
    {
        if (parameter is ArtistPreviewViewModel artistPreviewViewModel)
        {
            await EntityContainingViewModelsNavigationService.From<ArtistPreviewViewModel, ActiveArtistViewModel>()
                .NavigateAsync<ArtistViewModel>(artistPreviewViewModel);
        }
    }
}