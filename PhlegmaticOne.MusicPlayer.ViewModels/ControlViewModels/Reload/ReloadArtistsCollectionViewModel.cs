using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;

public class ReloadArtistsCollectionViewModel : ReloadViewModelBase<ArtistsCollectionViewModel>
{
    public ReloadArtistsCollectionViewModel(IEntityPagedListGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(ArtistsCollectionViewModel viewModel)
    {
        var pagedListViewModel = viewModel.PagedListViewModel;
        await pagedListViewModel.GetPage(pagedListViewModel.PageIndex);
    }
}