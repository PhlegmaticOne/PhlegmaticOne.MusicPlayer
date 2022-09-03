using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;

public class ReloadTracksCollectionViewModel : ReloadViewModelBase<TracksViewModel>
{
    public ReloadTracksCollectionViewModel(IEntityPagedListGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(TracksViewModel viewModel)
    {
        var entities = await EntityCollectionGetService
            .GetPagedListAsync<TrackBaseViewModel>(0, 0);
        await viewModel.UpdateItems(entities.Items);
    }
}