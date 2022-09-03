using Calabonga.UnitOfWork;
using Microsoft.Data.SqlClient;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Base;
using PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.PagedList;

public class AdoNetArtistsPagedListGetBase : AdoNetViewModelGetBase<ArtistPreviewViewModel>
{
    public AdoNetArtistsPagedListGetBase(ISqlClient sqlClient) : base(sqlClient) { }
    protected override string CommandName => "Get_All_Artists_Preview";

    protected override async Task<IPagedList<ArtistPreviewViewModel>> Create(SqlDataReader reader, int pageSize, int pageIndex)
    {
        var artists = new List<ArtistPreviewViewModel>();

        while (await reader.ReadAsync())
        {
            var id = await reader.GetFieldValueAsync<Guid>(0);
            var artistName = await reader.GetFieldValueAsync<string>(1);
            var tracksCount = await reader.GetFieldValueAsync<int>(2);
            var artist = new ArtistPreviewViewModel
            {
                Id = id,
                Title = artistName,
                TracksCount = tracksCount,
                Genres = new List<string>()
            };
            artists.Add(artist);
        }

        await reader.NextResultAsync();

        var index = 0;
        while (await reader.ReadAsync())
        {
            var cover = await reader.GetFieldValueAsync<byte[]>(0);
            var image = cover.ToBitmap();
            var albumCover = new AlbumCover { Cover = image };
            artists[index].Cover = albumCover.Cover;
            index++;
        }

        await reader.NextResultAsync();

        index = -1;
        var previousId = Guid.Empty;

        while (await reader.ReadAsync())
        {
            var artistId = await reader.GetFieldValueAsync<Guid>(0);
            var genreName = await reader.GetFieldValueAsync<string>(1);

            if (artistId != previousId)
            {
                index++;
            }

            artists[index].Genres.Add(genreName);
            previousId = artistId;
        }

        return artists.ToPagedList(pageSize:artists.Count, pageIndex:0);
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