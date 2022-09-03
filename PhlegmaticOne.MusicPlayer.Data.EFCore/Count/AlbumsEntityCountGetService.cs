using Calabonga.UnitOfWork;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Count;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.Count;

public class AlbumsEntityCountGetService : IGetEntitiesCountGetService<AlbumPreviewViewModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public AlbumsEntityCountGetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<int> GetEntitiesCountAsync() => await _unitOfWork.GetRepository<Album>().CountAsync();

    public int GetEntitiesCount() => _unitOfWork.GetRepository<Album>().Count();
}