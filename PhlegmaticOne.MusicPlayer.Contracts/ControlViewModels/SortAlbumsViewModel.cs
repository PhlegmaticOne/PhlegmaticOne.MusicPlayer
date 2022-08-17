using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels;

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