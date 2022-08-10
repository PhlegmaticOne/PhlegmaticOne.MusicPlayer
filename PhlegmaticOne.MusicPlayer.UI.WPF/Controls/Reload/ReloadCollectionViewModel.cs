using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;

public class ReloadCollectionViewModel : ReloadViewModelBase<AlbumsViewModel>
{
    public ReloadCollectionViewModel(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    protected override async Task ReloadViewModel(AlbumsViewModel viewModel)
    {
        var repository = UnitOfWork.GetRepository<Album>();
        var albums = await repository.GetAllAsync(
            include: include => include
                .Include(p => p.Artists)
                .Include(p => p.AlbumCover)
                //.Include(p => p.Songs)
        );

        var mapped = Mapper.Map<IList<AlbumEntityViewModel>>(albums);
        await viewModel.UpdateAlbums(mapped);
    }
}