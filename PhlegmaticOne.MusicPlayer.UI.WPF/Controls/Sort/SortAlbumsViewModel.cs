using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;

public class SortAlbumsViewModel : SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public SortAlbumsViewModel(ILocalizationService localizationService) : base(localizationService) { }

    protected override Dictionary<string, Func<IEnumerable<AlbumPreviewViewModel>, IEnumerable<AlbumPreviewViewModel>>> GetAvailableSorts()
    {
        var byNameText = LocalizationService.GetLocalizedValue("ByNameText");
        var byDateAddedText = LocalizationService.GetLocalizedValue("ByDateAddedText");
        var byArtistText = LocalizationService.GetLocalizedValue("ByArtistText");
        return new()
        {
            { byNameText, (albums) => albums.OrderBy(x => x.Title)},
            { byDateAddedText, (albums) => albums.OrderBy(x => x.DateAdded)},
            { byArtistText, (albums) => albums.OrderBy(x => x.Artists.First())}
        };
    }
}