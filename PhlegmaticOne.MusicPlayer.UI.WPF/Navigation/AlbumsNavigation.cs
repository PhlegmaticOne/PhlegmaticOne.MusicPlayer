using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class AlbumsNavigation : MusicNavigationBase<AlbumEntityViewModel>
{
    private readonly IDownloadService<AlbumEntityViewModel> _downloadService;
    private readonly IPlayerService _playerService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AlbumsNavigation(INavigator navigator, IDownloadService<AlbumEntityViewModel> downloadService,
        IPlayerService playerService, IUnitOfWork unitOfWork, IMapper mapper) : base(navigator)
    {
        _downloadService = downloadService;
        _playerService = playerService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    protected override async Task<BaseViewModel> CreateViewModel(AlbumEntityViewModel entity)
    {
        if (entity.Songs.Any() == false)
        {
            var albumRepository = _unitOfWork.GetRepository<Album>();
            var albumSongs = await albumRepository
                .GetAllAsync(
                    predicate: p => p.Id == entity.Id,
                    include: i => i.Include(s => s.Songs).ThenInclude(a => a.AlbumAppearances),
                    selector: s => s.Songs);

            var mapped = _mapper.Map<ICollection<SongEntityViewModel>>(albumSongs.First());
            foreach (var songEntityViewModel in mapped)
            {
                songEntityViewModel.CurrentCollection = _mapper.Map<CollectionDisplay>(entity);
            }
            entity.Songs = mapped;
        }
        return new AlbumViewModel(entity, _playerService, _downloadService);
    }
}