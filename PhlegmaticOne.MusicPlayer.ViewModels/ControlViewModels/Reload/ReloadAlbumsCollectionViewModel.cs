using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.MusicPlayer.ViewModels.CollectionViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;

public class ReloadAlbumsCollectionViewModel : ReloadViewModelBase<AlbumsCollectionViewModel>
{
    public ReloadAlbumsCollectionViewModel(IEntityPagedListGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(AlbumsCollectionViewModel viewModel)
    {
        var entities = await EntityCollectionGetService
            .GetPagedListAsync<AlbumPreviewViewModel>(2, 0);
        await viewModel.UpdateItems(entities.Items);
    }
}