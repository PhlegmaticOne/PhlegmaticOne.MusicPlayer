using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.MusicFactories;

public class EFActiveAlbumViewModelFactory : NavigationFactoryBase<AlbumPreviewViewModel, ActiveAlbumViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHandMapperService _handMapperProvider;

    public EFActiveAlbumViewModelFactory(ApplicationDbContext dbContext, IHandMapperService handMapperProvider)
    {
        _dbContext = dbContext;
        _handMapperProvider = handMapperProvider;
    }

    public override async Task<ActiveAlbumViewModel> CreateViewModelAsync(AlbumPreviewViewModel entity)
    {
        var set = _dbContext.Set<Album>();
        var songs = await set.Where(x => x.Id == entity.Id).SelectMany(x => x.Songs).ToListAsync();
        var mapped = _handMapperProvider
            .AddParameter("CollectionSongs", songs)
            .Map<ActiveAlbumViewModel>(entity);
        return mapped;
    }
}