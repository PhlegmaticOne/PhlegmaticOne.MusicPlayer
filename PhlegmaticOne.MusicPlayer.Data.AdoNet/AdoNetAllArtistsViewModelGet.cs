using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetAllArtistsViewModelGet : AdoNetViewModelGetBase<AllArtistsPreviewViewModel>
{
    public AdoNetAllArtistsViewModelGet(ISqlClient sqlClient) : base(sqlClient, "Get_All_Artists_Preview") { }
    protected override async Task<AllArtistsPreviewViewModel> Create(SqlDataReader reader)
    {
        var artists = new List<ArtistPreviewViewModel>();

        while (await reader.ReadAsync())
        {
            var (id, artistName) = await GetArtistName(reader);
            var genres = await GetGenres(reader);
            var tracksCount = await GetTracksCount(reader);
            var cover = await GetArtistCover(reader);
            var artist = new ArtistPreviewViewModel
            {
                Id = id,
                Name = artistName,
                Cover = cover,
                Genres = genres,
                TracksCount = tracksCount
            };
            artists.Add(artist);
        }

        return new AllArtistsPreviewViewModel
        {
            Artists = artists
        };
    }

    private static async Task<(Guid, string)> GetArtistName(SqlDataReader reader)
    {
        var artistId = await reader.GetFieldValueAsync<Guid>(0);
        var artistName = await reader.GetFieldValueAsync<string>(1);
        await reader.NextResultAsync();
        return (artistId, artistName);
    }

    private static async Task<List<string>> GetGenres(SqlDataReader reader)
    {
        var result = new List<string>();
        while (await reader.ReadAsync())
        {
            var genreName = await reader.GetFieldValueAsync<string>(0);
            result.Add(genreName);
        }
        await reader.NextResultAsync();
        return result;
    }
    private static async Task<int> GetTracksCount(SqlDataReader reader)
    {
        await reader.ReadAsync();
        var tracksCount = await reader.GetFieldValueAsync<int>(0);
        await reader.NextResultAsync();
        return tracksCount;
    }
    private static async Task<AlbumCover> GetArtistCover(SqlDataReader reader)
    {
        await reader.ReadAsync();
        var imageData = await reader.GetFieldValueAsync<byte[]>(0);
        await reader.NextResultAsync();
        return new AlbumCover()
        {
            Cover = imageData.ToBitmap()
        };
    }
}