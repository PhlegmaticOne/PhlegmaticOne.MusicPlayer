using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;

public class ReloadTracksCollectionViewModel : ReloadViewModelBase<TracksViewModel>
{
    public ReloadTracksCollectionViewModel(IEntityPagedListGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(TracksViewModel viewModel)
    {
        var pagedListViewModel = viewModel.PagedListViewModel;
        await pagedListViewModel.GetPage(pagedListViewModel.PageIndex);
    }
}