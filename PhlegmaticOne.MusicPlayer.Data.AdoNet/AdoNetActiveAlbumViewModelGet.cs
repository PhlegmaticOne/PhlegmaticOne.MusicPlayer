using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetActiveAlbumViewModelGet : AdoNetViewModelGetBase<ActiveAlbumViewModel>
{
    public AdoNetActiveAlbumViewModelGet(ISqlClient sqlClient) : base(sqlClient, "Get_Active_Album") { }
    protected override string ParameterName => "@albumId";
    protected override async Task<ActiveAlbumViewModel> Create(SqlDataReader reader, Guid id)
    {
        var tracks = new List<TrackBaseViewModel>();
        while (await reader.ReadAsync())
        {
            var trackId = await reader.GetFieldValueAsync<Guid>(0);
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
            tracks.Add(trackViewModel);
        }
        await reader.NextResultAsync();

        await reader.ReadAsync();
        var imageData = await reader.GetFieldValueAsync<byte[]>(0);
        var image = imageData.ToBitmap();
        var cover = new AlbumCover { Cover = image };
        await reader.NextResultAsync();

        var artistLinkViewModels = new List<ArtistLinkViewModel>();
        while (await reader.ReadAsync())
        {
            var artistId = await reader.GetFieldValueAsync<Guid>(0);
            var artistName = await reader.GetFieldValueAsync<string>(1);
            var artistLinkViewModel = new ArtistLinkViewModel
            {
                Id = artistId,
                Name = artistName
            };
            artistLinkViewModels.Add(artistLinkViewModel);
        }

        await reader.NextResultAsync();

        await reader.ReadAsync();
        var yearReleased = await reader.GetFieldValueAsync<int>(1);
        var albumType = Enum.Parse<AlbumType>(await reader.GetFieldValueAsync<string>(2));
        var albumOnlineUrl = await reader.GetFieldValueAsync<string>(3);
        var albumName = await reader.GetFieldValueAsync<string>(4);
        var dateAdded = await reader.GetFieldValueAsync<DateTime>(5);
        var albumIsFavorite = await reader.GetFieldValueAsync<bool>(6);

        var collectionLink = new CollectionLinkViewModel
        {
            Cover = cover,
            Title = albumName,
            Id = id
        };

        foreach (var trackBaseViewModel in tracks)
        {
            trackBaseViewModel.ArtistLinks = artistLinkViewModels;
            trackBaseViewModel.CollectionLink = collectionLink;
        }

        return new ActiveAlbumViewModel
        {
            Artists = artistLinkViewModels,
            Title = albumName,
            Id = id,
            Cover = cover,
            Tracks = tracks,
            IsFavorite = albumIsFavorite,
            IsDownloaded = string.IsNullOrEmpty(albumOnlineUrl) == false,
            IsDownloading = false,
            DateAdded = dateAdded,
            YearReleased = yearReleased,
            AlbumType = albumType
        };
    }
}