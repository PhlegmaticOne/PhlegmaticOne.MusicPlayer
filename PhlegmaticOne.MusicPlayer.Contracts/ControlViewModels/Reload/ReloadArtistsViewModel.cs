using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Data.Context;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;

public class ReloadArtistsViewModel : ReloadViewModelBase<ArtistsCollectionViewModel>
{
    public ReloadArtistsViewModel(ApplicationDbContext dbContext, IEntityCollectionGetService viewModelGetService) : base(dbContext, viewModelGetService) { }

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