using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet;

public class AdoNetArtistPreviewViewModelGet : AdoNetViewModelGetBase<ArtistPreviewViewModel>
{
    public AdoNetArtistPreviewViewModelGet(ISqlClient sqlClient) : base(sqlClient, "Get_Artist_Preview") { }

    protected override string ParameterName => "@artistId";

    protected override async Task<ArtistPreviewViewModel> Create(SqlDataReader reader, Guid id)
    {
        var artistName = await GetArtistName(reader);
        var genres = await GetGenres(reader);
        var tracksCount = await GetTracksCount(reader);
        var cover = await GetArtistCover(reader);

        return new ArtistPreviewViewModel
        {
            Id = id,
            Name = artistName,
            Cover = cover,
            Genres = genres,
            TracksCount = tracksCount
        };
    }

    private static async Task<string> GetArtistName(SqlDataReader reader)
    {
        await reader.ReadAsync();
        var artistName = await reader.GetFieldValueAsync<string>(0);
        await reader.NextResultAsync();
        return artistName;
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
        return new AlbumCover()
        {
            Cover = imageData.ToBitmap()
        };
    }
}