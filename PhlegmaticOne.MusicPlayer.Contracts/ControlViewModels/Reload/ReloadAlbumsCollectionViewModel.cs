using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.CollectionViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;

public class ReloadAlbumsCollectionViewModel : ReloadViewModelBase<AlbumsCollectionViewModel>
{
    public ReloadAlbumsCollectionViewModel(IEntityCollectionGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(AlbumsCollectionViewModel viewModel)
    {
        var entities = await EntityCollectionGetService.GetEntityCollectionAsync<AllAlbumsPreviewViewModel>();
        await viewModel.UpdateItems(entities.Albums);
    }
}