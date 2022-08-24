using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.CollectionViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;

public class ReloadTracksCollectionViewModel : ReloadViewModelBase<TracksViewModel>
{
    public ReloadTracksCollectionViewModel(IEntityCollectionGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(TracksViewModel viewModel)
    {
        var entities = await EntityCollectionGetService.GetEntityCollectionAsync<AllTracksViewModel>();
        await viewModel.UpdateItems(entities.Tracks);
    }
}