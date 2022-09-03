using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;

public class ReloadAlbumsCollectionViewModel : ReloadViewModelBase<AlbumsCollectionViewModel>
{
    public ReloadAlbumsCollectionViewModel(IEntityPagedListGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(AlbumsCollectionViewModel viewModel)
    {
        var pagedListViewModel = viewModel.PagedListViewModel;
        await pagedListViewModel.GetPage(pagedListViewModel.PageIndex);
    }
}