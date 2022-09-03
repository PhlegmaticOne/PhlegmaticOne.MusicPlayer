using PhlegmaticOne.MusicPlayer.Contracts;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Sort;

public class SortAlbumsCollectionViewModel : SortViewModelBase<AlbumsCollectionViewModel, AlbumPreviewViewModel>
{
    public SortAlbumsCollectionViewModel(ILocalizationService localizationService) : base(localizationService) { }

    protected override Dictionary<string, Func<IEnumerable<AlbumPreviewViewModel>, IEnumerable<AlbumPreviewViewModel>>> GetAvailableSorts()
    {
        var byNameText = LocalizationService.GetLocalizedValue(LocalizationConstants.SortByNameTextKey)!;
        var byDateAddedText = LocalizationService.GetLocalizedValue(LocalizationConstants.SortByDateAddedTextKey)!;
        var byArtistText = LocalizationService.GetLocalizedValue(LocalizationConstants.SortByArtistTextKey)!;
        return new()
        {
            { byNameText, (albums) => albums.OrderBy(x => x.Title)},
            { byDateAddedText, (albums) => albums.OrderBy(x => x.DateAdded)},
            { byArtistText, (albums) => albums.OrderBy(x => string.Join(" ", x.Artists))}
        };
    }

    protected override async Task SortViewModelAsync(AlbumsCollectionViewModel viewModel, 
        Func<IEnumerable<AlbumPreviewViewModel>, IEnumerable<AlbumPreviewViewModel>> sortFunc)
    {
        var sortedItems = sortFunc(viewModel.Items).ToList();
        await viewModel.UpdateItems(sortedItems);
    }
}