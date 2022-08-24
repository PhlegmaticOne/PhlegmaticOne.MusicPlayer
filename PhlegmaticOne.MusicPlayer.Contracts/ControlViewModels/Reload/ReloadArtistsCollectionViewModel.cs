using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.CollectionViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;

public class ReloadArtistsCollectionViewModel : ReloadViewModelBase<ArtistsCollectionViewModel>
{
    public ReloadArtistsCollectionViewModel(IEntityCollectionGetService entityCollectionGetService) : base(entityCollectionGetService) { }

    protected override async Task ReloadViewModel(ArtistsCollectionViewModel viewModel)
    {
        var entities = await EntityCollectionGetService.GetEntityCollectionAsync<AllArtistsPreviewViewModel>();
        await viewModel.UpdateItems(entities.Artists);
    }
}