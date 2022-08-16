using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.Services;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;

public class ReloadArtistsViewModel : ReloadViewModelBase<ArtistsCollectionViewModel>
{
    public ReloadArtistsViewModel(ApplicationDbContext dbContext, IViewModelGetService viewModelGetService) : base(dbContext, viewModelGetService) { }

    protected override async Task ReloadViewModel(ArtistsCollectionViewModel viewModel)
    {
        //var set = DbContext.Set<Artist>();
        //var artists = set
        //    .Include(x => x.Albums)
        //        .ThenInclude(x => x.Genres).Distinct()
        //    .Include(x => x.Albums)
        //        .ThenInclude(x => x.Songs);

        //var mapped = HandMapper.Map<ICollection<ArtistPreviewViewModel>>(artists);
    }
}