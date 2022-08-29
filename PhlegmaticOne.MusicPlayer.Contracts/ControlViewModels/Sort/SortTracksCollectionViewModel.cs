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
        var byDurationFromMinText = LocalizationService.GetLocalizedValue(LocalizationConstants.SortByDurationFromMinTextKey)!;
        var byDurationFromMaxText = LocalizationService.GetLocalizedValue(LocalizationConstants.SortByDurationFromMaxTextKey)!;
        return new()
        {
            { byNameText, (tracks) => tracks.OrderBy(x => x.Title) },
            { byDurationFromMinText, (tracks) => tracks.OrderBy(x => x.Duration) },
            { byDurationFromMaxText, (tracks) => tracks.OrderByDescending(x => x.Duration) },
        };
    }

    protected override async Task SortViewModelAsync(TracksViewModel viewModel, Func<IEnumerable<TrackBaseViewModel>, IEnumerable<TrackBaseViewModel>> sortFunc)
    {
        var sortedItems = sortFunc(viewModel.Items).ToList();
        await viewModel.UpdateItems(sortedItems);
    }
}