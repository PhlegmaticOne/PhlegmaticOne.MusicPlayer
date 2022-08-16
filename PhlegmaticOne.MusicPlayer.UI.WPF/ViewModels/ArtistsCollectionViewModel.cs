using System;
using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Services;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class ArtistsCollectionViewModel : CollectionViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    private readonly IViewModelGetService _viewModelGetService;
    public ObservableCollection<ArtistPreviewViewModel> Artists { get; }
    public ArtistsCollectionViewModel(IPlayerService playerService, 
        ReloadViewModelBase<ArtistsCollectionViewModel> reloadViewModel, 
        SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel> sortViewModelBase,
        IViewModelGetService viewModelGetService) : base(playerService, reloadViewModel, sortViewModelBase)
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