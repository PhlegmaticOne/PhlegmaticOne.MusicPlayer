using System.Collections.Generic;
using System.Linq;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.Services;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;

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