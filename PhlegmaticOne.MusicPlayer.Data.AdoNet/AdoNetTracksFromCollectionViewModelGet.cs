using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetTracksFromCollectionViewModelGet : AdoNetViewModelGetBase<TracksFromCollectionViewModel>
{
    public AdoNetTracksFromCollectionViewModelGet(ISqlClient sqlClient) : base(sqlClient, "Get_Tracks_Of_Collection") { }
    protected override string ParameterName => "@collectionId";
    protected override async Task<TracksFromCollectionViewModel> Create(SqlDataReader reader, Guid id)
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

        await reader.ReadAsync();
        var title = await reader.GetFieldValueAsync<string>(0);
        await reader.NextResultAsync();
        var collectionLink = new CollectionLinkViewModel
        {
            Id = id,
            Cover = cover,
            Title = title
        };

        var artistLinkViewModels = new List<ArtistLinkViewModel>();
        if (reader.HasRows)
        {
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
        }

        foreach (var trackBaseViewModel in tracks)
        {
            trackBaseViewModel.ArtistLinks = artistLinkViewModels;
            trackBaseViewModel.CollectionLink = collectionLink;
        }

        return new TracksFromCollectionViewModel
        {
            Tracks = tracks
        };
    }
}