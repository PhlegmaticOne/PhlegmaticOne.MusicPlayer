using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAllTracksViewModelGet : AdoNetViewModelGetBase<AllTracksViewModel>
{
    private readonly ICacheService _cacheService;
    public AdoNetAllTracksViewModelGet(ISqlClient sqlClient, ICacheService cacheService) : base(sqlClient, "Get_All_Tracks")
    {
        _cacheService = cacheService;
    }
    protected override async Task<AllTracksViewModel> Create(SqlDataReader reader)
    {
        var tracks = new List<TrackBaseViewModel>();

        while (await reader.ReadAsync())
        {
            var currentTracks = new List<TrackBaseViewModel>();
            ICollection<ArtistLinkViewModel>? cachedArtists = default;
            CollectionLinkViewModel? cachedCollectionLinkViewModel = default;
            var needToRetrieveOther = true;
            do
            {
                var trackId = await reader.GetFieldValueAsync<Guid>(0);
                if (_cacheService.TryGetCachedValue(trackId, out TrackBaseViewModel cachedTrack))
                {
                    tracks.Add(cachedTrack);
                    cachedArtists ??= cachedTrack.ArtistLinks;
                    cachedCollectionLinkViewModel ??= cachedTrack.CollectionLink;
                    needToRetrieveOther = false;
                    continue;
                }
                var duration = TimeSpan.FromTicks(await reader.GetFieldValueAsync<long>(1));
                var isFavorite = await reader.GetFieldValueAsync<bool>(2);
                var localUrl = await reader.IsDBNullAsync(3) ? null : await reader.GetFieldValueAsync<string>(3);
                var onlineUrl = await reader.GetFieldValueAsync<string>(4);
                var timePlayed = await reader.GetFieldValueAsync<TimeSpan>(5);
                var trackTitle = await reader.GetFieldValueAsync<string>(6);
                var trackViewModel = new TrackBaseViewModel
                {
                    Duration = duration,
                    Id = trackId,
                    IsDownloaded = string.IsNullOrWhiteSpace(localUrl) == false,
                    IsDownloading = false,
                    IsFavorite = isFavorite,
                    LocalUrl = localUrl,
                    OnlineUrl = onlineUrl,
                    Title = trackTitle,
                    TimePlayed = timePlayed
                };
                currentTracks.Add(trackViewModel);

                needToRetrieveOther = true;

            } while (await reader.ReadAsync());

            await reader.NextResultAsync();

            if (needToRetrieveOther)
            {
                await reader.ReadAsync();

                var artistLinkViewModels = new List<ArtistLinkViewModel>();
                if (reader.FieldCount == 2)
                {
                    do
                    {
                        var artistId = await reader.GetFieldValueAsync<Guid>(0);
                        var artistName = await reader.GetFieldValueAsync<string>(1);
                        var artistLinkViewModel = new ArtistLinkViewModel
                        {
                            Id = artistId,
                            Name = artistName
                        };
                        artistLinkViewModels.Add(artistLinkViewModel);
                    } while (await reader.ReadAsync());

                    await reader.NextResultAsync();

                    await reader.ReadAsync();
                }

                var imageData = await reader.GetFieldValueAsync<byte[]>(0);
                var image = imageData.ToBitmap();
                var cover = new AlbumCover { Cover = image };
                await reader.NextResultAsync();

                await reader.ReadAsync();
                var collectionId = await reader.GetFieldValueAsync<Guid>(0);
                var title = await reader.GetFieldValueAsync<string>(1);
                await reader.NextResultAsync();

                var collectionLink = new CollectionLinkViewModel
                {
                    Id = collectionId,
                    Cover = cover,
                    Title = title
                };

                foreach (var trackBaseViewModel in currentTracks)
                {
                    trackBaseViewModel.ArtistLinks = artistLinkViewModels;
                    trackBaseViewModel.CollectionLink = collectionLink;
                }

                tracks.AddRange(currentTracks);
            }
            else
            {
                await reader.NextResultAsync();
                await reader.NextResultAsync();
                await reader.NextResultAsync();
            }
        }

        return new AllTracksViewModel
        {
            Tracks = tracks
        };
    }
}