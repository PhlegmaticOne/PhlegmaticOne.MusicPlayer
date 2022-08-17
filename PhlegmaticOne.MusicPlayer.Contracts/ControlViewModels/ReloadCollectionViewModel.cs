using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels;

public class ReloadCollectionViewModel : ReloadViewModelBase<AlbumsCollectionViewModel>
{
    public ReloadCollectionViewModel(ApplicationDbContext dbContext, IViewModelGetService viewModelGetService) : base(dbContext, viewModelGetService) { }

    protected override async Task ReloadViewModel(AlbumsCollectionViewModel viewModel)
    {
        var guids = DbContext.Set<Album>().Select(x => x.Id);
        var result = new List<AlbumPreviewViewModel>();
        foreach (var guid in guids)
        {
            var model = await ViewModelGetService.GetViewModelAsync<AlbumPreviewViewModel>(guid);
            result.Add(model);
        }
        await viewModel.UpdateItems(result);
    }
}