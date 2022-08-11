using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using PhlegmaticOne.MusicPlayer.Data.Context;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;

public class ReloadCollectionViewModel : ReloadViewModelBase<AlbumsCollectionViewModel>
{
    public ReloadCollectionViewModel(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

    protected override async Task ReloadViewModel(AlbumsCollectionViewModel viewModel)
    {
        var albums = await Mapper.From(DbContext.Set<Album>()).AdaptToTypeAsync<List<AlbumPreviewViewModel>>(); ;
        await viewModel.UpdateItems(albums);
    }
}