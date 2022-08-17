using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Data.Context;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;

public class ReloadCollectionViewModel : ReloadViewModelBase<AlbumsCollectionViewModel>
{
    public ReloadCollectionViewModel(ApplicationDbContext dbContext, IEntityCollectionGetService viewModelGetService) : base(dbContext, viewModelGetService) { }

    protected override async Task ReloadViewModel(AlbumsCollectionViewModel viewModel)
    {
        var result = await ViewModelGetService.GetEntityCollectionAsync<AllAlbumsPreviewViewModel>();
        await viewModel.UpdateItems(result.Albums);
    }
}