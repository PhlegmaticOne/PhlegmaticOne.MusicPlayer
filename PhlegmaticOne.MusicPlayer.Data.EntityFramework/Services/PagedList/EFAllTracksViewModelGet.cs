using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Actions;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Context;
using PhlegmaticOne.MusicPlayer.Data.EntityFramework.Helpers.Expressions;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Services.PagedList;

public class EFAllTracksViewModelGet : IEntityPagedListGet<TrackBaseViewModel>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;

    public EFAllTracksViewModelGet(ApplicationDbContext dbContext, IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider)
    {
        _dbContext = dbContext;
        _trackActionsProvider = trackActionsProvider;
    }
    public async Task<IPagedList<TrackBaseViewModel>> GetPagedListAsync(int pageSize, int pageIndex, 
        Func<TrackBaseViewModel, object>? sortFunc = null, Func<TrackBaseViewModel, bool>? selectFunc = null)
    {
        var set = _dbContext.Set<Song>();

        var modelSortExpression = TrackExpressionsConverter.ConvertToSortExpression(sortFunc);

        var modelSelectExpression = TrackExpressionsConverter.ConvertToSelectExpression(selectFunc);

        IQueryable<Song> query = set;
        query = modelSelectExpression is null ? query : query.Where(modelSelectExpression);
        query = modelSortExpression is null ? query : query.OrderByDescending(modelSortExpression);

        query = query
            .Include(x => x.Album)
            .ThenInclude(x => x.AlbumCover)
            .Include(x => x.Artists)
            .Include(x => x.Playlists)
            .ThenInclude(x => x.AlbumCover);

        query = query.Skip(pageIndex * pageSize).Take(pageSize);

        var list = await query.ToListAsync();
        var result = list.SelectMany(x =>
        {
            var result = x.Playlists.Select(playlist => Create(x, playlist.Id, playlist.Title, playlist.AlbumCover)).ToList();
            result.Add(Create(x, x.Album.Id, x.Album.Title, x.Album.AlbumCover));
            return result;
        });

        return result.ToPagedList(0, pageSize);
    }

    private TrackBaseViewModel Create(Song x, Guid collectionId, string collectionTitle, AlbumCover collectionCover)
    {
        var track = new TrackBaseViewModel
        {
            Id = x.Id,
            ArtistLinks = x.Artists.Select(a => new ArtistLinkViewModel
            {
                Id = a.Id,
                IsFavorite = a.IsFavorite,
                Title = a.Title
            }).ToList(),
            IsDownloaded = string.IsNullOrEmpty(x.LocalUrl) == false,
            LocalUrl = x.LocalUrl,
            Duration = x.Duration,
            TimePlayed = x.TimePlayed,
            IsFavorite = x.IsFavorite,
            CollectionLink = new CollectionLinkViewModel()
            {
                Title = collectionTitle,
                Cover = collectionCover.Cover,
                Id = collectionId
            },
            IsDownloading = false,
            OnlineUrl = x.OnlineUrl,
            Title = x.Title
        };
        track.Actions = _trackActionsProvider.GetActions(track);
        return track;
    }
}