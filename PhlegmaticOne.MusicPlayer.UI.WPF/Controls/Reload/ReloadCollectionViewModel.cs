using AutoMapper;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;

public class ReloadCollectionViewModel : ReloadViewModelBase<AlbumsCollectionViewModel>
{
    public ReloadCollectionViewModel(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    protected override async Task ReloadViewModel(AlbumsCollectionViewModel viewModel)
    {
        var repository = UnitOfWork.GetRepository<Album>();
        var albums = await repository.GetAllAsync(
            include: include => include
                .Include(p => p.Artists)
                .Include(p => p.AlbumCover)
        );

        var mapped = Mapper.Map<IList<AlbumEntityViewModel>>(albums);
        await viewModel.UpdateItems(mapped);
    }
}