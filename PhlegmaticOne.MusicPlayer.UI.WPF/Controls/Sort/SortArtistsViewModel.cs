using System;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;

public class SortArtistsViewModel : SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    public SortArtistsViewModel(ILocalizationService localizationService) : base(localizationService)
    {
    }

    protected override Dictionary<string, Func<IEnumerable<ArtistPreviewViewModel>, IEnumerable<ArtistPreviewViewModel>>> GetAvailableSorts()
    {
        throw new NotImplementedException();
    }
}