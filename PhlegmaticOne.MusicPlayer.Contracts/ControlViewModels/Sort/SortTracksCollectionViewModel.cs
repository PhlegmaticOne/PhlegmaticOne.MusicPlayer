using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.CollectionViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;

public class SortTracksCollectionViewModel : SortViewModelBase<TracksViewModel, TrackBaseViewModel>
{
    public SortTracksCollectionViewModel(ILocalizationService localizationService) : base(localizationService) { }

    protected override Dictionary<string, Func<IEnumerable<TrackBaseViewModel>, IEnumerable<TrackBaseViewModel>>> GetAvailableSorts()
    {
        var byNameText = LocalizationService.GetLocalizedValue(LocalizationConstants.SortByNameTextKey)!;
        return new()
        {
            { byNameText, (albums) => albums.OrderBy(x => x.Title)},
        };
    }

    protected override async Task SortViewModelAsync(TracksViewModel viewModel, Func<IEnumerable<TrackBaseViewModel>, IEnumerable<TrackBaseViewModel>> sortFunc)
    {
        var sortedItems = sortFunc(viewModel.Items).ToList();
        await viewModel.UpdateItems(sortedItems);
    }
}