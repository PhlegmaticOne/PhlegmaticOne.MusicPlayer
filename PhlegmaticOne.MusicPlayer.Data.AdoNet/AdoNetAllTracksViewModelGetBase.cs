using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAllTracksViewModelGetBase : AdoNetViewModelGetBase<AllTracksViewModel>
{
    public AdoNetAllTracksViewModelGetBase(ISqlClient sqlClient) : base(sqlClient) { }
    protected override string CommandName => "Get_All_Tracks";

    protected override async Task<AllTracksViewModel> Create(SqlDataReader reader)
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
                previous = notFullTrack;
                tracks.Add(notFullTrack);
                continue;
            }

            var collectionTitle = await reader.GetFieldValueAsync<string>(1);
            var notFull = await GetNotFullTrack(reader, trackId);
            var artistLink = await GetTrackArtist(reader);

            var imageData = await reader.GetFieldValueAsync<byte[]>(11);
            var image = imageData.ToBitmap();
            var cover = new AlbumCover { Cover = image };

            notFull.CollectionLink = new CollectionLinkViewModel
            {
                Cover = cover,
                Id = collectionId,
                Title = collectionTitle
            };
            notFull.ArtistLinks = new List<ArtistLinkViewModel> {artistLink};

            previous = notFull;
            tracks.Add(notFull);
        }

        return new AllTracksViewModel
        {
            Tracks = tracks
        };
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
            Name = artistName
        };
    }
}