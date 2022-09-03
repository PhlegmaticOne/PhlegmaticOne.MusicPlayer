using PhlegmaticOne.MusicPlayer.Contracts;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Sort;

public class SortArtistsCollectionViewModel : SortViewModelBase<ArtistsCollectionViewModel, ArtistPreviewViewModel>
{
    public SortArtistsCollectionViewModel(ILocalizationService localizationService) : base(localizationService) { }

    protected override Dictionary<string, Func<IEnumerable<ArtistPreviewViewModel>, IEnumerable<ArtistPreviewViewModel>>> GetAvailableSorts()
    {
        var byNameText = LocalizationService.GetLocalizedValue(LocalizationConstants.SortByNameTextKey)!;
        return new()
        {
            { byNameText, (albums) => albums.OrderBy(x => x.Name)},
        };
    }

    protected override async Task SortViewModelAsync(ArtistsCollectionViewModel viewModel, 
        Func<IEnumerable<ArtistPreviewViewModel>, IEnumerable<ArtistPreviewViewModel>> sortFunc)
    {
        var sortedItems = sortFunc(viewModel.Items).ToList();
        await viewModel.UpdateItems(sortedItems);
    }
}