using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;

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