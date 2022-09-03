using Calabonga.UnitOfWork;
using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Actions;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.PagedList;

public class AdoNetTracksPagedListGetBase : AdoNetViewModelGetBase<TrackBaseViewModel>
{
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;
    public AdoNetTracksPagedListGetBase(ISqlClient sqlClient, IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider) : base(sqlClient)
    {
        _trackActionsProvider = trackActionsProvider;
    }
    protected override string CommandName => "Get_All_Tracks";

    protected override async Task<IPagedList<TrackBaseViewModel>> Create(SqlDataReader reader, int pageSize, int pageIndex)
    {
        var tracks = new List<TrackBaseViewModel>();
        TrackBaseViewModel? previous = default;
        while (await reader.ReadAsync())
        {
            var collectionId = await reader.GetFieldValueAsync<Guid>(0);
            var trackId = await reader.GetFieldValueAsync<Guid>(2);

            if (previous?.CollectionLink.Id == collectionId)
            {
                if (previous.Id == trackId)
                {
                    var artist = await GetTrackArtist(reader);
                    previous.ArtistLinks.Add(artist);
                    continue;
                }
                var notFullTrack = await GetNotFullTrack(reader, trackId);
                var newArtist = await GetTrackArtist(reader);
                notFullTrack.CollectionLink = previous.CollectionLink;
                notFullTrack.ArtistLinks = new List<ArtistLinkViewModel>() { newArtist };
                notFullTrack.Actions = _trackActionsProvider.GetActions(notFullTrack);
                previous = notFullTrack;
                tracks.Add(notFullTrack);
                continue;
            }

            var collectionTitle = await reader.GetFieldValueAsync<string>(1);
            var notFull = await GetNotFullTrack(reader, trackId);
            var artistLink = await GetTrackArtist(reader);

            var imageData = await reader.GetFieldValueAsync<byte[]>(11);
            var image = imageData.ToBitmap();
            var cover = image;

            notFull.CollectionLink = new CollectionLinkViewModel
            {
                Cover = cover,
                Id = collectionId,
                Title = collectionTitle
            };
            notFull.ArtistLinks = new List<ArtistLinkViewModel> { artistLink };

            notFull.Actions = _trackActionsProvider.GetActions(notFull);

            previous = notFull;
            tracks.Add(notFull);
        }

        return tracks.ToPagedList(pageSize:tracks.Count, pageIndex:0);
    }

    private async Task<TrackBaseViewModel> GetNotFullTrack(SqlDataReader reader, Guid id)
    {
        var trackTitle = await reader.GetFieldValueAsync<string>(3);
        var trackDuration = TimeSpan.FromTicks(await reader.GetFieldValueAsync<long>(4));
        var trackIsFavorite = await reader.GetFieldValueAsync<bool>(5);
        var localUrl = await reader.IsDBNullAsync(6) ? null : await reader.GetFieldValueAsync<string>(6);
        var onlineUrl = await reader.IsDBNullAsync(7) ? null : await reader.GetFieldValueAsync<string>(7);
        var trackTimePlayed = await reader.GetFieldValueAsync<TimeSpan>(8);
        return new()
        {
            Id = id,
            Title = trackTitle,
            IsFavorite = trackIsFavorite,
            Duration = trackDuration,
            IsDownloaded = string.IsNullOrEmpty(localUrl) == false,
            IsDownloading = false,
            LocalUrl = localUrl,
            OnlineUrl = onlineUrl,
            TimePlayed = trackTimePlayed
        };
    }

    private async Task<ArtistLinkViewModel> GetTrackArtist(SqlDataReader reader)
    {
        var artistId = await reader.GetFieldValueAsync<Guid>(9);
        var artistName = await reader.GetFieldValueAsync<string>(10);
        return new()
        {
            Id = artistId,
            Title = artistName
        };
    }
}